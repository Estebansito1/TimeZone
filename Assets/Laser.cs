using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 7f;
    public float topY = 6f;

    void Update()
    {
        // Move in the laser's local up direction.
        // This allows angled lasers to travel diagonally.
        transform.position +=
            transform.up *
            speed *
            Time.deltaTime;

        // Remove lasers after they leave the playable area.
        if (transform.position.y > topY ||
            transform.position.x < -10f ||
            transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check whether the laser hit a regular asteroid.
        Asteroid asteroid =
            other.GetComponent<Asteroid>();

        if (asteroid != null)
        {
            // Apply damage to the asteroid.
            asteroid.TakeDamage();

            // Remove the laser after impact.
            Destroy(gameObject);
        }
    }
}