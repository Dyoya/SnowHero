using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int nowstage;

    public Text UIStage;
    public GameObject ToMainButton; // 메인으로 이동 버튼

    // 결과창 버튼
    public GameObject NextStageButton; // 다음 스테이지로 이동 버튼
    public GameObject ResultToMainButton; // 결과창에서 메인으로 이동 버튼
    public GameObject RestartButton; // 현재 스테이지 재시작 버튼


    private void Start()
    {
        nowstage = 0;
        PlayerRePosition(nowstage);
        
        UIStage.text = "STAGE : " + nowstage.ToString();
    }
    public void NextStage()
    {

        Debug.Log("게임 클리어!");
        ToMainButton.SetActive(false);
        NextStageButton.SetActive(true);
        ResultToMainButton.SetActive(true);
        RestartButton.SetActive(true);

        int temp = 0;
        PlayerPrefs.GetInt("stage", temp);

        if(nowstage > temp)
           PlayerPrefs.SetInt("stage", nowstage); // PlayerPrefs의 stage에 클리어한 마지막 스테이지 저장
    }
    public void PlayerRePosition(int stage)
    {
        switch(stage)
        {
            case 1:
                // Player를 1스테이지 시작 좌표로 이동
                break;
            case 2:
                // Player를 2스테이지 시작 좌표로 이동
                break;
            case 3:
                // Player를 3스테이지 시작 좌표로 이동
                break;
            case 4:
                // Player를 4스테이지 시작 좌표로 이동
                break;
            default:
                // 시작 좌표 default 값으로 이동
                break;
        }    
    }
    public void NextStageBtnClicked()
    {
        if (nowstage == 4) // 마지막 스테이지면 메인 화면으로
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nowstage + 1);
        }
    }
    public void ToMainBtnClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartBtnClicked()
    {
        SceneManager.LoadScene(nowstage);
    }
}
