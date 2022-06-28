using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab, wavePrefab;
    public GameSettings settings;
    public Player player;
    public int spawnOffset, spawnSpread, enemiesRemaining, enemiesEasy, enemiesMedium, enemiesHard;
    public TMP_Text enemyCountUI, waveNumUI;
    public GameObject eBullets;
    public GameAudio gameAudio;
    int waveCount;
    private void Start()
    {
        waveCount = 0;
        waveNumUI.SetText($"Wave: {waveCount}");
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
                        CreateWave(enemiesEasy);
                        break;
                    case GameSettings.Difficulty.Medium:
                        CreateWave(enemiesMedium);
                        break;
                    case GameSettings.Difficulty.Hard:
                        CreateWave(enemiesHard);
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
        waveNumUI.SetText($"Wave: {waveCount}");
        numEnemies *= waveCount;
        enemiesRemaining = numEnemies;
        enemyCountUI.SetText($"Enemies Remaining: {enemiesRemaining}");
        enemyCountUI.gameObject.SetActive(true);
        GameObject wave = GameObject.Instantiate(wavePrefab, player.transform.position, Quaternion.identity);
        wave.name = $"Wave{waveCount}";
        for (int i = 0; i < numEnemies; i++)
        {
            AddEnemy(wave, i);
        }
        StartCoroutine(gameAudio.audioManager.FadeIn("BossMusic", 10f));
        StartCoroutine(gameAudio.audioManager.FadeOut("BackgroundMusic", 10f));
    }
    private void AddEnemy(GameObject wave, int enemyID)
    {
        GameObject enemy = Instantiate(enemyPrefab, wave.transform);
        enemy.GetComponent<Enemy>().spawner = gameObject.GetComponent<SpawnEnemy>();
        enemy.GetComponent<Enemy>().eBullets = eBullets;
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
        if (enemiesRemaining == 0)
        {
            enemyCountUI.SetText($"Wave Complete");
            StartCoroutine(gameAudio.audioManager.FadeOut("BossMusic", 10f));
            StartCoroutine(gameAudio.audioManager.FadeIn("BackgroundMusic", 10f));
        }
        else
            enemyCountUI.SetText($"Enemies Remaining: {enemiesRemaining}");
    }
}
