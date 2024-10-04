using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InnerForm : MonoBehaviour
{
    private GameObject player;
    public GameObject TriangleObject;
    private PlayerControl playerControl;
    public float Rush_distance=0.4f;
    private GameObject TriangleObject_0;
    public float attackDelay=0.8f;
    private Animator animator;
    private bool canFire;

     void Start()
    {
        player=this.gameObject;
        playerControl=this.GetComponent<PlayerControl>();
        animator=this.GetComponent<Animator>();
        canFire = true;
    }
    public void SpecialAttack()
    {

    }
    public void NormalAttack()
    {
        if (!canFire)
        {
            return;
        }
        animator.SetBool("attacking", true);
        Vector2 atk_direction= PlayerControl.CurrentDirection.normalized;
        TrigCreator(atk_direction);
        PlayerControl.CanMove=false;//关闭速度更改

        player.transform.eulerAngles = new Vector3(atk_direction.x, atk_direction.y, 0);
        Vector3 tem_position = player.transform.position;
        playerControl.rb.velocity= atk_direction * 2*playerControl.speed;//改变人物朝向和速度方向

        while(true){
            if (Vector3.Distance(player.transform.position, tem_position) <0.4f) {
               
                TriangleObject_0.transform.position= player.transform.position + new Vector3(0.012f * atk_direction.x, 0.012f * atk_direction.y, 0);
            }//冲刺4cm三角形跟随人物前进
            else {
                playerControl.rb.velocity /= 2;
                break;
            }    
        }



        StartCoroutine(Cooldown());
    }
    
    private void TrigCreator(Vector2 direction)//创建三角形
    {
        TriangleObject_0 =Instantiate(TriangleObject);
       RectTransform rectTransform_trig=TriangleObject_0.GetComponent<RectTransform>();

       TriangleObject_0.transform.position=player.transform.position+new Vector3(0.012f*direction.x,0.012f*direction.y,0);//设置三角形离玩家距离
       rectTransform_trig.eulerAngles=new Vector3(0,0,Mathf.Atan2(direction.x,direction.y)*Mathf.Rad2Deg+30.0f);//计算玩家方向与x轴正方向的角度，数学推算三角形旋转角度

    }

    private void OnCollisionEnter2D(Collision2D collision)//造成伤害
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    IEnumerator Cooldown()//CD
    {
        canFire= false;
        yield return new WaitForSeconds(attackDelay);
        canFire= true;
    }
}
