using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public Player player;
    public PBullets pBullets;
    public PBullet pBullet;
    public float bulletVelocity;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var currentPos = GetComponent<Transform>().position;
            var bulletDirection = player.crosshair.transform.position - currentPos;
            var bulletRotation = Math.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            var newBullet = Instantiate(pBullet, currentPos, Quaternion.Euler(0, 0, (float)bulletRotation), pBullets.transform);
            pBullet.player = player;
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletVelocity;
        }
    }
}
