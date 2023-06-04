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
        InvokeRepeating("UpdateTimer", 1f, 1f); // 1�ʸ��� UpdateTimer �Լ� ȣ��
        //PlayerRePosition(nowstage);

        UIStage.text = "STAGE " + nowstage.ToString();
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
            GameClearUI.SetActive(true);
            NextStage();
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
                // Player�� 1�������� ���� ��ǥ�� �̵�
                break;
            case 2:
                // Player�� 2�������� ���� ��ǥ�� �̵�
                break;
            case 3:
                // Player�� 3�������� ���� ��ǥ�� �̵�
                break;
            case 4:
                // Player�� 4�������� ���� ��ǥ�� �̵�
                break;
            default:
                // ���� ��ǥ default ������ �̵�
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
