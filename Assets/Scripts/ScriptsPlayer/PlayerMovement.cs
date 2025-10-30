using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int maxLives;
    private int currentLives;
    public float invulTime;
    private bool invulnerable;
    public Image[] lives;
    private SpriteRenderer spriteRenderer;
    public bool bulletOn;




    public void Start()
    {
        direccionSprite = true;
        timeNextShoot = false;
        spriteOn = false;
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateLives();
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

        float limitX = Mathf.Clamp(transform.position.x, -50, 50);
        float limitY = Mathf.Clamp(transform.position.y, -2.1f, 1.4f);
        transform.position = new Vector3(limitX, limitY, transform.position.z);

        if (horizontal > 0 && !direccionSprite)
            Flip();
        else if (horizontal < 0 && direccionSprite)
            Flip();
    }

    public void Shoot()
    {
        if (Input.GetKey(KeyCode.K) && !timeNextShoot)
        {
            GameObject bullet = BulletPoolManager.Instance.GetBullet();
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (invulnerable)
        {
            return;
        }

        currentLives--;

        UpdateLives();

        if (currentLives <= 0)
        {
            Debug.Log("jugador muerto");
            Destroy(gameObject);
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            return;
        }

        StartCoroutine(Invulnerability());
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        helicoptero1.SetActive(false);
        helicoptero2.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        helicoptero1.SetActive(true);
        helicoptero2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        helicoptero1.SetActive(false);
        helicoptero2.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        helicoptero1.SetActive(true);
        helicoptero2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        helicoptero1.SetActive(true);
        helicoptero2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        helicoptero1.SetActive(true);
        helicoptero2.SetActive(true);
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
    }

    private void UpdateLives()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].enabled = i < currentLives;
        }
    }

}
