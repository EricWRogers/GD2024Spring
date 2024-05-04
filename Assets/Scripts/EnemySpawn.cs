using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab to spawn
    public float spawnInterval = 3f; // Interval between enemy spawns

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            // Instantiate a new enemy at the spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}