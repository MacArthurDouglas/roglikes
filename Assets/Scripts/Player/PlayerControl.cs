using System.Collections;
using UnityEngine;

public class PlayerControl: MonoBehaviour
{
    public float speed;
    //无敌时间
    public bool IsInvincible { get; private set; } // 无敌状态
    private float invincibleTimer;
    private int currentHealth;
    public Rigidbody2D rb;
    public static bool Surface;//是否是表形态
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode fireUpKey = KeyCode.UpArrow;
    public KeyCode fireDownKey = KeyCode.DownArrow;
    public KeyCode fireLeftKey = KeyCode.LeftArrow;
    public KeyCode fireRightKey = KeyCode.RightArrow;
    public static Vector2 CurrentDirection;
    public static bool CanMove;
    public Animator animator;
    private bool moved;
    private InnerForm innerForm;

    private void Start()
    {
        currentHealth = Main.MaxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Dying());
        Surface=false;
        CurrentDirection = new Vector2(0,1);
        CanMove = true;
        animator = GetComponent<Animator>();
        innerForm = GetComponent<InnerForm>();
    }
    void Update()
    {
        moved = false;
        Vector3 move = Vector3.zero;
        bool fire=false;
        if (Input.GetKey(moveUpKey))
        {
            move += new Vector3(0, 1, 0); // 前进
            moved = true;
        }

        if (Input.GetKey(moveDownKey))
        {
            move -= new Vector3(0, 1, 0); // 后退
            moved = true;
        }

        if (Input.GetKey(moveLeftKey))
        {
            move -= new Vector3(1,0,0); // 左移
            moved = true;
        }

        if (Input.GetKey(moveRightKey))
        {
            move += new Vector3(1, 0, 0); // 右移
            moved = true;
        }
        Vector3 movement = new Vector3(move.x,move.y, 0f);
        if (CanMove)
        {
            transform.position += movement * speed*Time.deltaTime;
            if (moved)
            {
                animator.SetBool("running",true);
            }
            else
            {
                animator.SetBool("running", false);
            }
        }
        if (IsInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                IsInvincible = false; // 结束无敌状态
            }
        }
        Vector2 dirc = new Vector2(0, 0);
        if (Input.GetKey(fireUpKey))
        {
            dirc += new Vector2(0,1) ;
            
            fire = true;
        }
        if (Input.GetKey(fireDownKey))
        {
            dirc += new Vector2(0, -1);
            
            fire = true;
        }
        if (Input.GetKey(fireRightKey))
        {
            dirc += new Vector2(1, 0);
            fire = true;
        }
        if (Input.GetKey(fireLeftKey))
        {
            dirc += new Vector2(-1, 0);
            fire = true;
        }
        if (fire)
        {
            if (Surface || ((!Surface) && !innerForm.sprinting))
            {
                CurrentDirection = dirc;
            }
            
        }
        Vector3 characterScale = transform.localScale;
        if (CurrentDirection.x >= 0)
        {
            if(characterScale.x < 0)
            {
                characterScale.x*=-1;
            }
        }
        else
        {
            if (characterScale.x > 0)
            {
                characterScale.x *= -1;
            }
        }

        transform.localScale = characterScale;

        if (fire)
        {
            
            if (Surface)
            {
                this.GetComponent<SurfaceForm>().NormalAttack();
                if (CanMove)
                {
                    StartCoroutine(DisabledMove(0.1f));
                }
            }
            else
            {
                this.GetComponent<InnerForm>().NormalAttack();
            }
        }
        else
        {
            if (!innerForm.sprinting)
            {
                animator.SetBool("attacking", false);
            }
            
        }
    }
    static IEnumerator DisabledMove(float seconds)
    {
        CanMove = false;
        yield return new WaitForSeconds(seconds);
        CanMove = true;
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
            currentHealth = Mathf.Clamp(currentHealth, 0, Main.MaxHealth);
        }
    }
    public void StartInvincibleTime(float duration)
    {
        IsInvincible = true; // 设置无敌状态
        invincibleTimer = duration; // 设置无敌时间
    }
}
