using System.Collections;
using UnityEngine;

public class HelicopterEnemie : MonoBehaviour
{
    public float speed;
    public float erraticSpeed;
    public GameObject EnemieSprite1;
    public GameObject EnemieSprite2;
    public bool dirEnemieSprite;
    private float targetX = 0f;
    private bool goingToCenter = true;
    private int direction = -1;
    private int erraticDir;
    public GameObject bulletEnemie;
    public Transform shootPointEnemie;
    public bool shoot;
    public GameObject particlesDead;
    private bool freeze = false;

    public void Start()
    {
        particlesDead.SetActive(false);
        shoot = true;
        dirEnemieSprite = true;
        StartCoroutine(EnemieHelAnimator());
        if (transform.position.x < 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }
    public void Update()
    {
        DirHelicopter();
        StartCoroutine(ActivarDisparo());
    }

    public void DirHelicopter()
    {
        int index = Random.Range(0, 2);
        if (index == 1)
        {
            erraticDir = 1;
        }
        else
        {
            erraticDir = -1;
        }
        if (!freeze)
        {
        transform.Translate(Vector2.up * erraticDir * erraticSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        }

        if (goingToCenter && Mathf.Abs(transform.position.x - targetX) < 0.5f)
        {
            goingToCenter = false;
            direction *= -1;
        }
        else if (!goingToCenter &&(transform.position.x < -10f && direction <0) || (transform.position.x > 10f && direction > 0))
        {
            goingToCenter =true;
            direction *= -1;
        }



        if (direction > 0 && !dirEnemieSprite)
        {
            FlipEnemie();
        }
        else if (direction < 0 && dirEnemieSprite)
        {
            FlipEnemie();
        }

    }
    public void FlipEnemie()
    {
        dirEnemieSprite = !dirEnemieSprite;
        Vector3 scaleEnemie = transform.localScale;
        scaleEnemie.x *= -1;
        transform.localScale = scaleEnemie;
    }

    public void ShootEnemie()
    {
        int index = Random.Range(0, 5);
        if (index == 4 && shoot)
        {
            Instantiate(bulletEnemie, shootPointEnemie.transform.position, Quaternion.identity);
            Vector2 directionBullet = dirEnemieSprite ? Vector2.right : Vector2.left;
            shoot = false;
        }
    }

    IEnumerator EnemieHelAnimator()
    {
        while(true)
        {
            EnemieSprite1.SetActive(true);
            EnemieSprite2.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            EnemieSprite1.SetActive(false);
            EnemieSprite2.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBullets"))
        {
            freeze = true;

            particlesDead.SetActive(true);
            StopAllCoroutines();
            EnemieSprite1.SetActive(false);
            EnemieSprite2.SetActive(false);

            StartCoroutine(DesactivarParticulas());

        }
    }

    IEnumerator DesactivarParticulas()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }

    IEnumerator ActivarDisparo()
    {
        yield return new WaitForSeconds(2f);
        ShootEnemie();
    }



}
