using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D boxCollider;
    static GameObject instance;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        instance = this.gameObject;
        boxCollider.isTrigger=false;

    }
    public static void Open() { 
        instance.GetComponent<Door>().boxCollider.isTrigger = true;
    }
}
