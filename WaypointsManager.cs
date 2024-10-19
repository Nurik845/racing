using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform carTransform; // Ссылка на машину, чтобы взять её высоту

    private void Start()
    {
        // Проверка наличия путевых точек
        if (waypoints.Length == 0)
        {
            Debug.LogError("Нет путевых точек назначенных в WaypointsManager!");
            return;
        }

        if (carTransform == null)
        {
            Debug.LogError("Машина не назначена в WaypointsManager!");
            return;
        }

        // Получаем высоту машины
        float carHeight = carTransform.position.y;

        // Выровнять все путевые точки по высоте машины
        AlignWaypointsToCarHeight(carHeight);
    }

    void AlignWaypointsToCarHeight(float height)
    {
        foreach (Transform waypoint in waypoints)
        {
            Vector3 position = waypoint.position;
            position.y = height; // Устанавливаем одинаковую высоту для всех точек
            waypoint.position = position;
        }

        Debug.Log("Все путевые точки выровнены по высоте: " + height);
    }
}
