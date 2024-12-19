using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject minePrefab;

    [SerializeField]
    private float bulletSpeed;

    public int maxMines = 5;
    [HideInInspector]
    public int mineCount = 0;

    private AudioManager audioManager;

    private Transform gunPos;

    private PlayerController player;

    private Vector3 shootDirection = new Vector3(0, 1, 0);

    private GameObject gunObj;
    private bool isGunActive;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        gunPos = transform.Find("GunPos");

        gunObj = transform.Find("Gun").gameObject;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (player.aimDirection != Vector3.zero)
        {
            shootDirection = player.aimDirection;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGunActive = gunObj.activeSelf;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (isGunActive)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPos.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            audioManager.PlaySFX(audioManager.gunShot, 0.6f);

            rigid.velocity = shootDirection.normalized * bulletSpeed;

            Destroy(bullet, 2f);
        }
        else
        {
            if (mineCount < 5)
            {
                Instantiate(minePrefab, gunPos.position, Quaternion.identity);
                mineCount++;
            }
        }
    }
}
