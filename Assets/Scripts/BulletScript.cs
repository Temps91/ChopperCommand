using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;

    public void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
