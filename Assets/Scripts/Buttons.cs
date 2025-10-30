using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    [SerializeField]
    public GameObject panel;
    public void Jugar()
    {
        SceneManager.LoadScene("Game");
    }
    public void Controles()
    {
        panel.SetActive(true);
    }
    public void CerrarPanel()
    {
        panel.SetActive(false);
    }
    public void VolverAJugar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
