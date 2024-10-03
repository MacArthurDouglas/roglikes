using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceForm : MonoBehaviour
{
    public float attackDelay=0.8f;
    public bool canFire;
    public float angle = 60f;
    public float expansionSpeed = 0.1f;
    public float expandRadius=5f;
    public float startRadius=6f;
    private float currentRadius;

    public float centerDistance = 3f;
    public float damage = 10f;
    private LineRenderer lineRenderer;
    public LayerMask targetLayer;

    private void Start()
    {
        canFire = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 50;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        lineRenderer.useWorldSpace = false;
        targetLayer = 1<<gameObject.layer;//赋值为自己所在的图层
    }
    Vector2 UnitDirection(Vector2 direction)
    {
        Vector2 unitDirection=new Vector2(0,0);
        float distance = direction.magnitude;
        unitDirection.x=direction.x/distance;
        unitDirection.y=direction.y/distance;
        return unitDirection;
    }
    public void NormalAttack()
    {
/*       if (!canFire)
        {
            return;
        }
        Vector2 unitDirection = UnitDirection(PlayerControl.CurrentDirection);
        Vector3 center = new Vector3(0,0,0);
        center.x=this.transform.position.x-unitDirection.x*centerDistance;
        center.y=this.transform.position.y-unitDirection.y*centerDistance;
        center.z = this.transform.position.z;
        Debug.Log("aaa");
        StartCoroutine(AttackCooling());*/
    }
    void SweepDamage(Vector3 center, Vector3 forwardDirection)
    {
        // 检测周围的敌人
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, currentRadius, targetLayer);

        foreach (var collider in hitColliders)
        {
            // 获取敌人相对于玩家的方向
            Vector2 directionToTarget = (collider.transform.position - center).normalized;

            // 判断目标是否在圆弧范围内
            float angleToTarget = Vector2.SignedAngle(forwardDirection, directionToTarget);
            if (Mathf.Abs(angleToTarget) <= angle / 2)
            {
                // 目标在圆弧范围内，造成伤害
                // 在这里调用目标的伤害处理方法，例如：collider.GetComponent<Enemy>().TakeDamage(damage);
                Debug.Log($"Target {collider.name} hit for {damage} damage!");
            }
        }
    }
    
    private IEnumerator AttackCooling()
    {
        canFire = false;
        yield return new WaitForSeconds(attackDelay);
        canFire = true;
    }
    public void SpecialAttack()
    {

    }
}
