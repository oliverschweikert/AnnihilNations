using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public GameSettings gameSettings;
    private void Start()
    {
        gameSettings.difficulty = GameSettings.Difficulty.Easy;
    }
}
