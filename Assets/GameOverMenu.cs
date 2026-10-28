using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
    [Header("Play Again Flash")]
    public TextMeshProUGUI playAgainText;
    public float flashSpeed = 0.5f;

    void OnEnable()
    {
        // Start flashing when the Game Over panel becomes active.
        if (playAgainText != null)
        {
            StartCoroutine(
                FlashPlayAgainText()
            );
        }
    }

    IEnumerator FlashPlayAgainText()
    {
        while (true)
        {
            playAgainText.enabled =
                !playAgainText.enabled;

            yield return new WaitForSecondsRealtime(
                flashSpeed
            );
        }
    }

    public void PlayAgain()
    {
        // Restore normal game speed in case the game was paused.
        Time.timeScale = 1f;

        // Reload the current scene to reset the entire game.
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}