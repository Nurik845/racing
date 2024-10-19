using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Ссылки на WheelCollider
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    // Максимальный крутящий момент и угол поворота
    public float maxTorque = 600f; // Увеличенный крутящий момент для задних колес
    public float maxSteerAngle = 15f;

    // Объекты визуальных колес для синхронизации с WheelCollider
    public Transform frontLeftVisual;
    public Transform frontRightVisual;
    public Transform rearLeftVisual;
    public Transform rearRightVisual;

    // Постепенное ускорение
    public float startSpeed = 50f; // Увеличенная начальная скорость
    public float maxSpeed = 150f; // Увеличенная максимальная скорость
    public float accelerationTime = 5f; // Время для более быстрого достижения максимальной скорости
    private float currentSpeed; // Текущая скорость
    private float speedIncrement; // Прирост скорости за каждый кадр

    private void Start()
    {
        // Начальная скорость и расчет прироста скорости
        currentSpeed = startSpeed;
        speedIncrement = (maxSpeed - startSpeed) / accelerationTime;
    }

    private void FixedUpdate()
    {
        // Получение ввода от игрока
        float moveInput = Input.GetAxis("Vertical"); // W/S или стрелки вверх/вниз
        float steerInput = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо

        // Постепенное ускорение, если игрок движется
        if (moveInput > 0 && currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement * Time.deltaTime; // Увеличиваем скорость быстрее
        }
        else if (moveInput <= 0)
        {
            currentSpeed = 0; // Останавливаем машину, если нет ввода
        }

        // Применение крутящего момента к задним колесам
        rearLeftWheel.motorTorque = moveInput * maxTorque * (currentSpeed / maxSpeed);
        rearRightWheel.motorTorque = moveInput * maxTorque * (currentSpeed / maxSpeed);

        // Применение угла поворота к передним колесам
        frontLeftWheel.steerAngle = steerInput * maxSteerAngle;
        frontRightWheel.steerAngle = steerInput * maxSteerAngle;

        // Синхронизация визуальных колес с WheelCollider
        UpdateWheelVisual(frontLeftWheel, frontLeftVisual);
        UpdateWheelVisual(frontRightWheel, frontRightVisual);
        UpdateWheelVisual(rearLeftWheel, rearLeftVisual);
        UpdateWheelVisual(rearRightWheel, rearRightVisual);
    }

    // Метод для синхронизации визуальных колес с физическими
    private void UpdateWheelVisual(WheelCollider collider, Transform visualWheel)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.position = position;
        visualWheel.rotation = rotation;
    }
}

