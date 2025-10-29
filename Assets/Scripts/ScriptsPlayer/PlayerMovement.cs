using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public GameObject shootPoint;

    public void FixedUpdate()
    {
        Movimiento();
        Shoot();
    }
    public void Movimiento()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        }
    }
}
