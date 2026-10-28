using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
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