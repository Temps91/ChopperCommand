using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiManager : MonoBehaviour
{
    public GameObject[] spawnEnemies;
    public GameObject enemieHelicopter;

    public void Start()
    {
        StartCoroutine(SpawnEnemie());
    }


    IEnumerator SpawnEnemie()
    {
        while (true)
        {
            int index = Random.Range(0, spawnEnemies.Length);
            Instantiate(enemieHelicopter, spawnEnemies[index].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }


    

}
