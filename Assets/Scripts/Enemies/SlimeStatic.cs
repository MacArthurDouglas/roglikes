using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStatic : MonoBehaviour
{
    public GameObject player;
    private float attackDistance = 6f;
    private bool attacking;
    private Animator animator;
    private bool isDead;
    private float dyingDelay = 0.5f;
    private int currentHealth;
    private int maxHealth = 5;
    IEnumerator naturalDeath()
    {
        yield return new WaitForSeconds(17);
        isDead = true;
        animator.SetBool("isDead", isDead);

    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        isDead = false;
        attacking = false;
        currentHealth = maxHealth;
        animator.SetBool("attacking", attacking);
        animator.SetBool("isDead", isDead);
        StartCoroutine(naturalDeath());
    }
    private void Update()
    {
        if (isDead)
        {
            Destroy(gameObject, 0.5f);
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackDistance)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
        animator.SetBool("attacking", attacking);
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
        if (collision.gameObject.tag == "PlayerWeapons")
        {
            currentHealth--;
            if (currentHealth < 0)
            {
                isDead = true;
                animator.SetBool("isDead", isDead);
                Boss.KilledSlimeStatic++;
                if (Boss.KilledSlimeStatic >= 2)
                {
                    Boss.skill2finished = true;
                }
            }


        }
    }
}
