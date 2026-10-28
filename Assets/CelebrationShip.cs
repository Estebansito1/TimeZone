using UnityEngine;

public class CelebrationShip : MonoBehaviour
{
    public float flySpeed = 8f;
    public float topY = 7f;

    void Update()
    {
        transform.position +=
            Vector3.up *
            flySpeed *
            Time.deltaTime;

        if (transform.position.y > topY)
        {
            Destroy(gameObject);
        }
    }
}