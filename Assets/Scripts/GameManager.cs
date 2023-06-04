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

    public TMP_Text UIStage;
    public TMP_Text remainMonsterText;

    public GameObject GameClearUI;


    public float totalTime = 180f;
    public TMP_Text timerText;
    private float currentTime;
    private int monsterCount;

    private void Start()
    {
        currentTime = totalTime;
        UpdateTimerText();
        InvokeRepeating("UpdateTimer", 1f, 1f); // 1초마다 UpdateTimer 함수 호출
        //PlayerRePosition(nowstage);

        UIStage.text = "STAGE " + nowstage.ToString();
    }


    public void NextStage()
    {
        int temp = 0;
        PlayerPrefs.GetInt("stage", temp);

        if (nowstage > temp)
            PlayerPrefs.SetInt("stage", nowstage); // PlayerPrefs의 stage에 클리어한 마지막 스테이지 저장
    }
    private void UpdateTimer() // gameClear조건 검사와 타이머 역할
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        monsterCount = monsters.Length;

        remainMonsterText.text = monsterCount.ToString();
        currentTime--;
        UpdateTimerText();
        if (monsterCount <= 0)
        {
            GameClearUI.SetActive(true);
            NextStage();
        }
        if (currentTime <= 0f)
        {
            // 타이머가 종료되었을 때의 동작
            CancelInvoke("UpdateTimer");
            Debug.Log("타이머 종료");
        }
    }
    private void UpdateTimerText()
    {

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
