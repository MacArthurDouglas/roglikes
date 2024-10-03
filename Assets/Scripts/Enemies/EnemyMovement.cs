using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 50; // 敌人的移动速度
    public float detectionRadius = 1.0f; // 检测半径
    public float avoidDistance = 0.5f; // 避让距离

    [SerializeField] private GameObject player;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            AvoidOverlap(); // 添加避让逻辑
        }
        else
        {
            Debug.LogError("PlayerNotFound!");
        }

    }
    private void AvoidOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var collider in colliders)
        {
            if (collider != this && collider.CompareTag("Enemy")) // 确保是其他小怪
            {
                Vector2 direction = (transform.position - collider.transform.position).normalized;
                transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3)direction * avoidDistance, Time.deltaTime);
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // 计算敌人和玩家之间的方向
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // 计算距离
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // 设置一个停止移动的距离
        float stoppingDistance = 1.0f; 

        // 仅在距离大于停止距离时移动
        if (distance > stoppingDistance)
        {
            // 计算移动的目标位置
            Vector3 targetPosition = transform.position + direction * speed * Time.deltaTime;

            // 更新敌人的位置
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        }
    }

    /// <summary>
    /// 碰到玩家的box collider的时候执行
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerControl playerControl = collision.collider.GetComponent<PlayerControl>();
            if (!playerControl.IsInvincible) // 检查玩家是否无敌
            {
                playerControl.ChangeHealth(-10);
                playerControl.StartInvincibleTime(1f); // 启动无敌时间
            }

            // 销毁敌人
            Destroy(gameObject);
        }
    }

    private IEnumerator ResetInvincibleTime(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
