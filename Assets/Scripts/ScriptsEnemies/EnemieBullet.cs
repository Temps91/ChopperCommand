using System.Collections;
using UnityEngine;

public class EnemieBullet : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject originalBullet;
    public float speedBullet;

    public void Start()
    {
        originalBullet.SetActive(true);
        bullet1.SetActive(false);
        bullet2.SetActive(false);
    }

    public void Update()
    {
        StartCoroutine(BulletActive());
        MovementBullet();

    }

    public void MovementBullet()
    {
        if (bullet1.activeSelf)
        {
            bullet1.transform.Translate(Vector2.up * speedBullet * Time.deltaTime);
        }
        if (bullet2.activeSelf)
        {
            bullet2.transform.Translate(Vector2.down * speedBullet * Time.deltaTime);
        }

    }

    IEnumerator BulletActive()
    {
        yield return new WaitForSeconds(1f);
        originalBullet.SetActive(false);
        bullet1.SetActive(true);
        bullet2.SetActive(true);
    }


}
