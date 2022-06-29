using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings.cs/GameSettings", order = 0)]
public class GameSettings : ScriptableObject
{
    public enum Difficulty { Easy, Medium, Hard };
    public Difficulty difficulty;
    public void DifficultyEasy() { difficulty = Difficulty.Easy; }
    public void DifficultyMedium() { difficulty = Difficulty.Medium; }
    public void DifficultyHard() { difficulty = Difficulty.Hard; }
}
