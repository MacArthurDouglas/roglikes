using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : EnemyMovement
{
    public float specialAbilityCooldown = 5f; // 特殊技能冷却时间
    private float abilityTimer;
    // Start is called before the first frame update
    public GameObject bulletPrefab; // 法球Prefab

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
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float stoppingDistance = 6.0f; // 增加停止距离到 3.0f

        if (distance > stoppingDistance)
        {
            // 计算移动的目标位置
            Vector3 targetPosition = transform.position + direction * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        }
    }

    private void UseSpecialAbility()
    {
        // 每隔一段时间便向着玩家方向，发射直线缓慢移动的法球，可以被攻击抵消
        // 发射法球
        Debug.Log("Elite enemy uses special ability!");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
       bulletScript.SetTarget(player.transform); // 设置目标为玩家
    }

}
