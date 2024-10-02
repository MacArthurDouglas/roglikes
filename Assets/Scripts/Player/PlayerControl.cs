using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl: MonoBehaviour
{
    public float speed;
    private long currentHealth;
    Rigidbody2D rb;
    //public Animator animator;
    private void Start()
    {
        currentHealth = Main.MaxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Dying());
    }
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0f);
        //animator.SetFloat("Horizontal",movement.x);
        rb.velocity = new Vector2(movement.x*speed, movement.y*speed);
    }
    IEnumerator Dying()
    {
        while (currentHealth > 0)
        {
            yield return 0;
        }
        Debug.Log("you are dead");
    }
    public void ChangeHealth(int value)
    {
        currentHealth += value;
        //Debug.Log("oh hurt£¡");
    }
}
