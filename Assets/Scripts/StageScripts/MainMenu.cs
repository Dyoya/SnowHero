using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject StageUI;
    public void StartButtonClicked()
    {
        MainUI.SetActive(false);
        StageUI.SetActive(true);
        
        SceneManager.LoadScene(1);
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
