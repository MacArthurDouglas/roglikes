using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource inGame;
    public AudioSource win;
    public AudioSource menu;
    public static AudioSource Win;
    public static AudioSource StaticInGame;
    public string scene;
    private void Awake()
    {
        if (win != null)
        {
            Win = win;
        }
        if(inGame != null)
        {
            StaticInGame = inGame;

        }
        
        
    }
    private void Start()
    {

        if (scene == "Menu")
        {
            menu.loop = true;
            menu.Play();

        }
        else if (scene == "Map2")
        { 
            inGame.loop = true;
            inGame.Play();
        }
    }
        
    
    public static void Winning()
    {
        StaticInGame.Stop();
        Win.Play();
    }
    
}
