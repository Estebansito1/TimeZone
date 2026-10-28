using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;

    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -3f;
    public float maxY = 4f;

    [Header("Starting Position")]
    public float startX = 0f;
    public float startY = -2.8f;

    [Header("Shooting")]
    public GameObject laserPrefab;
    public float fireDelay = 0.25f;

    [Header("Sound")]
    public AudioClip laserSound;
    public AudioClip playerDamageSound;
    public AudioClip gameOverSound;
    public AudioClip lowLifeWarningSound;

    [Header("Lives")]
    public int lives = 3;
    public int maxLives = 3;
    public float invincibleTime = 1f;

    [Header("Crash Effect")]
    public GameObject fireEffect;
    public float crashTime = 2f;
    public float crashRiseSpeed = 3f;
    public float crashSpinSpeed = 500f;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameObject startPanel;

    private float nextFireTime = 0f;

    private ScoreCounter scoreCounter;
    private TextMeshProUGUI livesText;
    private SpriteRenderer spriteRenderer;
    private Collider playerCollider;
    private AudioSource audioSource;

    private bool isInvincible = false;
    private bool gameOver = false;

    void Start()
    {
        // Place the player at the correct starting position.
        transform.position =
            new Vector3(startX, startY, 0f);

        // Find and cache references used throughout the script.
        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();

        spriteRenderer =
            GetComponent<SpriteRenderer>();

        playerCollider =
            GetComponent<Collider>();

        audioSource =
            GetComponent<AudioSource>();

        // Find the Lives UI text.
        GameObject livesGO =
            GameObject.Find("LivesText");

        if (livesGO != null)
        {
            livesText =
                livesGO.GetComponent<TextMeshProUGUI>();
        }

        // Find the fire effect automatically if it was not assigned.
        if (fireEffect == null)
        {
            Transform fireTransform =
                transform.Find("FireEffect");

            if (fireTransform != null)
            {
                fireEffect =
                    fireTransform.gameObject;
            }
        }

        // Hide effects and Game Over screen at the beginning.
        if (fireEffect != null)
        {
            fireEffect.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateLivesText();
    }

    void Update()
    {
        // Disable player input after Game Over begins.
        if (gameOver)
        {
            return;
        }

        MovePlayer();
        Shoot();
    }

    void MovePlayer()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Horizontal input.
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1f;
        }

        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1f;
        }

        // Vertical input.
        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -1f;
        }

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow))
        {
            vertical = 1f;
        }

        Vector3 movement =
            new Vector3(
                horizontal,
                vertical,
                0f
            );

        // Move at a consistent speed in all directions.
        transform.position +=
            movement.normalized *
            speed *
            Time.deltaTime;

        // Keep the player inside the playable area.
        Vector3 pos =
            transform.position;

        pos.x =
            Mathf.Clamp(
                pos.x,
                minX,
                maxX
            );

        pos.y =
            Mathf.Clamp(
                pos.y,
                minY,
                maxY
            );

        transform.position = pos;
    }

    void Shoot()
    {
        // Z fires, with a delay to control the fire rate.
        if (Input.GetKey(KeyCode.Z) &&
            Time.time >= nextFireTime)
        {
            nextFireTime =
                Time.time + fireDelay;

            PlayLaserSound();

            // Weapon strength increases as the player's level increases.
            int level = 1;

            if (scoreCounter != null)
            {
                level =
                    (scoreCounter.score / 1000) + 1;
            }

            if (level == 1)
            {
                FireSingleShot();
            }
            else if (level == 2 ||
                     level == 3)
            {
                FireDoubleShot();
            }
            else
            {
                FireTripleShot();
            }
        }
    }

    void PlayLaserSound()
    {
        if (audioSource != null &&
            laserSound != null)
        {
            audioSource.PlayOneShot(
                laserSound
            );
        }
    }

    void PlayDamageSound()
    {
        if (audioSource != null &&
            playerDamageSound != null)
        {
            audioSource.PlayOneShot(
                playerDamageSound
            );
        }
    }

    void PlayGameOverSound()
    {
        if (audioSource != null &&
            gameOverSound != null)
        {
            audioSource.PlayOneShot(
                gameOverSound
            );
        }
    }

    void PlayLowLifeWarning()
    {
        if (audioSource != null &&
            lowLifeWarningSound != null)
        {
            audioSource.PlayOneShot(
                lowLifeWarningSound
            );
        }
    }

    void FireSingleShot()
    {
        // Level 1 weapon: one straight laser.
        Vector3 spawnPosition =
            transform.position +
            new Vector3(
                0f,
                1.1f,
                0f
            );

        Instantiate(
            laserPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    void FireDoubleShot()
    {
        // Level 2-3 weapon: two straight lasers.
        Vector3 leftPosition =
            transform.position +
            new Vector3(
                -0.45f,
                1f,
                0f
            );

        Vector3 rightPosition =
            transform.position +
            new Vector3(
                0.45f,
                1f,
                0f
            );

        Instantiate(
            laserPrefab,
            leftPosition,
            Quaternion.identity
        );

        Instantiate(
            laserPrefab,
            rightPosition,
            Quaternion.identity
        );
    }

    void FireTripleShot()
    {
        // Level 4+ weapon: center shot plus two angled side shots.
        Vector3 centerPosition =
            transform.position +
            new Vector3(
                0f,
                1.1f,
                0f
            );

        Vector3 leftPosition =
            transform.position +
            new Vector3(
                -0.45f,
                1f,
                0f
            );

        Vector3 rightPosition =
            transform.position +
            new Vector3(
                0.45f,
                1f,
                0f
            );

        Instantiate(
            laserPrefab,
            centerPosition,
            Quaternion.identity
        );

        Instantiate(
            laserPrefab,
            leftPosition,
            Quaternion.Euler(
                0f,
                0f,
                15f
            )
        );

        Instantiate(
            laserPrefab,
            rightPosition,
            Quaternion.Euler(
                0f,
                0f,
                -15f
            )
        );
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore collisions once the player is already in Game Over.
        if (gameOver)
        {
            return;
        }

        Asteroid asteroid =
            other.GetComponent<Asteroid>();

        // Regular asteroid collisions damage the player.
        if (asteroid != null &&
            !isInvincible)
        {
            LoseLife();

            Destroy(other.gameObject);

            if (!gameOver)
            {
                StartCoroutine(
                    DamageCooldown()
                );
            }
        }
    }

    public void LoseLife()
    {
        lives--;

        // Final life starts the crash and Game Over sequence.
        if (lives <= 0)
        {
            lives = 0;

            UpdateLivesText();

            PlayGameOverSound();

            StartCoroutine(
                CrashSequence()
            );

            return;
        }

        // Use a different sound when the player reaches one life.
        if (lives == 1)
        {
            PlayLowLifeWarning();
        }
        else
        {
            PlayDamageSound();
        }

        UpdateLivesText();
    }

    public void AddLife()
    {
        // Health pickups cannot work after Game Over.
        if (gameOver)
        {
            return;
        }

        // Health cannot increase above the maximum.
        if (lives < maxLives)
        {
            lives++;
        }

        UpdateLivesText();
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text =
                "Lives: " +
                lives.ToString();
        }
    }

    IEnumerator DamageCooldown()
    {
        // Temporarily prevent repeated collision damage.
        isInvincible = true;

        float blinkDelay = 0.15f;
        float elapsedTime = 0f;

        // Blink the player during the invincibility period.
        while (elapsedTime < invincibleTime)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled =
                    !spriteRenderer.enabled;
            }

            yield return new WaitForSeconds(
                blinkDelay
            );

            elapsedTime += blinkDelay;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        isInvincible = false;
    }

    IEnumerator CrashSequence()
    {
        gameOver = true;

        // Disable collisions while the ship performs the crash animation.
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // Turn on the fire particle effect.
        if (fireEffect != null)
        {
            fireEffect.SetActive(true);

            ParticleSystem fireParticles =
                fireEffect.GetComponent<ParticleSystem>();

            if (fireParticles != null)
            {
                fireParticles.Play();
            }
        }

        float timer = 0f;

        // Spin and move the ship upward during the crash.
        while (timer < crashTime)
        {
            transform.Rotate(
                0f,
                0f,
                crashSpinSpeed *
                Time.deltaTime
            );

            transform.position +=
                Vector3.up *
                crashRiseSpeed *
                Time.deltaTime;

            timer += Time.deltaTime;

            yield return null;
        }

        // Hide the Start screen so it does not overlap Game Over.
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        // Show the Game Over screen and freeze gameplay.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}