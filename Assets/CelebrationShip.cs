using UnityEngine;

public class CelebrationShip : MonoBehaviour
{
    [Header("Movement")]
    public float flySpeed = 8f;
    public float topY = 7f;

    void Update()
    {
        // Move the celebration ship upward every frame.
        transform.position +=
            Vector3.up *
            flySpeed *
            Time.deltaTime;

        // Remove the ship after it leaves the top of the screen.
        if (transform.position.y > topY)
        {
            Destroy(gameObject);
        }
    }
}