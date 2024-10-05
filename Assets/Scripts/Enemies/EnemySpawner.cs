using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 普通敌人预制件
    public GameObject eliteEnemyPrefab; // 精英敌人预制件
    public GameObject eliteEnemyPrefab2; // 另一个精英敌人预制件
    public Transform player;
    public float spawnRadius = 20f;
    private Camera mainCamera;
    private int maxEnemies = 8;
    private List<GameObject> enemies = new List<GameObject>(); // 用于跟踪敌人列表

    // 预定义的生成位置
    public List<Transform> spawnPoints; // 存储生成位置的列表

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnEnemies", 1f, 1f);
    }

    void SpawnEnemies()
    {
        // 移除已死亡的敌人
        enemies.RemoveAll(enemy => enemy == null);

        if (enemies.Count < maxEnemies)
        {
            for (int i = 0; i < 5; i++)
            {
                if (enemies.Count >= maxEnemies)
                    break;

                // 随机选择一个生成位置
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                Vector3 spawnPosition = spawnPoint.position;

                GameObject newEnemy;
                float randomValue = Random.Range(0f, 1f);
                if (randomValue < 0.2f) // 5% 概率生成精英怪 1
                {
                    newEnemy = Instantiate(eliteEnemyPrefab, spawnPosition, Quaternion.identity);
                }
                else if (randomValue < 0.2f) // 20% 概率生成精英怪 2（25% - 5% = 20%）
                {
                    newEnemy = Instantiate(eliteEnemyPrefab2, spawnPosition, Quaternion.identity);
                }
                else // 默认生成普通敌人
                {
                    newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                }

                enemies.Add(newEnemy); // 添加新生成的敌人到列表
            }
        }
    }
}
