using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public Vector2 direction = Vector2.right;



    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = direction.x > 0 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
