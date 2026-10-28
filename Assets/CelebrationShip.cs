using UnityEngine;

public class CelebrationShip : MonoBehaviour
{
    [Header("Movement")]
    public float flySpeed = 8f;
    public float topY = 7f;

    [Header("Random Direction")]
    public float maxHorizontalDirection = 0.4f;

    private Vector3 moveDirection;

    void Start()
    {
        // Give each celebration ship a slightly different
        // horizontal direction while still moving upward.
        float randomX =
            Random.Range(
                -maxHorizontalDirection,
                maxHorizontalDirection
            );

        moveDirection =
            new Vector3(
                randomX,
                1f,
                0f
            ).normalized;
    }

    void Update()
    {
        // Move the celebration ship in its randomly chosen direction.
        transform.position +=
            moveDirection *
            flySpeed *
            Time.deltaTime;

        // Remove the ship after it leaves the visible area.
        if (transform.position.y > topY ||
            transform.position.x < -10f ||
            transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }
}