using UnityEngine;

public class SupportCode : MonoBehaviour
{
    [Header("Support Ship")]
    public GameObject supportShipPrefab;

    [Header("Spawn Positions")]
    public float spawnY = -3.5f;
    public float leftX = -3f;
    public float centerX = 0f;
    public float rightX = 3f;

    private int codeStep = 0;

    void Update()
    {
        CheckSupportCode();
    }

    void CheckSupportCode()
    {
        // Step 1: Up
        if (codeStep == 0 &&
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            codeStep = 1;
            return;
        }

        // Step 2: Up
        if (codeStep == 1 &&
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            codeStep = 2;
            return;
        }

        // Step 3: Down
        if (codeStep == 2 &&
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            codeStep = 3;
            return;
        }

        // Step 4: Down
        if (codeStep == 3 &&
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            ActivateSupportShips();

            codeStep = 0;
            return;
        }

        // Wrong arrow key resets the sequence.
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            codeStep = 0;
        }
    }

    void ActivateSupportShips()
    {
        if (supportShipPrefab == null)
        {
            return;
        }

        SpawnSupportShip(leftX);
        SpawnSupportShip(centerX);
        SpawnSupportShip(rightX);
    }

    void SpawnSupportShip(float x)
    {
        Vector3 spawnPosition =
            new Vector3(
                x,
                spawnY,
                0f
            );

        Instantiate(
            supportShipPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}