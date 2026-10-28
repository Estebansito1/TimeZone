using UnityEngine;

public class HealthAsteroid : MonoBehaviour
{
    [Header("Movement")]
    public float fallSpeed = 2.2f;
    public float bottomY = -6f;

    [Header("Sound")]
    public AudioClip healthPickupSound;
    public float soundVolume = 1f;

    void Update()
    {
        // Move the health asteroid downward each frame.
        transform.position +=
            Vector3.down *
            fallSpeed *
            Time.deltaTime;

        // Remove it after it leaves the playable area.
        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check whether the object touching this pickup is the player.
        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player != null)
        {
            // Play the health pickup sound.
            if (healthPickupSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    healthPickupSound,
                    transform.position,
                    soundVolume
                );
            }

            // Restore one life. PlayerController prevents going above max lives.
            player.AddLife();

            // Remove the pickup after it is collected.
            Destroy(gameObject);
        }
    }
}