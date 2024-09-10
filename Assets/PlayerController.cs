using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip JumpSE;
    private AudioSource aud;

    public float speed = 5f;
    public float jumpForce = 10f;
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    public Image hpGaugeFill; // HPゲージの前景

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;
    private bool moveLeft;
    private bool moveRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
        CurrentHealth = maxHealth;
        UpdateHPGauge();
    }

    void Update()
    {
        float moveInput = 0f;

        if (moveLeft)
        {
            moveInput = -1f;
        }
        else if (moveRight)
        {
            moveInput = 1f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveRight = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveRight = false;
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        float clampedX = Mathf.Clamp(transform.position.x, -7f, 7f);
        transform.position = new Vector2(clampedX, transform.position.y);

        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Jump();
            if (aud != null && JumpSE != null)
            {
                aud.PlayOneShot(JumpSE); // ジャンプ音を再生
            }
        }

        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver Scene");
        }
    }

    public void OnLeftButtonDown()
    {
        moveLeft = true;
    }

    public void OnLeftButtonUp()
    {
        moveLeft = false;
    }

    public void OnRightButtonDown()
    {
        moveRight = true;
    }

    public void OnRightButtonUp()
    {
        moveRight = false;
    }

    public void OnJumpButtonDown()
    {
        if (isGrounded)
        {
            Jump();
            if (aud != null && JumpSE != null)
            {
                aud.PlayOneShot(JumpSE); // ボタンを押したときにジャンプ音を再生
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (!isInvincible)
        {
            int damage = 0;
            switch (collision.gameObject.tag)
            {
                case "Paper":
                    damage = 5;
                    break;
                case "Cabbage":
                    damage = 10;
                    break;
                case "Dog":
                    damage = 20;
                    break;
            }

            if (damage > 0)
            {
                TakeDamage(damage);

                Collider2D collider = collision.collider;
                if (collider != null)
                {
                    Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>(), true);
                    StartCoroutine(ResetCollision(collider));
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        UpdateHPGauge();
        StartCoroutine(InvincibilityCoroutine());
    }

    void UpdateHPGauge()
    {
        hpGaugeFill.fillAmount = (float)CurrentHealth / maxHealth;
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        float elapsedTime = 0f;
        float blinkInterval = 0.1f;
        while (elapsedTime < 2f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    IEnumerator ResetCollision(Collider2D collider)
    {
        yield return new WaitForSeconds(2f);
        if (collider != null && GetComponent<Collider2D>() != null)
        {
            Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>(), false);
        }
    }
}
