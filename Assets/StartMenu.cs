using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject startPanel;
    public AudioClip startGameSound;

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Debug.Log("START BUTTON CLICKED");

        Time.timeScale = 1f;

        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

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