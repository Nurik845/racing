using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Объект (машина), за которым следует камера
    public Vector3 offset = new Vector3(0, 5, -10); // Смещение камеры от машины
    public float smoothSpeed = 2f; // Скорость сглаживания перемещений камеры

    private void LateUpdate() // Используем LateUpdate для сглаживания камеры
    {
        if (target == null) return;

        // Рассчитываем желаемую позицию камеры с учётом смещения
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Плавное перемещение камеры к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Применяем сглаженную позицию
        transform.position = smoothedPosition;

        // Поворачиваем камеру, чтобы она всегда смотрела на машину
        transform.LookAt(target);
    }
}
