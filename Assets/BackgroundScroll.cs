using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Scrolling")]
    public float baseScrollSpeed = 0.5f;

    private float backgroundHeight;
    private ScoreCounter scoreCounter;

    void Start()
    {
        // Read the height of this background sprite.
        SpriteRenderer spriteRenderer =
            GetComponent<SpriteRenderer>();

        backgroundHeight =
            spriteRenderer.bounds.size.y;

        // Use the score to scale background speed by level.
        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();

        // Place the second background directly above the first one.
        if (gameObject.name == "Background2")
        {
            transform.position =
                new Vector3(
                    transform.position.x,
                    backgroundHeight,
                    transform.position.z
                );
        }
    }

    void Update()
    {
        int level = 1;

        // Every 1000 points increases the current level.
        if (scoreCounter != null)
        {
            level =
                (scoreCounter.score / 1000) + 1;
        }

        // Increase scrolling speed slightly as difficulty increases.
        float currentScrollSpeed =
            baseScrollSpeed +
            ((level - 1) * 0.15f);

        transform.position +=
            Vector3.down *
            currentScrollSpeed *
            Time.deltaTime;

        // When a background moves off-screen,
        // move it above the other background for seamless scrolling.
        if (transform.position.y <= -backgroundHeight)
        {
            transform.position +=
                new Vector3(
                    0f,
                    backgroundHeight * 2f,
                    0f
                );
        }
    }
}