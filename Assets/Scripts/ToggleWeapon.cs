using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWeapon : MonoBehaviour
{
    [SerializeField]
    private GunUI gunUI;

    [SerializeField]
    private MineUI mineUI;

    private GameObject gunSprite;
    private GameObject mineSprite;

    void Awake()
    {
        gunSprite = transform.Find("Gun").gameObject;
        mineSprite = transform.Find("Landmine").gameObject;

        gunSprite.SetActive(true);
        mineSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGun();
            gunUI.SetGunEquippedUI();
            mineUI.SetMineUnequippedUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetMine();
            gunUI.SetGunUnequippedUI();
            mineUI.SetMineEquippedUI();
        }
    }

    private void SetGun()
    {
        gunSprite.SetActive(true);
        mineSprite.SetActive(false);
    }

    private void SetMine()
    {
        gunSprite.SetActive(false);
        mineSprite.SetActive(true);
    }
}
