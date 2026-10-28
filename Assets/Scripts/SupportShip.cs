using UnityEngine;

public class SupportShip : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2.5f;
    public float lifeTime = 5f;

    [Header("Scatter Movement")]
    public float maxHorizontalDrift = 0.5f;

    [Header("Shooting")]
    public GameObject laserPrefab;
    public float minFireDelay = 0.25f;
    public float maxFireDelay = 0.65f;

    [Header("Sound")]
    public AudioClip laserSound;
    public AudioClip hitSound;
    public float soundVolume = 0.6f;

    private float nextFireTime = 0f;
    private float timer = 0f;

    private float fireDelay;
    private Vector3 moveDirection;

    void Start()
    {
        // Give each support ship its own firing speed.
        fireDelay =
            Random.Range(
                minFireDelay,
                maxFireDelay
            );

        // Give each ship a slightly different flight direction
        // so the group scatters while still moving upward.
        float randomX =
            Random.Range(
                -maxHorizontalDrift,
                maxHorizontalDrift
            );

        moveDirection =
            new Vector3(
                randomX,
                1f,
                0f
            ).normalized;

        // Stagger the first shot so all three ships
        // do not fire at the exact same moment.
        nextFireTime =
            Time.time +
            Random.Range(
                0.05f,
                fireDelay
            );
    }

    void Update()
    {
        // Move upward with a small random sideways drift.
        transform.position +=
            moveDirection *
            moveSpeed *
            Time.deltaTime;

        // Automatically fire using this ship's individual fire rate.
        if (Time.time >= nextFireTime)
        {
            FireLaser();

            nextFireTime =
                Time.time +
                Random.Range(
                    minFireDelay,
                    maxFireDelay
                );
        }

        // Remove the support ship after its support period ends.
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void FireLaser()
    {
        if (laserPrefab == null)
        {
            return;
        }

        Vector3 spawnPosition =
            transform.position +
            new Vector3(
                0f,
                0.8f,
                0f
            );

        Instantiate(
            laserPrefab,
            spawnPosition,
            Quaternion.identity
        );

        if (laserSound != null)
        {
            AudioSource.PlayClipAtPoint(
                laserSound,
                transform.position,
                soundVolume
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid =
            other.GetComponent<Asteroid>();

        if (asteroid != null)
        {
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    hitSound,
                    transform.position,
                    soundVolume
                );
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}