using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionPrefab;

    [SerializeField]
    private float explosionRadius;

    private AudioManager audioManager;

    private PlayerShoot playerShoot;

    private void Awake()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        audioManager.PlaySFX(audioManager.minePlace, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            audioManager.PlaySFX(audioManager.mineExplode, 0.7f);
            Destroy(gameObject);
            playerShoot.mineCount--;

            ParticleSystem explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            DamageRadius(explosionRadius);

            Destroy(explosion.gameObject, explosion.main.duration);
        }
    }

    private void DamageRadius(float radius)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D enemyCollider in enemiesInRange)
        {
            EnemyController enemy = enemyCollider.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.health -= 2;
            }
        }
    }
}
