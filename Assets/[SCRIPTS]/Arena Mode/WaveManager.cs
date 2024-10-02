using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public float timeBetweenWaves = 5f;  

    public TextMeshProUGUI countdownText;           
    public ScoreManager scoreManager;    
    public EnemySpawner enemySpawner;    

    private int waveNumber = 1;          
    private int enemiesLeftToSpawn;      
    private int enemiesAlive;            

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(2f); 

        while (true)
        {
            yield return StartCoroutine(WaveCountdown());
            StartWave();

            while (enemiesAlive > 0)
            {
                yield return null;
            }

            waveNumber++;  
        }
    }

    IEnumerator WaveCountdown()
    {
        float countdown = timeBetweenWaves;

        while (countdown > 0)
        {
            countdownText.text = "Next wave in: " + Mathf.Ceil(countdown).ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "";
    }

    void StartWave()
    {
        enemiesLeftToSpawn = waveNumber * 2 + 3;
        enemiesAlive = enemiesLeftToSpawn;

        enemySpawner.SpawnEnemies(enemiesLeftToSpawn);
    }

    public void EnemyKilled()
    {
        enemiesAlive--;
        scoreManager.AddScore(100);  
    }
}
