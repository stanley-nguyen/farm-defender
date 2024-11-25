using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUI : MonoBehaviour
{
    [SerializeField]
    private Sprite gunUnequipped;

    [SerializeField]
    private Sprite gunEquipped;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetGunEquippedUI()
    {
        spriteRenderer.sprite = gunEquipped;
    }

    public void SetGunUnequippedUI()
    {
        spriteRenderer.sprite = gunUnequipped;
    }
}
