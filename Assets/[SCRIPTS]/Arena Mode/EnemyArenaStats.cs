using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArenaStats : MonoBehaviour
{
    public EnemySpawner enemySpawner;  // Reference to EnemySpawner to manage enemies
    public List<WaveEnemyInfo> waveEnemies;  // List to control which enemies spawn on which wave

    public void ScaleEnemy(Enemy enemy, int waveNumber)
    {
        // Access the EnemyStats component
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

        // Set the enemy level based on the wave number (or apply a custom formula)
        enemyStats.SetLevel(waveNumber);  // Adjust the level based on wave

        // Additional scaling logic could be added here if needed
    }

    // This method checks which enemies are allowed to spawn for the current wave
    public List<GameObject> GetAvailableEnemiesForWave(int waveNumber)
    {
        List<GameObject> availableEnemies = new List<GameObject>();

        foreach (WaveEnemyInfo waveEnemy in waveEnemies)
        {
            if (waveEnemy.spawnOnWave <= waveNumber)
            {
                availableEnemies.Add(waveEnemy.enemyPrefab);
            }
        }

        return availableEnemies;
    }
}

[System.Serializable]
public class WaveEnemyInfo
{
    public GameObject enemyPrefab;  // The enemy prefab
    public int spawnOnWave;  // The wave at which this enemy becomes available
}
