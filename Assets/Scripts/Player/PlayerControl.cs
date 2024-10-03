using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl: MonoBehaviour
{
    public float speed;
    private float currentHealth;
    Rigidbody2D rb;

    //无敌时间
    public bool IsInvincible { get; private set; } // 无敌状态
    private float invincibleTimer;
    //public Animator animator;

    private void Start()
    {
        currentHealth = Main.MaxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Dying());
    }
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0f);
        //animator.SetFloat("Horizontal",movement.x);
        rb.velocity = new Vector2(movement.x*speed, movement.y*speed);
        print(currentHealth);
        if (IsInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                IsInvincible = false; // 结束无敌状态
            }
        }
    }
    IEnumerator Dying()
    {
        while (currentHealth > 0)
        {
            yield return 0;
        }
        Debug.Log("你挂了！");
    }
    public void ChangeHealth(int value)
    {
        if (!IsInvincible) // 只有在不无敌的情况下才改变生命值
        {
            currentHealth += value; // 更新当前生命值
            currentHealth = Mathf.Clamp(currentHealth, 0, Main.MaxHealth); // 限制生命值在 0 和最大值之
        }
    }
    public void StartInvincibleTime(float duration)
    {
        IsInvincible = true; // 设置无敌状态
        invincibleTimer = duration; // 设置无敌时间
    }

}
