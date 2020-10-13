using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameManager GM;
    public GameObject HelpObj;

    public void PlayBtn()
    {
        gameObject.SetActive(false);
        GM.StartGame();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void HelpBtn()
    { 
       HelpObj.SetActive(true);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void MainMenuBtn()
    {
        HelpObj.SetActive(false);
    }
}
