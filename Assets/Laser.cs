using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 7f;
    public float topY = 6f;

    void Update()
    {
        transform.position +=
            transform.up * speed * Time.deltaTime;

        if (transform.position.y > topY ||
            transform.position.x < -10f ||
            transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid =
            other.GetComponent<Asteroid>();

        if (asteroid != null)
        {
            asteroid.TakeDamage();

            Destroy(gameObject);
        }
    }
}