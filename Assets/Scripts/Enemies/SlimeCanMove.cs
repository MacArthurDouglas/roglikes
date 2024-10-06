using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCanMove : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f;
    private float attackDistance=4f;
    private bool attacking;
    private Animator animator;
    private bool isDead;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        isDead = false;
        attacking = false;
        animator.SetBool("attacking", attacking);
        animator.SetBool("isDead", isDead);
    }
    private void Update()
    {
        if (isDead)
        {
            Destroy(gameObject, 0.5f);
            return;
        }
        // 计算怪物到玩家的方向向量
        Vector3 direction = player.transform.position - transform.position;

        // 将方向向量归一化（标准化为单位向量），以确保怪物匀速移动
        direction.Normalize();

        // 根据方向向量和速度移动怪物
        transform.position += direction * speed * Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackDistance)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
        animator.SetBool("attacking",attacking);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            // 获取动画控制器中第 0 层（默认层）的当前动画状态信息
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // 判断当前动画是否是某个特定的动画
            if (stateInfo.IsName("slimeattack"))
            {
                player.GetComponent<PlayerControl>().ChangeHealth(-10);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerWeapons")
        {
            isDead = true;
            animator.SetBool("isDead", isDead);
            Energy.AddEnergy();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
