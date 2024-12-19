using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy)
        {
            enemy.health -= 1;
            Destroy(gameObject);
        }
    }
}
