using UnityEngine;

public class HealthAsteroid : MonoBehaviour
{
    public float fallSpeed = 2.2f;
    public float bottomY = -6f;

    [Header("Sound")]
    public AudioClip healthPickupSound;
    public float soundVolume = 1f;

    void Update()
    {
        transform.position +=
            Vector3.down *
            fallSpeed *
            Time.deltaTime;

        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player != null)
        {
            if (healthPickupSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    healthPickupSound,
                    transform.position,
                    soundVolume
                );
            }

            player.AddLife();

            Destroy(gameObject);
        }
    }
}