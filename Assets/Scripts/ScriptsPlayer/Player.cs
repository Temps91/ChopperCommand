using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public int maxLives;
    private int currentLives;
    public float invulTime;
    private bool invulnerable;

    public Image[] lives;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateLives();
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
