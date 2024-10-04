using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // 法球飞行速度
    private Transform target;
    public int damage = 20; // 法球造成的伤害

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Destroy(gameObject, 5f); // 5秒后销毁法球
    }

    private void Update()
    {
        if (target != null)
        {
            // 朝着目标移动
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 检测碰撞
            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                // 对目标造成伤害
                PlayerControl playerControl = target.GetComponent<PlayerControl>();
                if (playerControl != null&& !playerControl.IsInvincible)
                {
                    playerControl.ChangeHealth(-damage); // 使用 ChangeHealth 方法
                    playerControl.StartInvincibleTime(1f);
                }
                Destroy(gameObject); // 销毁法球
            }
        }
    }
}
