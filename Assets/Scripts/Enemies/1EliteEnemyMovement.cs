using System.Collections;
using UnityEngine;

public class EliteEnemyMovement : EnemyMovement
{
    public float specialAbilityCooldown = 5f; // 特殊技能冷却时间
    private float abilityTimer;
    private bool isDashing = false; // 用于判断是否正在冲刺

    protected override void Start()
    {
        base.Start(); 
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
        if (!isDashing) // 只有在不冲刺的情况下才进行移动
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float stoppingDistance = 2.0f; // 增加停止距离

            if (distance > stoppingDistance)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + direction * speed * Time.deltaTime, 0.5f);
            }
            else
            {
                StartCoroutine(DashTowardsPlayer(direction)); // 开始冲刺
            }
        }
    }
    private IEnumerator DashTowardsPlayer(Vector3 direction)
    {
        isDashing = true; // 设置为冲刺状态
        yield return new WaitForSeconds(1f); // 等待1秒
        float dashSpeed = speed * 2; // 冲刺时的速度
        float dashDuration = 0.5f; // 冲刺持续时间
        float elapsed = 0;

        while (elapsed < dashDuration)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + direction * dashSpeed * Time.deltaTime, elapsed / dashDuration);
            elapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        isDashing = false; // 恢复为非冲刺状态
    }
    private void UseSpecialAbility()
    {
        // 靠近玩家一段距离后开始蓄力，蓄力结束后进行一次俯冲，若此时玩家在俯冲范围内则受到伤害，怪物消失
        Debug.Log("Elite enemy uses special ability!");
    }
}
