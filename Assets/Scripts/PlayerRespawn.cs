using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    private int respawnTime = 5;
    private int count = 1;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Update()
    {
        if (player == null) return;

        if (count != 0)
        {
            timerText.text = (respawnTime - count).ToString();
        }

        if (!player.gameObject.activeSelf && count == 1)
        {
            InvokeRepeating(nameof(IncrementCount), 0, 1);
        }

        if (count >= respawnTime)
        {
            player.gameObject.SetActive(true);
            player.health = player.maxHealth;
            player.healthBar.UpdateHealthBar(player.health, player.maxHealth);
            CancelInvoke(nameof(IncrementCount));
            count = 1;
            gameObject.SetActive(false);
        }
    }

    private void IncrementCount()
    {
        count++;
    }

    public void ShowRespawn()
    {
        gameObject.SetActive(true);
    }
}
