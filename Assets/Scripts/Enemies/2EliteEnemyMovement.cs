using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : EnemyMovement
{
    public float specialAbilityCooldown = 5f; // 特殊技能冷却时间
    private float abilityTimer;
    // Start is called before the first frame update
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

    private void UseSpecialAbility()
    {
        // 每隔一段时间便向着玩家方向，发射直线缓慢移动的法球，可以被攻击抵消
        Debug.Log("Elite enemy uses special ability!");
    }
}
