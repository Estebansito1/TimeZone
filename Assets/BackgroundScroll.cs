using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float baseScrollSpeed = 0.5f;

    private float backgroundHeight;
    private ScoreCounter scoreCounter;

    void Start()
    {
        SpriteRenderer spriteRenderer =
            GetComponent<SpriteRenderer>();

        backgroundHeight =
            spriteRenderer.bounds.size.y;

        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();

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

        if (scoreCounter != null)
        {
            level =
                (scoreCounter.score / 1000) + 1;
        }

        float currentScrollSpeed =
            baseScrollSpeed +
            ((level - 1) * 0.15f);

        transform.position +=
            Vector3.down *
            currentScrollSpeed *
            Time.deltaTime;

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