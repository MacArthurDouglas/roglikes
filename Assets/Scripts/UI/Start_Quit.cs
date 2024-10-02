using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Quit: MonoBehaviour
{
    public void StartLoadingGame()
    {   
        SceneManager.LoadScene("Main");
    }
    public void Quit(){
        Application.Quit();
    }

}
