using System.Collections;
using UnityEngine;

public class HelicopterEnemie : MonoBehaviour
{
    public float speed;
    public float erraticSpeed;
    public GameObject EnemieSprite1;
    public GameObject EnemieSprite2;
    public bool dirEnemieSprite;
    public float maxPos;
    public float minPos;
    private int direction = -1;
    private int erraticDir;
    public GameObject bulletEnemie;
    public Transform shootPointEnemie;
    public bool shoot;

    public void Start()
    {
        shoot = true;
        dirEnemieSprite = true;
        StartCoroutine(EnemieHelAnimator());
    }
    public void Update()
    {
        DirHelicopter();
        ShootEnemie();
    }

    public void DirHelicopter()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        if (transform.position.x <= minPos)
        {
            direction = 1;
        }
        else if(transform.position.x >= maxPos)
        {
            direction = -1;
        }
        transform.Translate(Vector2.up * erraticDir * erraticSpeed * Time.deltaTime);
        int index = Random.Range(0, 2);
        if (index == 1)
        {
            erraticDir = 1;
        }
        else
        {
            erraticDir = -1;
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
        int index = Random.Range(0, 8);
        if (index == 1 && shoot)
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

}
