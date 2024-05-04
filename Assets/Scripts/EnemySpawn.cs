using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab to spawn
    public Transform target; // The target (player) transform for enemies to seek
    public float spawnInterval = 3f; // Interval between enemy spawns
    public float enemySpeed = 5f; // Speed of the spawned enemy
    public float nextWayPointDistance = 1f; // Distance threshold to consider reaching a waypoint

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

            // Access the EnemyAI component of the spawned enemy
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();

            // Set the target for the enemy AI to seek
            enemyAI.target = target;

            // Set the speed and waypoint settings for the enemy AI
            enemyAI.speed = enemySpeed;
            enemyAI.nextWayPointDes = nextWayPointDistance;

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}