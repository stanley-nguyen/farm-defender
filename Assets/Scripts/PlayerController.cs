using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    public float health, maxHealth = 5f;

    public FloatingHealthBar healthBar;
    public Vector3 hpOffset;

    [SerializeField]
    private Vector2 horizontalBounds;

    [SerializeField]
    private Vector2 verticalBounds;

    public Camera cam;

    private AudioManager audioManager;

    private FarmHealth farmHealth;

    public GameOverManager gameOverManager;
    public PlayerRespawn playerRespawn;

    private Rigidbody2D rb2d;
    private Animator anim;
    private DirectionIndicator triangle;

    private float verticalAxis, horizontalAxis;

    public Vector3 aimDirection = new Vector3(0, 1, 0);

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();    
        triangle = GetComponentInChildren<DirectionIndicator>();
        triangle.transform.localPosition = aimDirection / 1.5f;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        farmHealth = FindObjectOfType<FarmHealth>();
        farmHealth.SetMaxHealth(100);

        ScoreManager.instance.currentScore = 0;
        InvokeRepeating(nameof(UpdateScore), 1, 1);
    }

    private void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        healthBar.transform.position = screenPos + hpOffset;

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        float horizontalVel = horizontalAxis * moveSpeed;
        float verticalVel = verticalAxis * moveSpeed;
        rb2d.velocity = new Vector2(horizontalVel, verticalAxis * moveSpeed);
        if (transform.position.x < horizontalBounds.x && horizontalVel < 0 || transform.position.x > horizontalBounds.y && horizontalVel > 0)
            rb2d.velocity = new Vector2(0, verticalVel);

        if (transform.position.y < verticalBounds.x && verticalVel < 0 || transform.position.y > verticalBounds.y && verticalVel > 0)
            rb2d.velocity = new Vector2(horizontalVel, 0);

        aimDirection = new Vector3(horizontalAxis, verticalAxis, 0).normalized;
        if (aimDirection != Vector3.zero)
        {
            if (horizontalAxis != 0 && verticalAxis != 0) aimDirection /= 1.5f;
            else if (horizontalAxis != 0) aimDirection /= 2f;
            else if (verticalAxis != 0) aimDirection /= 1.5f;
            triangle.transform.localPosition = aimDirection;

            float angle = Mathf.Atan2(verticalAxis, horizontalAxis) * Mathf.Rad2Deg - 90f;

            triangle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);

        audioManager.PlaySFX(audioManager.playerHit, 1f);
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void FarmTakeDamage(int damage)
    {
        farmHealth.TakeDamage(damage);
    }

    private void Die()
    {
        gameObject.SetActive(false);
        playerRespawn.ShowRespawn();
    }

    private void UpdateScore()
    {
        ScoreManager.instance.currentScore += 10;
    }
}
