using System.Collections;
using UnityEngine;

public class PlayerControl: MonoBehaviour
{
    public float speed;
    private long currentHealth;
    Rigidbody2D rb;
    public static bool Surface;
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

    private void Start()
    {
        currentHealth = Main.MaxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Dying());
        Surface=true;
        CurrentDirection = new Vector2(0,1);
        CanMove = false;
    }
    void Update()
    {
        Vector3 move = Vector3.zero;
        bool fire=false;
        if (Input.GetKey(moveUpKey))
        {
            move += new Vector3(0, 1, 0); // Ç°½ø
        }

        if (Input.GetKey(moveDownKey))
        {
            move -= new Vector3(0, 1, 0); // ºóÍË
        }

        if (Input.GetKey(moveLeftKey))
        {
            move -= new Vector3(1,0,0); // ×óÒÆ
        }

        if (Input.GetKey(moveRightKey))
        {
            move += new Vector3(1, 0, 0); // ÓÒÒÆ
        }
        Vector3 movement = new Vector3(move.x,move.y, 0f);
        //animator.SetFloat("Horizontal",movement.x);
        if (CanMove)
        {
            rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
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
            CurrentDirection = dirc;
        }
        if (CurrentDirection.x == 0 && CurrentDirection.y == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(CurrentDirection.x == 0 &&CurrentDirection.y == -1){
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (CurrentDirection.x == 1 && CurrentDirection.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else if (CurrentDirection.x == -1 && CurrentDirection.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (CurrentDirection.x == 1 && CurrentDirection.y == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 315);
        }
        else if (CurrentDirection.x == -1 && CurrentDirection.y == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 135);
        }
        else if (CurrentDirection.x == -1 && CurrentDirection.y == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else if (CurrentDirection.x == 1 && CurrentDirection.y == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 225);
        }

        if (fire)
        {
            if (Surface)
            {
                this.GetComponent<SurfaceForm>().NormalAttack();
            }
        }

    }
    IEnumerator Dying()
    {
        while (currentHealth > 0)
        {
            yield return 0;
        }
        Debug.Log("Äã¹ÒÁË£¡");
    }
    public void ChangeHealth(int value)
    {
        currentHealth += value;
        //Debug.Log("oh hurt£¡");
    }
    
}
