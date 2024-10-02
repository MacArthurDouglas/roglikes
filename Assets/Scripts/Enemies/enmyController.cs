using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRadius = 20f;
    private Camera mainCamera;
    private int maxEnemies = 50;
    private List<GameObject> enemies = new List<GameObject>(); // 用于跟踪敌人列表

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

                Vector3 spawnPosition;
                do
                {
                    spawnPosition = player.position + new Vector3(
                        Random.Range(-spawnRadius, spawnRadius),
                        Random.Range(-spawnRadius, spawnRadius),
                        Random.Range(-spawnRadius, spawnRadius));
                } while (IsVisibleFromCamera(spawnPosition));

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemies.Add(newEnemy); // 添加新生成的敌人到列表
            }
        }
    }

    bool IsVisibleFromCamera(Vector3 position)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, new Bounds(position, Vector3.one));
    }
}
