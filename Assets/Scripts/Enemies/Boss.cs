using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] fivePoints;
    public GameObject[] eightPoints;
    public static int MaxHealth=100;
    public static int CurrentHealth; 
    
    public GameObject slimeCanMove;//可以移动的史莱姆
    public GameObject slimeStatic;//不可移动的史莱姆

    public GameObject selfExplodeMonster;//自爆怪
    private GameObject player;
    private float attackDelay=5f;
    private float normalAttackDelay = 10f;
    private bool attckCooling;
    private bool normalAttackCooling;
    private int beingAttackedTimes;
    private int whenTimesSkill;
   
    private bool usedSkill1;
    public GameObject bossHealthBar;
    private float invincibleTime = 0.5f;
    private bool invincible;
    public static bool skill2finished;
    public static int KilledSlimeStatic;
    private Vector3 centerOfMagicCircle=new Vector3(4.96f,18.92f,-2.22f);
    private Coroutine invincibling;//无敌的协程
    private void Start()
    {
        beingAttackedTimes = 0;
        whenTimesSkill = 0;
        
        usedSkill1=false;
        invincible=false;
        skill2finished=true;
        normalAttackCooling= false;
        player = GameObject.FindWithTag("Player");
        CurrentHealth = MaxHealth;
        ShowBossHealth();
        StartCoroutine(NormalAttackCool());
        StartCoroutine(Dying());

    }
    IEnumerator Dying()
    {
        while (true) {
            if (CurrentHealth<=0)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
    public void ShowBossHealth()
    {
        bossHealthBar.SetActive(true);
        
    }
    private void Update()
    {
        //Debug.Log(beingAttackedTimes);
        if (beingAttackedTimes>=5)
        {
            if (!usedSkill1&&attckCooling==false)
            {
                StartCoroutine(Skill1());
                return;
            }
            if (usedSkill1&&skill2finished && attckCooling == false)
            {
                StartCoroutine(Skill2());
                return;
            }
        }
        if (attckCooling == false)
        {
            StartCoroutine(Moving());
        }
    }
    IEnumerator NormalAttackCool() { 
        normalAttackCooling = true;
        yield return new WaitForSeconds(normalAttackDelay);
        normalAttackCooling= false;
    }
    IEnumerator Moving()
    {
        attckCooling = true;
        int randomPoint;
        randomPoint = Random.Range(0, 5);

        transform.position = fivePoints[randomPoint].transform.position;
        if (!normalAttackCooling)
        {
            StartCoroutine(NormalAttack());
        }
        
        yield return new WaitForSeconds(attackDelay);
        attckCooling=false;

    }
    IEnumerator NormalAttack()
    {

        //地板晃动
        yield return new WaitForSeconds(0.5f);
        bool[] visited = new bool[8];
        int random;
        for(int i = 0; i < 3; i++)
        {
            random=Random.Range(0, 8);
            if (!visited[random])
            {
                visited[random]=true;
                Instantiate(slimeCanMove, eightPoints[random].transform.position,Quaternion.identity);

            }
        }
    }
    IEnumerator Skill1()
    {
        usedSkill1 = true;
        attckCooling = true;
        int randomCount=Random.Range(5, 9);
        int randomPoint;
        for (int i = 0; i < randomCount; i++) {
            randomPoint = Random.Range(0, 8);//返回0-7的整数
            Instantiate(selfExplodeMonster, eightPoints[randomPoint].transform.position, Quaternion.identity);
        }


        
        yield return new WaitForSeconds(attackDelay);
        attckCooling = false;
        beingAttackedTimes = 0;
    }
    IEnumerator SummonSlime()
    {
        while (!skill2finished) {
            yield return new WaitForSeconds(4f);
            Instantiate(slimeStatic,player.transform.position,Quaternion.identity);


        }
    }
    IEnumerator Skill2()
    {
 
        attckCooling = true;
        
        KilledSlimeStatic = 0;
        this.transform.position=centerOfMagicCircle;//回到法阵中心
        //StopCoroutine(Invincibling());
        if (invincibling != null)
        {
            StopCoroutine(invincibling);
        }
        invincible = true;//进入无敌
        skill2finished = false;
        StartCoroutine(SummonSlime());
        while (!skill2finished) {
            yield return null;
        }


        usedSkill1 = false;
        beingAttackedTimes = 0;
        invincible =false;
        attckCooling = false;
    }
    void BeingHit(int damage)
    {
        CurrentHealth-= damage;
    }
    IEnumerator Invincibling()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "PlayerWeapons":
                if (!invincible)
                {
                    BeingHit(1);
                    beingAttackedTimes++;
                    invincibling=StartCoroutine(Invincibling());
                    //Debug.Log(CurrentHealth);
                }
                break;
        }
    }
}
