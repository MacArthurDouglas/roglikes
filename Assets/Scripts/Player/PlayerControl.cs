using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl: MonoBehaviour
{
    public float speed;
    //无敌时间
    public bool IsInvincible { get; private set; } // 无敌状态
    private float invincibleTimer;
    private int currentHealth;

    public Image[] heartImages; // 通过Inspector关联心形图标
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
    public KeyCode test_specialAttack = KeyCode.E;
    public KeyCode test_switchSurface = KeyCode.Q;
    public static Vector2 CurrentDirection;
    public static bool CanMove;
    public Animator animator;
    private bool moved;
    private InnerForm innerForm;

    private void Start()
    {
        currentHealth = Main.MaxHealth;
        UpdateHeartsUI(5); // 初始化显示5颗心
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Dying());
        Surface=true;
        CurrentDirection = new Vector2(0,1);
        CanMove = true;
        animator = GetComponent<Animator>();
        innerForm = GetComponent<InnerForm>();
        animator.SetBool("dying", false);
        animator.SetBool("resurrection", false);
        animator.SetBool("surface", Surface);
    }
    void Update()
    {
        print(currentHealth);
        moved = false;
        Vector3 move = Vector3.zero;
        bool fire=false;
        //animator.SetBool("surface", Surface);
        if (Input.GetKey(test_specialAttack))
        {
            if (Surface)
            {
                GetComponent<SurfaceForm>().SpecialAttack();
            }
            else
            {
                GetComponent<InnerForm>().SpecialAttack();
            }
        }
        if (Input.GetKey(test_switchSurface))
        {
            Surface = !Surface;
        }
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
    public void UpdateHeartsUI(int remainingHearts)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < remainingHearts)
            {
                heartImages[i].gameObject.SetActive(true); // 显示心形图标
            }
            else
            {
                heartImages[i].gameObject.SetActive(false); // 隐藏心形图标
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
        animator.SetBool("dying", true);
        Debug.Log("你挂了！");
    }
    public void ChangeHealth(int value)
    {
        if (!IsInvincible)
        {
            currentHealth += value;
            currentHealth = Mathf.Clamp(currentHealth, 0, Main.MaxHealth);

            // 计算需要减少的心数
            int heartsLost = Mathf.FloorToInt((Main.MaxHealth - currentHealth) / 40);

            // 确保不超过5颗心
            heartsLost = Mathf.Min(heartsLost, 5);

            Debug.Log("Current Health: " + currentHealth);
            Debug.Log("Hearts Lost: " + heartsLost);

            // 这里可以调用更新UI的方法，显示剩余心数
            UpdateHeartsUI(5 - heartsLost);
        }
        else
        {
            Debug.Log("Player is invincible, health not changed.");
        }
    }
    public void StartInvincibleTime(float duration)
    {
        IsInvincible = true; // 设置无敌状态
        invincibleTimer = duration; // 设置无敌时间
        StartCoroutine(ResetInvincibleState(duration)); // 启动协程
    }
    private IEnumerator ResetInvincibleState(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsInvincible = false; // 重置无敌状态
    }
}
