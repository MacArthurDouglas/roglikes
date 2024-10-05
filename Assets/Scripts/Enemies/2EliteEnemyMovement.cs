using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : EnemyMovement
{
    public float specialAbilityCooldown = 5f; // 特殊技能冷却时间
    private float abilityTimer;
    // Start is called before the first frame update
    public GameObject bulletPrefab; // 法球Prefab
    private Animator animator2; // Animator组件
    bool isDead;
    bool isAttacking;
    bool isMoving;

    protected override void Start()
    {

        base.Start();
        abilityTimer = specialAbilityCooldown; // 初始化冷却计时器
        animator2 = GetComponent<Animator>(); // 获取Animator组件

    }
    private void UpdateAnimation()
    {
        // 更新死亡动画
        if (isDead)
        {
            animator2.SetBool("isDead", true);
            return; // 死亡时不再检查其他状态
        }

        // 更新攻击动画
        if (isAttacking)
        {
            animator2.SetBool("isAttacking", true);
            isAttacking = false; // 重置攻击状态
            return; // 攻击时不再检查其他状态
        }

        // 更新移动动画
        if (isMoving)
        {
            animator2.SetBool("isWalking", true);
        }
        else
        {
            animator2.SetBool("isWalking", false);
        }
    }
    protected override void Update()
    {
        base.Update();
        abilityTimer -= Time.deltaTime;
        UpdateAnimation(); // 更新动画状态

        if (abilityTimer <= 0)
        {
            UseSpecialAbility();
            abilityTimer = specialAbilityCooldown; // 重置冷却计时器
        }


    }
    protected override void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float stoppingDistance = 6.0f; // 增加停止距离到 6.0f

        if (distance > stoppingDistance)
        {
            // 计算移动的目标位置
            Vector3 targetPosition = transform.position + direction * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);

            // 设置移动状态
            isMoving = true;
        }
        else
        {
            isMoving = false; // 如果在停止距离内，停止移动
        }
    }

    private void UseSpecialAbility()
    {
        // 发射法球
        Debug.Log("Elite enemy uses special ability!");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(player.transform); // 设置目标为玩家

        // 设置攻击状态
        isAttacking = true;
    }


}
