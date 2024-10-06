using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Main.FindItemNum++;
            if(Main.FindItemNum >= 3)
            {
                Door.Open();
            }
            Destroy(gameObject);
        }
    }
}
