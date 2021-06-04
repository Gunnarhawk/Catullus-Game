using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [System.Serializable]
    public class EnemySettings
    {
        [Header("General")]
        public float spawnTime;

        [Header("Movement")]
        public float moveSpeed;
    }
    public EnemySettings enemySettings;

    private float totalTime;

    public GameObject[] enemyPrefab;

    public bool bossOut = false;

    void Update()
    {
        int direction;
        float xSpawn;
        float ySpawn = Random.Range(4.0f, -4.0f);
        int xSpawnLocation = Random.Range(0, 2);
        if(xSpawnLocation == 0)
        {
            xSpawn = -20;
            direction = 1;
        }
        else
        {
            xSpawn = 20;
            direction = -1;
        }

        Vector2 enemySpawnLocation = new Vector2(xSpawn, ySpawn);

        if (totalTime < Time.time && !bossOut)
        {
            SpawnEnemy(direction, enemySpawnLocation);
            totalTime = Time.time + enemySettings.spawnTime;
        }
    }

    void SpawnEnemy(int direction, Vector2 location)
    {
        int randEnemy = Random.Range(0, enemyPrefab.Length);
        Vector2 moveVector = new Vector2(enemySettings.moveSpeed, 0.0f);
        GameObject enemy = Instantiate(enemyPrefab[randEnemy], location, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().AddForce(moveVector * direction);
        Destroy(enemy, 30);
    }
}
