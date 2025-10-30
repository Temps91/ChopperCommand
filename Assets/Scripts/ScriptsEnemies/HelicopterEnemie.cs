using System.Collections;
using UnityEngine;

public class HelicopterEnemie : MonoBehaviour
{
    public float speed;
    public float erraticSpeed;
    public GameObject EnemieSprite1;
    public GameObject EnemieSprite2;
    public bool dirEnemieSprite;
    private int direction = -1;
    private int erraticDir;
    public GameObject bulletEnemie;
    public Transform shootPointEnemie;
    public bool shoot;
    public float shootRange = 3f;
    public GameObject particlesDead;
    private bool freeze = false;
    public Transform player;
    public float followDelay;
    private float followTimer;

    public void Start()
    {
        particlesDead.SetActive(false);
        shoot = false;
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

    }

    public void DirHelicopter()
    {

        if (transform.position.y <= -2.1f)
        {
            erraticDir = 1;

        }
        else if (transform.position.y >= 1.4f)
        {
            erraticDir = -1;

        }
        else
        {
            int index = Random.Range(0, 2);
            erraticDir = index == 1 ? 1 : -1;
        }

        if (!freeze)
        {
            followTimer += Time.deltaTime;
            if (followTimer >= followDelay)
            {
                if (player != null)
                {
                    if (player.position.x > transform.position.x && direction != 1)
                    {
                        direction = 1;
                        FlipEnemie();
                    }
                    else if (player.position.x < transform.position.x && direction != -1)
                    {
                        direction = -1;
                        FlipEnemie();
                    }
                    followTimer = 0f;
                }
                
            }
            transform.Translate(Vector2.up * erraticDir * erraticSpeed * Time.deltaTime);
            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        }


  
        float limitY = Mathf.Clamp(transform.position.y, -1f, 1.4f);
        transform.position = new Vector2(transform.position.x, limitY);



        if (direction > 0 && !dirEnemieSprite)
        {
            FlipEnemie();

        }
        else if (direction < 0 && dirEnemieSprite)
        {
            FlipEnemie();

        }
        if (player != null)
        {
            float distancePlayer = Mathf.Abs(player.position.x - transform.position.x);


            if (!shoot && distancePlayer <= shootRange)
            {
                ShootEnemie();
                shoot = true;
            }

            if (shoot && distancePlayer > shootRange)
            {
                shoot = false;
            }
        }
    }

    public void ShootEnemie()
    {
        Instantiate(bulletEnemie, shootPointEnemie.transform.position, Quaternion.identity);
        Vector2 directionBullet = dirEnemieSprite ? Vector2.right : Vector2.left;
        
    }
    public void FlipEnemie()
    {
        dirEnemieSprite = !dirEnemieSprite;
        Vector3 scaleEnemie = transform.localScale;
        scaleEnemie.x *= -1;
        transform.localScale = scaleEnemie;
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


            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(100);
            }

            StartCoroutine(DesactivarParticulas());

        }

    }

    IEnumerator DesactivarParticulas()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }




}
