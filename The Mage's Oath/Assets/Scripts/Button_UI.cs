using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_UI : MonoBehaviour
{
    public CanvasGroup OptionPanel;
    
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        SceneManager.LoadScene(2);
    }


    public void Player_Menu()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
         SceneManager.LoadScene(0);
    }
   
}
