using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject minePrefab;

    [SerializeField]
    private float bulletSpeed;

    private Transform gunPos;

    private PlayerController player;

    private Vector3 shootDirection;

    private GameObject gunObj;
    private bool isGunActive;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        gunPos = transform.Find("GunPos");

        gunObj = transform.Find("Gun").gameObject;
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

            rigid.velocity = shootDirection.normalized * bulletSpeed;
        }
        else
        {
            GameObject mine = Instantiate(minePrefab, gunPos.position, Quaternion.identity);
        }
    }
}
