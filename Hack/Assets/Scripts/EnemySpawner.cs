using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player; // Pass the player ref to enemies when spawning them
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay = 5f; // Delay in seconds between spawns
    [SerializeField] private Transform spawnPoint; // Transform of the spawn point for this spawner
    [SerializeField] private Transform enemyParent;

    private void Awake(){
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnDelay);
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, enemyParent);
        enemy.GetComponent<Enemy>().Initialize(player);
    }
}
