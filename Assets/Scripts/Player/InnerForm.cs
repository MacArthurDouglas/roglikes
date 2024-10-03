using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static UnityEngine.UI.Image;

public class InnerForm : MonoBehaviour
{
    public GameObject Player;
    public GameObject TriangleObject;
    public PlayerControl PlayerControl;
    public Texture2D triangleTexture;
    public float Rush_distance=0.4f;
    private GameObject TriangleObject_0;
    public float CD;

     void Start()
    {
        Player = GameObject.FindWithTag("player");
      
        if (Player == null)
        {
           
        }
    }
    private void Update()
    {
       
    }

    public void SpecialAttack()
    {

    }
    public void NormalAttack(Vector2 direction)
    {
       Vector2 atk_direction= direction.normalized;
       TrigCreator(atk_direction);
       PlayerControl.CanMove=false;//关闭速度更改

       Player.transform.eulerAngles = new Vector3(atk_direction.x, atk_direction.y, 0);
        Vector3 tem_position = Player.transform.position;
        PlayerControl.rb.velocity= atk_direction * 2*PlayerControl.speed;//改变人物朝向和速度方向

        while(true){
            if (Vector3.Distance(Player.transform.position, tem_position) <0.4f) {
               
                TriangleObject_0.transform.position= Player.transform.position + new Vector3(0.012f * atk_direction.x, 0.012f * atk_direction.y, 0);
            }//冲刺4cm三角形跟随人物前进
            else {
                PlayerControl.rb.velocity /= 2;
                break;
            }    
        }



        StartCoroutine(Cooldown());
    }
    
    private void TrigCreator(Vector2 direction)//创建三角形
    {
        TriangleObject_0 =Instantiate(TriangleObject);
       RectTransform rectTransform_trig=TriangleObject_0.GetComponent<RectTransform>();

       TriangleObject_0.transform.position=Player.transform.position+new Vector3(0.012f*direction.x,0.012f*direction.y,0);//设置三角形离玩家距离
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
        yield return new WaitForSeconds(0.8f);
    }
}
