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
   
    public int maxHearts = 5; // 最大心数
    public int heartValue = 40; // 每颗心对应的生命值
    public Image[] heartImages; // UI中的心形图像数组
    private AudioSource audioSource;
    public AudioClip walkSound; // 引用行走音效
    public AudioClip surfaceAttackSound; // 引用行走音效
    public AudioClip innerAttackSound; // 引用行走音效
    private SurfaceForm surfaceForm;
    private InnerForm innerForm;
    private Vector3 spawnPoint=new Vector3(-2.17f,-32.27f,-1.409f);
    private void Start()
    {
        currentHealth = Main.MaxHealth;
        UpdateHeartUI(); // 初始化心的UI
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Dying());
        Surface=true;
        CurrentDirection = new Vector2(0,1);
        CanMove = true;
        animator = GetComponent<Animator>();
        innerForm = GetComponent<InnerForm>();
        surfaceForm = GetComponent<SurfaceForm>();
        animator.SetBool("dying", false);
        animator.SetBool("resurrection", false);
        animator.SetBool("surface", Surface);
        audioSource = GameObject.Find("AudioManager1").GetComponent<AudioSource>();

    }
    void Update()
    {
        //print(currentHealth);
        moved = false;
        Vector3 move = Vector3.zero;
        bool fire=false;
        animator.SetBool("surface", Surface);
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
            transform.position += movement * speed * Time.deltaTime;
            if (moved)
            {
                animator.SetBool("running", true);
                if (!audioSource.isPlaying) // 检查音效是否正在播放
                {
                    audioSource.PlayOneShot(walkSound);
                }
            }
            else
            {
                animator.SetBool("running", false);
                if (audioSource.isPlaying) // 检查音效是否正在播放
                {
                    audioSource.Stop(); // 停止音效
                }
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
            dirc += new Vector2(0, 1);
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
                if (surfaceForm.active())
                {
                    audioSource.PlayOneShot(surfaceAttackSound); // 播放攻击音效
                    this.GetComponent<SurfaceForm>().NormalAttack();

                    if (CanMove)
                    {
                        StartCoroutine(DisabledMove(0.1f));
                    }
                    animator.SetBool("attacking", true);
                }
                
            }
            else
            {
                if (innerForm.active())
                {
                    audioSource.PlayOneShot(innerAttackSound); // 播放攻击音效
                    this.GetComponent<InnerForm>().NormalAttack();
                    animator.SetBool("attacking", true);
                }
                
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
        animator.SetBool("dying", true);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Reborn());
        
    }
    IEnumerator Reborn() {
        animator.SetBool("dying", false);
        animator.SetBool("resurrection",true );
        transform.position=spawnPoint;

        yield return new WaitForSeconds(0.2f);
        currentHealth=Main.MaxHealth;
        UpdateHeartUI();
        animator.SetBool("resurrection", false);
        StartCoroutine(Dying());
    }
    public void ChangeHealth(int value)
    {

        if (!IsInvincible) // 只有在不无敌的情况下才改变生命值
        {
            currentHealth += value; // 更新当前生命值
            currentHealth = Mathf.Clamp(currentHealth, 0, Main.MaxHealth);

            // 更新心的UI
            UpdateHeartUI();
        }
    }
    private void UpdateHeartUI()
    {
        int currentHearts = maxHearts - (Main.MaxHealth - currentHealth) / heartValue;

        for (int i = 0; i < heartImages.Length; i++)
        {
            // 根据当前心的数量显示或隐藏心形图像
            heartImages[i].enabled = i < currentHearts;
        }
    }
    public void StartInvincibleTime(float duration)
    {
        IsInvincible = true; // 设置无敌状态
        invincibleTimer = duration; // 设置无敌时间
    }
}
