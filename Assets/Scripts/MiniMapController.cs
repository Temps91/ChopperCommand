using UnityEngine;
using System.Collections.Generic;

public class MiniMapController : MonoBehaviour
{
    public Transform player;
    public List<Transform> helicopters = new List<Transform>();
    public List<Transform> trucks = new List<Transform>();

    public GameObject playerBlipPrefab;
    public GameObject helicopterBlipPrefab;
    public GameObject truckBlipPrefab;

    private GameObject playerBlip;
    private List<GameObject> helicopterBlips = new List<GameObject>();
    private List<GameObject> truckBlips = new List<GameObject>();

    public float worldXMin;
    public float worldXMax;
    public float worldYMin;
    public float worldYMax;

    private Vector2 minimapSize;

    void Start()
    {
        // Obtenemos el tamaño de la imagen de fondo
        SpriteRenderer bg = GetComponent<SpriteRenderer>();
        if (bg != null)
            minimapSize = bg.bounds.size;
        else
            minimapSize = new Vector2(10f, 10f); // Valor por defecto

        // Instanciamos blip del jugador
        if (playerBlipPrefab != null)
            playerBlip = Instantiate(playerBlipPrefab, transform);

        // Instanciamos blips de helicópteros
        foreach (Transform hel in helicopters)
        {
            if (hel != null && helicopterBlipPrefab != null)
            {
                GameObject blip = Instantiate(helicopterBlipPrefab, transform);
                helicopterBlips.Add(blip);
            }
        }

        // Instanciamos blips de camiones
        foreach (Transform tr in trucks)
        {
            if (tr != null && truckBlipPrefab != null)
            {
                GameObject blip = Instantiate(truckBlipPrefab, transform);
                truckBlips.Add(blip);
            }
        }
    }

    void Update()
    {
        // Actualizamos posición del jugador
        if (player != null && playerBlip != null)
            playerBlip.transform.localPosition = WorldToMiniMap(player.position);

        // Actualizamos posición de helicópteros
        for (int i = helicopterBlips.Count - 1; i >= 0; i--)
        {
            if (helicopters[i] != null)
                helicopterBlips[i].transform.localPosition = WorldToMiniMap(helicopters[i].position);
            else
            {
                Destroy(helicopterBlips[i]);
                helicopterBlips.RemoveAt(i);
                helicopters.RemoveAt(i);
            }
        }

        // Actualizamos posición de camiones
        for (int i = truckBlips.Count - 1; i >= 0; i--)
        {
            if (trucks[i] != null)
                truckBlips[i].transform.localPosition = WorldToMiniMap(trucks[i].position);
            else
            {
                Destroy(truckBlips[i]);
                truckBlips.RemoveAt(i);
                trucks.RemoveAt(i);
            }
        }
    }

    private Vector2 WorldToMiniMap(Vector3 worldPos)
    {
        float xNorm = Mathf.Clamp01(Mathf.InverseLerp(worldXMin, worldXMax, worldPos.x));
        float yNorm = Mathf.Clamp01(Mathf.InverseLerp(worldYMin, worldYMax, worldPos.y));

        float xMap = (xNorm - 0.5f) * minimapSize.x;
        float yMap = (yNorm - 0.5f) * minimapSize.y;

        return new Vector2(xMap, yMap);
    }
}
