using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.1f;
    private float minX = -50f; // Límite hacia atrás

    void LateUpdate()
    {
        if (player == null) return;

        // Posición objetivo de la cámara solo en X
        float targetX = Mathf.Max(player.position.x, minX);
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);

        // Movimiento suave
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
    }
}