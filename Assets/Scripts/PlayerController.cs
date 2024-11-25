using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float health, maxHealth = 5f;

    [SerializeField]
    private FloatingHealthBar healthBar;

    public GameOverManager gameOverManager;

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
    }

    private void Update()
    {
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
        rb2d.velocity = new Vector2(horizontalAxis * moveSpeed, verticalAxis * moveSpeed);
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
        
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        gameOverManager.ShowGameOver();
    }
}
