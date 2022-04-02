using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float backgroundTransitionDelay = 5f;
    public Animator transition;
    public float transitionTime = 1f;
    public Sprite[] images;
    private int currentImage = 0;
    public Background background;
    private void Start()
    {
        InvokeRepeating("CycleBackgroundImages", backgroundTransitionDelay, backgroundTransitionDelay);
    }
    public void CycleBackgroundImages()
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
    IEnumerator ChangeBackgroundImage(Sprite image)
    {
        transition.SetTrigger("Toggle");
        yield return new WaitForSeconds(1);
        background.GetComponent<Image>().sprite = image;
        transition.SetTrigger("Toggle");
    }
}
