using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings.cs/GameSettings", order = 0)]
public class GameSettings : ScriptableObject
{
    public enum Difficulty { Easy, Medium, Hard };
    public Difficulty difficulty;
    public void DifficultyEasy() { difficulty = Difficulty.Easy; Debug.Log(difficulty.ToString()); }
    public void DifficultyMedium() { difficulty = Difficulty.Medium; Debug.Log(difficulty.ToString()); }
    public void DifficultyHard() { difficulty = Difficulty.Hard; Debug.Log(difficulty.ToString()); }
}
