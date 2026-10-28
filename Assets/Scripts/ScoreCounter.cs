using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Score")]
    public int score = 0;
    public int pointsPerLevel = 1000;

    [Header("Level Celebration")]
    public GameObject celebrationShipPrefab;
    public float jetSpacing = 1.5f;
    public float jetSpawnY = -4.5f;

    [Header("Sound")]
    public AudioClip levelUpSound;
    public AudioClip weaponUpgradeSound;
    public AudioClip celebrationJetSound;
    public float soundVolume = 1f;

    private int currentLevel = 1;
    private int highScore = 0;

    // Tracks whether the player beat the previous high score this run.
    private bool newHighScore = false;

    void Start()
    {
        if (scoreText == null)
        {
            GameObject scoreGO =
                GameObject.Find("ScoreCounter");

            if (scoreGO != null)
            {
                scoreText =
                    scoreGO.GetComponent<TextMeshProUGUI>();
            }
        }

        if (levelText == null)
        {
            GameObject levelGO =
                GameObject.Find("LevelText");

            if (levelGO != null)
            {
                levelText =
                    levelGO.GetComponent<TextMeshProUGUI>();
            }
        }

        if (finalScoreText == null)
        {
            GameObject finalScoreGO =
                GameObject.Find("FinalScoreText");

            if (finalScoreGO != null)
            {
                finalScoreText =
                    finalScoreGO.GetComponent<TextMeshProUGUI>();
            }
        }

        if (highScoreText == null)
        {
            GameObject highScoreGO =
                GameObject.Find("HighScoreText");

            if (highScoreGO != null)
            {
                highScoreText =
                    highScoreGO.GetComponent<TextMeshProUGUI>();
            }
        }

        // Load the previously saved high score.
        highScore =
            PlayerPrefs.GetInt(
                "HighScore",
                0
            );

        currentLevel = 1;
        newHighScore = false;

        UpdateUI();
        UpdateGameOverScores();
    }

    public void AddScore(int points)
    {
        score += points;

        // If the player beats the saved high score,
        // mark this run as a new record.
        if (score > highScore)
        {
            highScore = score;
            newHighScore = true;

            PlayerPrefs.SetInt(
                "HighScore",
                highScore
            );

            PlayerPrefs.Save();
        }

        int newLevel =
            (score / pointsPerLevel) + 1;

        if (newLevel > currentLevel)
        {
            currentLevel = newLevel;

            StartCoroutine(
                LevelUpCelebration()
            );
        }

        UpdateUI();
        UpdateGameOverScores();
    }

    IEnumerator LevelUpCelebration()
    {
        PlayLevelUpSound();

        yield return new WaitForSeconds(0.4f);

        // Weapon upgrades happen at Level 2 and Level 4.
        if (currentLevel == 2 ||
            currentLevel == 4)
        {
            PlayWeaponUpgradeSound();

            yield return new WaitForSeconds(0.35f);
        }

        PlayCelebrationJetSound();

        StartCoroutine(
            SpawnCelebrationJets()
        );
    }

    void PlayLevelUpSound()
    {
        if (levelUpSound != null)
        {
            AudioSource.PlayClipAtPoint(
                levelUpSound,
                Vector3.zero,
                soundVolume
            );
        }
    }

    void PlayWeaponUpgradeSound()
    {
        if (weaponUpgradeSound != null)
        {
            AudioSource.PlayClipAtPoint(
                weaponUpgradeSound,
                Vector3.zero,
                soundVolume
            );
        }
    }

    void PlayCelebrationJetSound()
    {
        if (celebrationJetSound != null)
        {
            AudioSource.PlayClipAtPoint(
                celebrationJetSound,
                Vector3.zero,
                0.6f
            );
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Score: " +
                score.ToString();
        }

        if (levelText != null)
        {
            levelText.text =
                "Level: " +
                currentLevel.ToString();
        }
    }

    public void UpdateGameOverScores()
    {
        if (finalScoreText != null)
        {
            finalScoreText.text =
                "Final Score: " +
                score.ToString();
        }

        if (highScoreText != null)
        {
            if (newHighScore)
            {
                highScoreText.text =
                    "NEW HIGH SCORE: " +
                    highScore.ToString();
            }
            else
            {
                highScoreText.text =
                    "High Score: " +
                    highScore.ToString();
            }
        }
    }

    IEnumerator SpawnCelebrationJets()
    {
        if (celebrationShipPrefab == null)
        {
            yield break;
        }

        // Level 2 = 1 ship
        // Level 3 = 2 ships
        // Level 4+ = 3 ships
        int jetsToSpawn = 1;

        if (currentLevel == 3)
        {
            jetsToSpawn = 2;
        }
        else if (currentLevel >= 4)
        {
            jetsToSpawn = 3;
        }

        float centerX = 0f;

        if (jetsToSpawn == 1)
        {
            SpawnOneJet(centerX);

            yield break;
        }

        float startX =
            centerX -
            ((jetsToSpawn - 1) *
            jetSpacing / 2f);

        for (int i = 0;
             i < jetsToSpawn;
             i++)
        {
            float x =
                startX +
                (i * jetSpacing);

            SpawnOneJet(x);

            yield return new WaitForSeconds(
                0.15f
            );
        }
    }

    void SpawnOneJet(float x)
    {
        Vector3 spawnPosition =
            new Vector3(
                x,
                jetSpawnY,
                0f
            );

        Instantiate(
            celebrationShipPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}