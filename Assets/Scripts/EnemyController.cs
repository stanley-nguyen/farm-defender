using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private Animator anim;

    private Transform player;

    private bool isKnockedBack = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>().transform;
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack && player != null) {
            Pursue();
        }
    }

    private void Pursue()
    {
        Vector2 PlayerDirection = (player.position - transform.position).normalized;
        if (PlayerDirection != Vector2.zero)
        {
            rb.velocity = PlayerDirection * speed;
            
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerObj = collision.gameObject.GetComponent<PlayerController>();

            if (playerObj != null)
            {
                playerObj.TakeDamage(1);

                Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;

                rb.AddForce(knockbackDir * 1000, ForceMode2D.Impulse);
                isKnockedBack = true;

                Invoke(nameof(ResetKnockBack), 1);
            }
        }
    }

    private void ResetKnockBack()
    {
        isKnockedBack = false;
    }
}
