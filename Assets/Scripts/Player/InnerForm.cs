using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InnerForm : MonoBehaviour
{
    private GameObject player;
    private PlayerControl playerControl;
    public float Rush_distance=0.4f;
    public float attackDelay=0.8f;
    private Animator animator;
    private bool canFire;
    public GameObject innerBurstPrefab;
    
    [HideInInspector]public bool sprinting;//³å´Ì
    public float sprintingTime = 1f;

     void Start()
    {
        player=this.gameObject;
        playerControl=this.GetComponent<PlayerControl>();
        animator=this.GetComponent<Animator>();
        canFire = true;
        sprinting = false;
    }
    
    private void Update()
    {
        if (sprinting)
        {
            transform.position += new Vector3(PlayerControl.CurrentDirection.x, PlayerControl.CurrentDirection.y,0) * playerControl.speed *2* Time.deltaTime;
        }
    }
    public void NormalAttack()
    {
        if (!canFire||sprinting)
        {
            return;
        }
        animator.SetBool("attacking", true);
        StartCoroutine(Sprinting(sprintingTime));
        StartCoroutine(Cooldown());
    }
    public void SpecialAttack()
    {
        Instantiate(innerBurstPrefab);
    }
    IEnumerator Sprinting(float time)
    {
        PlayerControl.CanMove = false;
        sprinting = true;
        yield return new WaitForSeconds(time);
        sprinting=false;
        PlayerControl.CanMove = true;
    }

    IEnumerator Cooldown()//CD
    {
        canFire= false;
        yield return new WaitForSeconds(attackDelay);
        canFire= true;
    }
}
