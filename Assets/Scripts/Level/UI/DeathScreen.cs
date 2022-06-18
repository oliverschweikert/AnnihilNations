using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public CanvasGroup deathScreenUI, uiElements;
    public Player player;
    private void Start()
    {
        deathScreenUI.gameObject.SetActive(false);
        deathScreenUI.interactable = false;
        uiElements.interactable = false;
        uiElements.alpha = 0;
        deathScreenUI.alpha = 0;
    }
    private void Update()
    {
        if (player.dead)
        {
            deathScreenUI.gameObject.SetActive(true);
            deathScreenUI.interactable = true;
            Cursor.visible = true;
            if (deathScreenUI.alpha < .85) deathScreenUI.alpha += (float)(Time.deltaTime / 5);
            else
            {
                uiElements.interactable = true;
                if (uiElements.alpha < 1) uiElements.alpha += Time.deltaTime;
            }
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
