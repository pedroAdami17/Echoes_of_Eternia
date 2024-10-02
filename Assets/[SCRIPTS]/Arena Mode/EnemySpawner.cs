using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform leftSpawnPoint, rightSpawnPoint; 
    public int maxEnemiesOnScreen = 10;

    private List<GameObject> enemies = new List<GameObject>();
    public EnemyArenaStats arenaStats;

    private int currentWave;

    public void SpawnEnemies(int numberOfEnemies)
    {
        StartCoroutine(SpawnEnemyCoroutine(numberOfEnemies));
    }

    IEnumerator SpawnEnemyCoroutine(int numberOfEnemies)
    {
        // Get available enemies for the current wave
        List<GameObject> availableEnemies = arenaStats.GetAvailableEnemiesForWave(currentWave);

        while (numberOfEnemies > 0)
        {
            if (enemies.Count < maxEnemiesOnScreen)
            {
                SpawnEnemy(availableEnemies);
                numberOfEnemies--;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy(List<GameObject> availableEnemies)
    {
        // Choose random enemy type from availableEnemies and spawn point
        int enemyIndex = Random.Range(0, availableEnemies.Count);
        Transform spawnPoint = (Random.Range(0, 2) == 0) ? leftSpawnPoint : rightSpawnPoint;

        GameObject newEnemy = Instantiate(availableEnemies[enemyIndex], spawnPoint.position, Quaternion.identity);
        enemies.Add(newEnemy);

        // Scale enemy stats for the current wave
        arenaStats.ScaleEnemy(newEnemy.GetComponent<Enemy>(), currentWave);
    }

    public void SetCurrentWave(int wave)
    {
        currentWave = wave;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
