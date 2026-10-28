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
        transform.position =
            new Vector3(startX, startY, 0f);

        scoreCounter =
            FindFirstObjectByType<ScoreCounter>();

        spriteRenderer =
            GetComponent<SpriteRenderer>();

        playerCollider =
            GetComponent<Collider>();

        audioSource =
            GetComponent<AudioSource>();

        GameObject livesGO =
            GameObject.Find("LivesText");

        if (livesGO != null)
        {
            livesText =
                livesGO.GetComponent<TextMeshProUGUI>();
        }

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

        transform.position +=
            movement.normalized *
            speed *
            Time.deltaTime;

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
        if (Input.GetKey(KeyCode.Z) &&
            Time.time >= nextFireTime)
        {
            nextFireTime =
                Time.time + fireDelay;

            PlayLaserSound();

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
        if (gameOver)
        {
            return;
        }

        Asteroid asteroid =
            other.GetComponent<Asteroid>();

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
        if (gameOver)
        {
            return;
        }

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
        isInvincible = true;

        float blinkDelay = 0.15f;
        float elapsedTime = 0f;

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

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

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

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}