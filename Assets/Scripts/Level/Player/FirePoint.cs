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
    float timeSinceFire;
    private void Start()
    {
        timeSinceFire = 0;
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (timeSinceFire >= .2)
            {
                var currentPos = GetComponent<Transform>().position;
                var bulletDirection = (player.crosshair.transform.position - currentPos).normalized;
                var bulletRotation = Math.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
                var newBullet = Instantiate(pBullet, currentPos, Quaternion.Euler(0, 0, (float)bulletRotation), pBullets.transform);
                newBullet.player = player;
                Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletDirection * bulletVelocity;
                timeSinceFire = 0;
            }
        }
        timeSinceFire += Time.deltaTime;
    }
}
