using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MineUI : MonoBehaviour
{
    [SerializeField]
    private Sprite mineUnequipped;

    [SerializeField]
    private Sprite mineEquipped;

    [SerializeField]
    private TextMeshProUGUI mineText;

    private SpriteRenderer spriteRenderer;

    private PlayerShoot playerShoot;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerShoot = FindObjectOfType<PlayerShoot>();
    }

    private void Update()
    {
        if (playerShoot != null)
        {
            mineText.text = playerShoot.mineCount + " / " + playerShoot.maxMines;
        }
    }

    public void SetMineEquippedUI()
    {
        spriteRenderer.sprite = mineEquipped;
        mineText.gameObject.SetActive(true);
    }

    public void SetMineUnequippedUI()
    {
        spriteRenderer.sprite = mineUnequipped;
        mineText.gameObject.SetActive(false);
    }
}
