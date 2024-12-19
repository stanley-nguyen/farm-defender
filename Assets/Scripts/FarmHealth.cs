using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmHealth : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField]
    private GameOverManager gameOverManager;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private PlayerRespawn playerRespawn;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void TakeDamage(int damage)
    {
        slider.value -= damage;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (slider.value <= 0 && playerController)
        {
            Destroy(playerController.gameObject);
            playerRespawn.gameObject.SetActive(false);
            gameOverManager.ShowGameOver();
        }
    }
}
