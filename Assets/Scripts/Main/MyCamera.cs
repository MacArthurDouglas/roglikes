using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        Vector3 playerLocation = new Vector3(0, 0,0);
        playerLocation.x = player.transform.position.x;
        playerLocation.y = player.transform.position.y;
        playerLocation.z=-10;
        this.transform.position = playerLocation;      
    }
}
