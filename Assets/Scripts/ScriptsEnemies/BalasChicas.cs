using System.Collections;
using UnityEngine;

public class BalasChicas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Truck"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(-100);
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullets"))
        {
            gameObject.SetActive(false);
        }
    }





}
