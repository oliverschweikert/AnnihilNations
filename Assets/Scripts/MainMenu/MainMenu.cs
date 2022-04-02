using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float backgroundTransitionDelay = 5f;
    public Animator transition;
    public float transitionTime = 1f;
    public Sprite[] images;
    private int currentImage = 0;
    public Background background;
    public MainMenu menu;
    private void Start()
    {
        InvokeRepeating("CycleBackgroundImages", backgroundTransitionDelay, backgroundTransitionDelay);
    }
    void CycleBackgroundImages()
    {
        if (menu.isActiveAndEnabled)
        {
            if (currentImage == 0)
            {
                Console.WriteLine("Changing to image 2");
                StartCoroutine(ChangeBackgroundImage(images[1]));
                currentImage++;
                Console.WriteLine("Changed to image 2");
            }

            else if (currentImage == 1)
            {
                Console.WriteLine("Changing to image 3");
                StartCoroutine(ChangeBackgroundImage(images[2]));
                currentImage++;
                Console.WriteLine("Changed to image 3");
            }
            else
            {
                Console.WriteLine("Changing to image 1");
                StartCoroutine(ChangeBackgroundImage(images[0]));
                currentImage = 0;
                Console.WriteLine("Changed to image 1");
            }
        }
    }
    IEnumerator ChangeBackgroundImage(Sprite image)
    {
        transition.SetTrigger("Toggle");
        yield return new WaitForSeconds(1);
        background.GetComponent<Image>().sprite = image;
        transition.SetTrigger("Toggle");
    }
    public void ExitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void PlayGame()
    {
        Debug.Log("Starting New Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Started New Game");
    }
}
