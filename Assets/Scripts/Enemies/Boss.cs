using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] fivePoints;
    public GameObject[] eightPoints;
    public int maxHealth;
    public int currentHealth;
    public GameObject tentacle;//触手
    public GameObject selfExplodeMonster;//自爆怪
    public float attackDelay=5f;
    private bool attckCooling;
    private int beingAttackedTimes;
    private int whenTimesSkill;
    private bool usedSkill1;
    private bool usedSkill2;
    private void Start()
    {
        beingAttackedTimes = 0;
        whenTimesSkill = 0;
        usedSkill1=false;
        usedSkill2=false;
    }
    private void Update()
    {
        if (beingAttackedTimes>=4&&beingAttackedTimes<9)
        {
            if (!usedSkill1&&attckCooling==false)
            {
                StartCoroutine(Skill1());
            }
        }
        else if(beingAttackedTimes>=9)
        {
            if (!usedSkill2 && attckCooling == false)
            {
                StartCoroutine(Skill2());
            }
        }
        else {
            if (attckCooling == false)
            {
                StartCoroutine(NormalAttack());
            }
        }
    }
    IEnumerator NormalAttack()
    {
        attckCooling = true;
        int randomPoint;
        for (int i = 0; i < 3; i++)
        {
            randomPoint = Random.Range(0, 8);//返回0-7的整数
            Instantiate(tentacle, eightPoints[randomPoint].transform.position,Quaternion.identity);
        }

        
        yield return new WaitForSeconds(attackDelay);
        attckCooling=false;

    }
    IEnumerator Flying()
    {
        yield return new WaitForSeconds(0.1f);
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
        attckCooling=false;
    }
    IEnumerator Skill2()
    {
        usedSkill2 = true;
        attckCooling = true;

        StartCoroutine(Flying());

        yield return new WaitForSeconds(attackDelay);
        attckCooling = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
