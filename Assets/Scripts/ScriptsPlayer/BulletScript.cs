using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Si la bala sale de los límites, desactívala
        if (Mathf.Abs(transform.position.x) > 100f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Por ejemplo, al chocar con enemigo:
        if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
