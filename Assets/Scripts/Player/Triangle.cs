using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public GameObject player;
    private InnerForm innerForm;
    private Renderer render;
    public float playerDistance = 2f;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        innerForm=player.GetComponent<InnerForm>();
        render=GetComponent<Renderer>();
    }
    void Update()
    {
        
        
        if (innerForm.sprinting)
        {
            this.transform.position = player.transform.position;
            this.transform.eulerAngles=Main.GetEulerAnglesByDirection(PlayerControl.CurrentDirection);
            Vector3 location = new Vector3(0, 0, 0);
            Vector2 unitDirection = SurfaceForm.UnitDirection(PlayerControl.CurrentDirection);
            location.x = this.transform.position.x + unitDirection.x * playerDistance;
            location.y = this.transform.position.y + unitDirection.y * playerDistance;
            location.z = this.transform.position.z;
            this.transform.position = location;
            render.enabled = true;
        }
        else
        {
            render.enabled = false;
        }
    }
}
