using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnerLoc;
    public GameObject[] enemyPrefabs;
    internal void SpawnEnemy()
    {
        var randomEnemyPrefab = Random.Range(0, enemyPrefabs.Length);
        var enemyPrefab = Instantiate(
            enemyPrefabs[randomEnemyPrefab], 
            new Vector3(spawnerLoc.transform.position.x, spawnerLoc.transform.position.y, -1f) , 
            spawnerLoc.transform.rotation
            );
    }
}
