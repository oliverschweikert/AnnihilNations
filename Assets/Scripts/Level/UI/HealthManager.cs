using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Texture2D fullHeart, emptyHeart;
    public RawImage heart;
    public Transform healthBar;
    public CharacterCombat player;
    float offset = 30, heartSize = 50, padding = 15;
    List<RawImage> hearts = new List<RawImage>();
    void Start()
    {
        player.currentHealth = player.totalHealth;
        InitialiseHearts();
    }

    private void InitialiseHearts()
    {
        for (int h = 0; h < player.totalHealth; h++)
        {
            float offsetX = h * (heartSize + padding) + offset;
            Vector3 position = new Vector3(offsetX, healthBar.transform.position.y, 0);
            RawImage uiHeart = GameObject.Instantiate(heart, position, Quaternion.identity, healthBar);
            uiHeart.name = $"Heart {h + 1}";
            uiHeart.texture = fullHeart;
            hearts.Add(uiHeart);
        }
    }

    public void RedrawHearts()
    {
        for (int h = player.currentHealth; h < hearts.Count; h++)
        {
            hearts[h].texture = emptyHeart;
        }
    }
}
