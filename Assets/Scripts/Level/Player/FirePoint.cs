using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public Player player;
    public PBullets bullets;
    public GameObject bullet;
    public float disappearDistance;
    public float bulletForce;

    List<GameObject> bulletList;

    private void Start()
    {
        bulletList = new List<GameObject>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var currentPos = GetComponent<Transform>().position;
            var bulletDirection = player.crosshair.transform.position - currentPos;
            var bulletRotation = Math.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            var newBullet = Instantiate(bullet, currentPos, Quaternion.Euler(0, 0, (float)bulletRotation), bullets.transform);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletForce;
            bulletList.Add(newBullet);
        }
        RemoveDistantBullets();
    }

    private void RemoveDistantBullets()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (Vector3.Distance(player.transform.position, bulletList[i].transform.position) > disappearDistance)
            {
                GameObject.Destroy(bulletList[i]);
                bulletList.RemoveAt(i);
            }
        }
    }
}
