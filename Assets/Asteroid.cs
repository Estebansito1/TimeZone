using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float baseFallSpeed = 2.5f;
    public float bottomY = -6f;

    [Header("Health")]
    public int hitPoints = 2;

    [Header("Score")]
    public int points = 100;

    [Header("Sound")]
    public AudioClip hitSound;
    public AudioClip explosionSound;
    public float soundVolume = 1f;

    private SpriteRenderer spriteRenderer;
    private ScoreCounter scoreCounter;

    void Start()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();

        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();
    }

    void Update()
    {
        int level = 1;

        if (scoreCounter != null)
        {
            level =
                (scoreCounter.score / 1000) + 1;
        }

        float currentSpeed =
            baseFallSpeed +
            ((level - 1) * 0.35f);

        transform.position +=
            Vector3.down *
            currentSpeed *
            Time.deltaTime;

        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        hitPoints--;

        // First hit
        if (hitPoints == 1)
        {
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    hitSound,
                    transform.position,
                    soundVolume
                );
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.color =
                    new Color(
                        1f,
                        0.5f,
                        0.5f
                    );
            }
        }

        // Second hit - asteroid destroyed
        if (hitPoints <= 0)
        {
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    explosionSound,
                    transform.position,
                    soundVolume
                );
            }

            if (scoreCounter != null)
            {
                scoreCounter.AddScore(points);
            }

            Destroy(gameObject);
        }
    }
}