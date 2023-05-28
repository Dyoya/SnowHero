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
    public GameObject ToMainButton; // �������� �̵� ��ư

    // ���â ��ư
    public GameObject NextStageButton; // ���� ���������� �̵� ��ư
    public GameObject ResultToMainButton; // ���â���� �������� �̵� ��ư
    public GameObject RestartButton; // ���� �������� ����� ��ư


    private void Start()
    {
        nowstage = 0;
        PlayerRePosition(nowstage);
        
        UIStage.text = "STAGE : " + nowstage.ToString();
    }
    public void NextStage()
    {

        Debug.Log("���� Ŭ����!");
        ToMainButton.SetActive(false);
        NextStageButton.SetActive(true);
        ResultToMainButton.SetActive(true);
        RestartButton.SetActive(true);

        int temp = 0;
        PlayerPrefs.GetInt("stage", temp);

        if(nowstage > temp)
           PlayerPrefs.SetInt("stage", nowstage); // PlayerPrefs�� stage�� Ŭ������ ������ �������� ����
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
