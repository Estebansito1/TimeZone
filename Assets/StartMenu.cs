using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject startPanel;

    [Header("Sound")]
    public AudioClip startGameSound;

    void Start()
    {
        // Pause gameplay while the start menu is visible.
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        // Resume gameplay.
        Time.timeScale = 1f;

        // Hide the start menu.
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        // Play the start sound when the game begins.
        if (startGameSound != null)
        {
            AudioSource.PlayClipAtPoint(
                startGameSound,
                Vector3.zero,
                1f
            );
        }
    }
}