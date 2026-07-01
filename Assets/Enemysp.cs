using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, 3f);
    }

    void SpawnEnemy()
    {
        GameObject enemyObj =
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        enemyObj.GetComponent<Enemy>().waypoints = waypoints;
    }
}