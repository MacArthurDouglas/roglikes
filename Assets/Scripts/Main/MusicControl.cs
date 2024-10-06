using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource inGame;
    public AudioSource win;
    public static AudioSource Win;
    public static AudioSource StaticInGame;
    private void Awake()
    {
        Win = win;
        StaticInGame = inGame;
    }
    private void Start()
    {

        
        inGame.loop = true;
        inGame.Play();
    }
    public static void Winning()
    {
        StaticInGame.Stop();
        Win.Play();
    }
    
}
