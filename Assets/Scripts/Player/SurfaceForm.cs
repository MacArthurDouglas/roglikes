using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceForm : MonoBehaviour
{
    public float attackDelay=0.8f;
    public bool canFire;
    public float angle = 60f;
    public float expansionSpeed = 0.1f;
    public float maxRadius=5f;
    private float startRadius=3f;
    private void Start()
    {
        canFire = true;
    }
    public void NormalAttack()
    {

        StartCoroutine(AttackCooling());
    }
    private IEnumerator AttackCooling()
    {
        canFire = false;
        yield return new WaitForSeconds(attackDelay);
        canFire = true;
    }
    public void SpecialAttack()
    {

    }
}
