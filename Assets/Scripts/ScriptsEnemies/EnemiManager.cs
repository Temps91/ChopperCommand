using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiManager : MonoBehaviour
{
    public GameObject[] spawnEnemies;
    public GameObject enemieHelicopter;
    public GameObject enemiePlane;
    public Transform player;

    public void Start()
    {
        StartCoroutine(SpawnEnemie());
    }


    IEnumerator SpawnEnemie()
    {
        while (true)
        {
            int index = Random.Range(0, spawnEnemies.Length);
            
            GameObject hel = Instantiate(enemieHelicopter, spawnEnemies[index].transform.position, Quaternion.identity);
            HelicopterEnemie helScript = hel.GetComponent<HelicopterEnemie>();
            if (helScript != null)
            {
                helScript.player = player;
            }
            GameObject pla = Instantiate(enemiePlane, spawnEnemies[index].transform.position,Quaternion.identity);
            PlaneEnemie plaScript = pla.GetComponent<PlaneEnemie>();
            if (plaScript != null)
            {
                plaScript.player = player;
            }
            yield return new WaitForSeconds(4f);
        }
    }


    

}
