using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineUI : MonoBehaviour
{
    [SerializeField]
    private Sprite mineUnequipped;

    [SerializeField]
    private Sprite mineEquipped;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetMineEquippedUI()
    {
        spriteRenderer.sprite = mineEquipped;
    }

    public void SetMineUnequippedUI()
    {
        spriteRenderer.sprite = mineUnequipped;
    }
}
