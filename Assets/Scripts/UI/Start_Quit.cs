using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Quit: MonoBehaviour
{
/*    private Button Button_start;
    private Button Button_quit;
    private Button Button_quit_1;
    private Button Button_backtoS;
    GameObject canves_start;
    GameObject canves_quit;*/

/*    public void Start()
    {   canves_start =GameObject.Find("Canvas_Start");
        Button_start = canves_start.transform.Find("startBtn").GetComponent<Button>();
        Button_quit = canves_start.transform.Find("quitBtn").GetComponent<Button>();
        Button_start.onClick.AddListener(StartLoadingGame);
        Button_quit.onClick.AddListener(Quit);

        canves_quit = GameObject.Find("Canvas_quit");
        canves_quit.SetActive(false);
        Button_quit_1= canves_quit.transform.Find("Quit_game").GetComponent<Button>();
        Button_backtoS= canves_quit.transform.Find("BackToStartUI").GetComponent<Button>();
        Button_quit_1.onClick.AddListener(Quit);
        Button_backtoS.onClick.AddListener(()=>
        {
            canves_quit.SetActive(false) ;
            canves_start.SetActive(true);
        });

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { canves_quit.SetActive(true); }
    }*/
    public void StartLoadingGame()
    {   
        //canves_start.SetActive(false);
        SceneManager.LoadScene("Main");
    }
    public void Quit(){
        //canves_start.SetActive(false);
        //canves_quit.SetActive(false);
        Application.Quit();
    }

}
