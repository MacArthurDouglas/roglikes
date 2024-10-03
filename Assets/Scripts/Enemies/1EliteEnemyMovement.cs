using System.Collections;
using UnityEngine;

public class EliteEnemyMovement : EnemyMovement
{
    public float specialAbilityCooldown = 5f; // 特殊技能冷却时间
    private float abilityTimer;

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
        // 靠近玩家一段距离后开始蓄力，蓄力结束后进行一次俯冲，若此时玩家在俯冲范围内则受到伤害，怪物消失
        Debug.Log("Elite enemy uses special ability!");
    }
}
