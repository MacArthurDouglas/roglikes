using System.Collections;
using UnityEngine;

public class EliteEnemyMovement : EnemyMovement
{
    public float specialAbilityCooldown = 5f; // 特殊技能冷却时间
    private float abilityTimer;
    private bool isDashing = false; // 用于判断是否正在冲刺
    private Animator animator1;
    protected override void Start()
    {
        base.Start();
        animator1 = GetComponent<Animator>(); // 获取 Animator 组件
        abilityTimer = specialAbilityCooldown; // 初始化冷却计时器
    }

    protected override void Update()
    {
        base.Update();
        abilityTimer -= Time.deltaTime;

        if (abilityTimer <= 0)
        {
            UseSpecialAbility();
            abilityTimer = specialAbilityCooldown; // 重置冷却计时器
        }
    }
    protected override void MoveTowardsPlayer()
    {
        if (!isDashing)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float stoppingDistance = 2.0f;

            if (distance > stoppingDistance)
            {
                animator1.SetBool("isWalking", true); // 开始行走动画
                transform.position = Vector3.Lerp(transform.position, transform.position + direction * speed * Time.deltaTime, 0.5f);
            }
            else
            {
                animator1.SetBool("isWalking", false); // 停止行走动画
                StartCoroutine(DashTowardsPlayer(direction));
            }
        }
    }
    private IEnumerator DashTowardsPlayer(Vector3 direction)
    {
        isDashing = true;
        float dashSpeed = speed * 2;
        float dashDuration = 0.5f;
        float elapsed = 0;

        while (elapsed < dashDuration)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + direction * dashSpeed * Time.deltaTime, elapsed / dashDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 造成伤害并播放爆炸动画
        if (player != null)
        {
            PlayerControl playerControl = player.GetComponent<PlayerControl>();
            if (playerControl != null && !playerControl.IsInvincible)
            {
                playerControl.ChangeHealth(-20);
                playerControl.StartInvincibleTime(1f);

                // 播放爆炸动画
                animator1.SetTrigger("explode"); // 使用 Trigger 触发爆炸动画
                yield return new WaitForSeconds(21.5f); // 等待动画播放时间
                animator1.SetBool("isDead", true); // 播放死亡动画
                Destroy(gameObject, 5f); // 延迟销毁敌人
            }
        }

        isDashing = false;
    }


    private void UseSpecialAbility()
    {
        // 靠近玩家一段距离后开始蓄力，蓄力结束后进行一次俯冲，若此时玩家在俯冲范围内则受到伤害，怪物消失
        Debug.Log("Elite enemy uses special ability!");
    }
}
