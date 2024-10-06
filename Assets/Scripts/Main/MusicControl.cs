using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource inGame;
    public AudioSource win;
    public static AudioSource Win;
    private void Awake()
    {
        Win = win;
    }
    private void Start()
    {
        
        inGame.loop = true;
        inGame.Play();
    }
    public static void Winning()
    {
        Win.Play();
    }
    
}
