using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceForm : MonoBehaviour
{
    public float attackDelay=0.8f;
    public bool canFire;
    public float centerDistance = 1f;
    public GameObject jianQiPrefab;

    private void Start()
    {
        canFire = true;
        
    }
    public static Vector2 UnitDirection(Vector2 direction)
    {
        Vector2 unitDirection=new Vector2(0,0);
        float distance = direction.magnitude;
        unitDirection.x=direction.x/distance;
        unitDirection.y=direction.y/distance;
        return unitDirection;
    }
    public void NormalAttack()
    {
        
        if (!canFire)
        {
            return;
        }
        Vector2 unitDirection = UnitDirection(PlayerControl.CurrentDirection);
        Vector3 center = new Vector3(0,0,0);
        center.x=this.transform.position.x-unitDirection.x*centerDistance;
        center.y=this.transform.position.y-unitDirection.y*centerDistance;
        center.z = this.transform.position.z;

        GameObject obj=Instantiate(jianQiPrefab,center,this.transform.rotation);
        obj.GetComponent<JianQi>().currentDirection=PlayerControl.CurrentDirection;
        //Debug.Log("aaa");
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
