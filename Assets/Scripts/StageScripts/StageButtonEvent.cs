using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButtonEvent : MonoBehaviour
{
    public void GoToStage1()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToStage2()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToStage3()
    {
        SceneManager.LoadScene(3);
    }
    public void GoToStage4()
    {
        SceneManager.LoadScene(4);
    }
    public void QuitBtnClick()
    {
        Application.Quit();
    }
}
