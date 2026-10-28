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

        currentLevel = 1;

        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;

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
    }

    IEnumerator LevelUpCelebration()
    {
        PlayLevelUpSound();

        yield return new WaitForSeconds(0.4f);

        // Weapon upgrades only happen
        // at Level 2 and Level 4.
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

    IEnumerator SpawnCelebrationJets()
    {
        if (celebrationShipPrefab == null)
        {
            yield break;
        }

        float centerX = 0f;

        if (jetsPerLevelUp == 1)
        {
            SpawnOneJet(centerX);

            yield break;
        }

        float startX =
            centerX -
            ((jetsPerLevelUp - 1) *
            jetSpacing / 2f);

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