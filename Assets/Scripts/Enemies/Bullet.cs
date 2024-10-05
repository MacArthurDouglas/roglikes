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
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                PlayerControl playerControl = target.GetComponent<PlayerControl>();
                if (playerControl != null && !playerControl.IsInvincible)
                {
                    playerControl.ChangeHealth(-damage);
                    playerControl.StartInvincibleTime(1f); 
                }
                Destroy(gameObject);
            }
        }
    }
}
