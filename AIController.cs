using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player; // Ссылка на машину игрока
    private int currentWaypointIndex = 0;
    public float maxSpeed = 40f; // Максимальная скорость ИИ
    public float accelerationTime = 10f; // Время, за которое ИИ достигает максимальной скорости
    public float rotationSpeed = 2f; // Скорость поворота
    public float waypointRadius = 2f; // Радиус обнаружения путевой точки
    public float playerDetectionRadius = 15f; // Радиус, при котором ИИ замечает игрока и ускоряется

    private float currentSpeed = 0f; // Текущая скорость
    private float speedIncrement; // Прирост скорости за каждый кадр

    private void Start()
    {
        // Рассчитываем прирост скорости за каждый кадр
        speedIncrement = maxSpeed / accelerationTime;
    }

    private void FixedUpdate()
    {
        if (waypoints.Length == 0 || player == null) return;

        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Получаем текущую путевую точку
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0; // Игнорируем изменение по высоте (если есть)

        // Поворачиваем машину ИИ в сторону путевой точки с плавным поворотом
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Постепенное ускорение
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement * Time.deltaTime; // Увеличиваем скорость
        }

        // Проверка расстояния до игрока и дополнительное ускорение
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < playerDetectionRadius)
        {
            // Ускорение, если игрок рядом (добавляем ещё больше скорости)
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, (playerDetectionRadius - distanceToPlayer) / playerDetectionRadius);
        }

        // Перемещаем машину ИИ вперёд с текущей скоростью
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Проверка близости к путевой точке
        if (direction.magnitude < waypointRadius)
        {
            // Переходим к следующей путевой точке
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

