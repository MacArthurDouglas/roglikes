using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JianQi : MonoBehaviour
{
    public float angle = 60f;
    public float expansionSpeed = 0.1f;
    public float expandRadius = 5f;
    public float startRadius = 6f;
    private float currentRadius;
    [HideInInspector] public Vector2 currentDirection;
    
    public float damage = 10f;
    void Start()
    {

    }
    void Update()
    {
        
    }
}
