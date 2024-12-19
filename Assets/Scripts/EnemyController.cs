using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public int health;

    private AudioManager audioManager;

    private FarmHealth farmHealth;

    private Rigidbody2D rb;

    private Animator anim;

    private Transform player;

    private Transform farm;

    private bool isKnockedBack = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null )
            player = playerController.transform;

        farm = GameObject.FindWithTag("FarmHitbox").transform;

        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

        farmHealth = FindObjectOfType<FarmHealth>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }

        if (!isKnockedBack && player != null) {
            Pursue();
        }
    }

    private void Pursue()
    {
        if (farmHealth.slider.value <= 0) return;

        Vector2 playerChase = new Vector3(player.position.x, player.position.y - 0.25f);
        Vector2 farmChase = new Vector3(farm.position.x, farm.position.y);
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        float farmDist = Vector2.Distance(currentPos, farmChase);
        float playerDist = Vector2.Distance(currentPos, playerChase);

        bool playerPursue = (farmDist > playerDist) ? true : false;

        if (!player.gameObject.activeSelf)
        {
            playerPursue = false;
        }

        Vector2 direction;

        if (playerPursue)
        {
            direction = playerChase - currentPos;
        }
        else
        {
            direction = farmChase - currentPos;
        }

        direction.Normalize();

        if (direction != Vector2.zero)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerObj = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerObj != null)
            {
                playerObj.TakeDamage(1);

                ApplyKnockBack(collision);
            }
        }
        if (collision.gameObject.CompareTag("FarmHitbox"))
        {
            farmHealth.TakeDamage(10);

            ApplyKnockBack(collision);
        }
    }

    private void ResetKnockBack()
    {
        isKnockedBack = false;
    }

    private void ApplyKnockBack(Collision2D collision)
    {
        Vector2 knockbackDir = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
        knockbackDir.Normalize();

        rb.AddForce(knockbackDir * 1000, ForceMode2D.Impulse);
        isKnockedBack = true;

        Invoke(nameof(ResetKnockBack), 1);
    }

    private void Die()
    {
        audioManager.PlaySFXRandomPitch(audioManager.enemyDeath, 0.5f, 1.5f, 0.25f);

        ScoreManager.instance.currentScore += 20;
        Destroy(gameObject);
    }
}
