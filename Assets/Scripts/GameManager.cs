using System;
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
    public Transform ResultCamTransform;
    private GameObject gamedata;
    //public GameObject GameClearUI;
    public GameObject Player;


    public float totalTime = 300f;
    public TMP_Text timerText;
    private float currentTime;
    private int monsterCount;

    private void Start()
    {
        gamedata = GameObject.Find("GameData");
        if (nowstage != 0)
        {
            currentTime = totalTime;
            UpdateTimerText();
            InvokeRepeating("UpdateTimer", 1f, 1f); // 1�ʸ��� UpdateTimer �Լ� ȣ��

            UIStage.text = "STAGE " + nowstage.ToString();
        }
        else
        {
            int result = PlayerPrefs.GetInt("result");
            PlayerRePosition(result);
            PlayerPrefs.SetInt("result", 0);
            Debug.Log(gamedata.GetComponent<GameData>().HP);
            Debug.Log(gamedata.GetComponent<GameData>().Timer);
        }

    }


    public void NextStage()
    {
        int temp = 0;
        PlayerPrefs.GetInt("stage", temp);

        if (nowstage > temp)
            PlayerPrefs.SetInt("stage", nowstage); // PlayerPrefs�� stage�� Ŭ������ ������ �������� ����
    }
    private void UpdateTimer() // gameClear���� �˻�� Ÿ�̸� ����
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        monsterCount = monsters.Length;
        remainMonsterText.text = monsterCount.ToString();
        currentTime--;
        UpdateTimerText();
        if (monsterCount <= 0)
        {
            PlayerPrefs.SetInt("prev_Stage", nowstage);
            NextStage();
            PlayerPrefs.SetInt("result", 1);
            gamedata.GetComponent<GameData>().SetTimer(currentTime);
            gamedata.GetComponent<GameData>().SetHP(Player.GetComponent<Player>().hp);
            SceneManager.LoadScene(0);
        }
        if (currentTime <= 0f)
        {
            // Ÿ�̸Ӱ� ����Ǿ��� ���� ����
            CancelInvoke("UpdateTimer");
            Debug.Log("Ÿ�̸� ����");
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
                // Player�� 2�������� ���� ��ǥ�� �̵�
                PlayerPrefs.SetInt("Clear", 1);
                //GameObject.Find("OVRCameraRig").transform.position = ResultCamTransform.position;
                break;
        }    
    }
    public void NextStageBtnClicked()
    {
        if (nowstage == 4) // ������ ���������� ���� ȭ������
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
