using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;

    [Header("Score")]
    public int score = 0;
    public int pointsPerLevel = 1000;

    [Header("Level Celebration")]
    public GameObject celebrationShipPrefab;
    public int jetsPerLevelUp = 3;
    public float jetSpacing = 1.5f;
    public float jetSpawnY = -4.5f;

    [Header("Sound")]
    public AudioClip levelUpSound;
    public AudioClip weaponUpgradeSound;
    public AudioClip celebrationJetSound;
    public float soundVolume = 1f;

    private int currentLevel = 1;

    void Start()
    {
        // Find the score text automatically if it was not assigned
        // in the Inspector.
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

        // Find the level text automatically if needed.
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

        currentLevel = 1;

        UpdateUI();
    }

    public void AddScore(int points)
    {
        // Add points when an asteroid is destroyed.
        score += points;

        // Every pointsPerLevel points increases the level.
        int newLevel =
            (score / pointsPerLevel) + 1;

        // Start the celebration only when entering a new level.
        if (newLevel > currentLevel)
        {
            currentLevel = newLevel;

            StartCoroutine(
                LevelUpCelebration()
            );
        }

        UpdateUI();
    }

    IEnumerator LevelUpCelebration()
    {
        // Play the main level-up sound first.
        PlayLevelUpSound();

        yield return new WaitForSeconds(0.4f);

        // Weapon upgrades happen at Level 2 and Level 4.
        if (currentLevel == 2 ||
            currentLevel == 4)
        {
            PlayWeaponUpgradeSound();

            yield return new WaitForSeconds(0.35f);
        }

        // Play the jet sound before the celebration ships appear.
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
        // Update the score display.
        if (scoreText != null)
        {
            scoreText.text =
                "Score: " +
                score.ToString();
        }

        // Update the level display.
        if (levelText != null)
        {
            levelText.text =
                "Level: " +
                currentLevel.ToString();
        }
    }

    IEnumerator SpawnCelebrationJets()
    {
        // Stop if no celebration prefab was assigned.
        if (celebrationShipPrefab == null)
        {
            yield break;
        }

        float centerX = 0f;

        // Special case for only one celebration ship.
        if (jetsPerLevelUp == 1)
        {
            SpawnOneJet(centerX);

            yield break;
        }

        // Calculate the starting position so the jets are centered.
        float startX =
            centerX -
            ((jetsPerLevelUp - 1) *
            jetSpacing / 2f);

        // Spawn each jet with a short delay between them.
        for (int i = 0;
             i < jetsPerLevelUp;
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