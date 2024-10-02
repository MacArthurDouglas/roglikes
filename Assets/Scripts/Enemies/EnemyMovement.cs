using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.05f; // 敌人的移动速度
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // 假设玩家对象有标签 "Player"
    }

    void Update()
    {
        if (player != null)
        {
            // 向玩家移动
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime*0.5f;

            // 检查是否与玩家碰撞
            if (Vector3.Distance(transform.position, player.position) < 1f) // 根据需要调整距离
            {
                Destroy(gameObject); // 碰撞后销毁敌人
            }
        }
    }
}
