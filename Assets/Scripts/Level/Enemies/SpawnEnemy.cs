using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab, wavePrefab;
    public GameSettings settings;
    public Player player;
    public int spawnOffset, spawnSpread, enemiesRemaining;
    public TMP_Text enemyRemainingUI;
    int waveCount;
    private void Start()
    {
        waveCount = 0;
        enemiesRemaining = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enemiesRemaining == 0)
                switch (settings.difficulty)
                {
                    case GameSettings.Difficulty.Easy:
                        CreateWave(5);
                        break;
                    case GameSettings.Difficulty.Medium:
                        CreateWave(10);
                        break;
                    case GameSettings.Difficulty.Hard:
                        CreateWave(50);
                        break;
                    default:
                        CreateWave(1);
                        break;
                }
        }
    }
    private void CreateWave(int numEnemies)
    {
        waveCount++;
        numEnemies *= waveCount;
        enemiesRemaining = numEnemies;
        enemyRemainingUI.SetText(enemiesRemaining.ToString());
        GameObject wave = GameObject.Instantiate(wavePrefab, player.transform.position, Quaternion.identity);
        wave.name = $"Wave{waveCount}";
        for (int i = 0; i < numEnemies; i++)
        {
            AddEnemy(wave, i);
        }
    }
    private void AddEnemy(GameObject wave, int enemyID)
    {
        GameObject enemy = Instantiate(enemyPrefab, wave.transform);
        enemy.GetComponent<Enemy>().spawner = gameObject.GetComponent<SpawnEnemy>();
        enemy.name = $"enemy{enemyID + 1}";
        enemy.SetActive(false);
        bool spawnValidated = false;
        List<Vector2> spawnPoints = new List<Vector2>();
        while (!spawnValidated)
        {
            (int, int)[] offsetChoices = new (int, int)[] { (0, spawnOffset), (spawnOffset, 0), (spawnOffset, spawnOffset) };
            int initialXPos = Random.Range(-spawnSpread, spawnSpread);
            int initialYPos = Random.Range(-spawnSpread, spawnSpread);
            (int, int) offset = offsetChoices[Random.Range(0, offsetChoices.Length)];
            int xPos = initialXPos == 0 ? Random.Range(0, 2) == 0 ? initialXPos - offset.Item1 : initialXPos + offset.Item1 : initialXPos < 0 ? initialXPos - offset.Item1 : initialXPos + offset.Item1;
            int yPos = initialYPos == 0 ? Random.Range(0, 2) == 0 ? initialYPos - offset.Item2 : initialYPos + offset.Item2 : initialYPos < 0 ? initialYPos - offset.Item2 : initialYPos + offset.Item2;
            Vector2 currentSpawn = new Vector2(xPos + wave.transform.position.x, yPos + wave.transform.position.y);
            spawnValidated = true;
            foreach (Vector2 v in spawnPoints)
            {
                if (v == currentSpawn)
                {
                    spawnValidated = false;
                }
            }
            spawnPoints.Add(currentSpawn);
            enemy.transform.position = new Vector3(currentSpawn.x, currentSpawn.y);
            enemy.SetActive(true);
        }
    }
    public void UpdateRemaining()
    {
        enemiesRemaining--;
        enemyRemainingUI.SetText(enemiesRemaining.ToString());
    }
}
