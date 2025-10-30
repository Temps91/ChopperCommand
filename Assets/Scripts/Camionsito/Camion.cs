

using UnityEngine;

public class Camion : MonoBehaviour
{
    public float speed;
    public float direction;
    private void Start()
    {
        direction = -1;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * direction *  speed * Time.deltaTime);
        if (transform.position.x >= 50)
        {
            ScoreManager.instance.AddScore(200);
        }
    }


}
