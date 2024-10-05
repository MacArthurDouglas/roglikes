using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 50; // 敌人的移动速度
    public float detectionRadius = 1.0f; // 检测半径
    public float avoidDistance = 0.5f; // 避让距离
    private Animator animator;
    [SerializeField] protected GameObject player;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>(); // 获取Animator组件
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            AvoidOverlap(); // 添加避让逻辑
            animator.SetBool("isWalking", Vector3.Distance(transform.position, player.transform.position) > 1.0f);
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

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float stoppingDistance = 1.0f;

        if (distance > stoppingDistance)
        {
            // 这里设置为行走状态
            animator.SetBool("isWalking", true);

            Vector3 targetPosition = transform.position + direction * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        }
        else
        {
            // 如果玩家在攻击范围内，设置攻击状态
            animator.SetBool("isAttacking", true);
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
            if (!playerControl.IsInvincible)
            {
                playerControl.ChangeHealth(-10);
                playerControl.StartInvincibleTime(1f);
            }

            // 设置死亡状态
            animator.SetBool("isDead", true);

            // 销毁敌人
            Destroy(gameObject, 1f); // 延迟1秒后销毁，给动画播放时间
        }
    }

    private IEnumerator ResetInvincibleTime(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
