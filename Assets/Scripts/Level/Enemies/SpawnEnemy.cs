using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject player, enemyPrefab, wavePrefab;
    public int numEnemies, spawnOffset, spawnSpread, enemiesRemaining;
    public TMP_Text enemyRemainingUI;
    int waveCount;
    private void Start()
    {
        waveCount = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateWave();
        }
    }

    private void CreateWave()
    {
        waveCount++;
        enemiesRemaining = numEnemies;
        enemyRemainingUI.SetText(enemiesRemaining.ToString());
        GameObject wave = GameObject.Instantiate(wavePrefab);
        wave.name = $"Wave{waveCount}";
        Vector3 spawningPosition = new Vector3(0, 0, 0);
        for (int i = 0; i < numEnemies; i++)
        {
            AddEnemy(wave, i);
        }
    }

    private void AddEnemy(GameObject wave, int enemyID)
    {
        GameObject enemy = GameObject.Instantiate(enemyPrefab, wave.transform);
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
            Vector2 currentSpawn = new Vector2(xPos, yPos);
            foreach (Vector2 v in spawnPoints)
            {
                if (v == currentSpawn)
                {
                    continue;
                }
            }
            spawnValidated = true;
            spawnPoints.Add(currentSpawn);
            enemy.transform.position = new Vector3(currentSpawn.x, currentSpawn.y);
            enemy.SetActive(true);
        }
    }
}
