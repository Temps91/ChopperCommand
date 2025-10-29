using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public bool direccionSprite;
    public bool timeNextShoot;
    public GameObject helicoptero1;
    public GameObject helicoptero2;
    public bool spriteOn;



    public void Start()
    {
        direccionSprite = true;
        timeNextShoot = false;
        spriteOn = false;
        StartCoroutine(ActSprite());
    }
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
        if (horizontal > 0 && !direccionSprite)
        {
            Flip();
        }
        else if (horizontal < 0 && direccionSprite)
        {
            Flip();
        }
    }

    public void Shoot()
    {
        if (Input.GetKey(KeyCode.K) && !timeNextShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            Vector2 direction = direccionSprite ? Vector2.right : Vector2.left;
            bullet.GetComponent<BulletScript>().SetDirection(direction);
            timeNextShoot = true;
            StartCoroutine(ShootTime());
        }
    }

    public void Flip()
    {
        direccionSprite = !direccionSprite;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        
    }

    IEnumerator ShootTime()
    {
        yield return new WaitForSeconds(0.4f);
        timeNextShoot = false;
    }
    
    IEnumerator ActSprite()
    {
        while(true)
        {
            helicoptero1.SetActive(true);
            helicoptero2.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            helicoptero1.SetActive(false);
            helicoptero2.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }

    }

}
