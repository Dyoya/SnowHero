using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject StageUI;
    public GameObject OptionUI;
    public GameObject UpgradeUI;
    public GameObject KeyControlUI;

    public GameObject ResultUI;

    public GameObject fullStarPrefab;
    public Transform starLayout;

    //public Transform FirstSceneTransform;
    private GameObject gamedata;

    public TMP_Text UIStageText;
    public TMP_Text timerText;
    public TMP_Text remainHpText;
    public void Update()
    {
        if (PlayerPrefs.GetInt("Clear") == 1)
        {
            closeAllUI();
            ResultUI.SetActive(true);
            if (ResultUI.activeSelf)
            {
                gamedata = GameObject.Find("GameData");

                int result_hp = gamedata.GetComponent<GameData>().HP;
                float result_Timer = gamedata.GetComponent<GameData>().Timer;

                int prev_Stage = PlayerPrefs.GetInt("prev_Stage");
                UIStageText.text = "STAGE " + prev_Stage.ToString();
        
                int minutes = Mathf.FloorToInt(result_Timer / 60f);
                int seconds = Mathf.FloorToInt(result_Timer % 60f);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                remainHpText.text = result_hp.ToString();
                int starCount = 1;
                if (result_hp > 10)
                {
                    starCount++;
                }
                if (result_Timer > 60)
                {
                    starCount++;
                }
                for (int i = 0; i < starCount; i++)
                {
                    GameObject star = Instantiate(fullStarPrefab, starLayout);
                }
                int now_star = PlayerPrefs.GetInt("total_star");
                int stage_star = 0;
                if(PlayerPrefs.HasKey("stage_star_" + (prev_Stage - 1).ToString()))
                    stage_star = PlayerPrefs.GetInt("stage_star_" + (prev_Stage - 1).ToString());
           

                
                if(stage_star < starCount)
                {
                    PlayerPrefs.SetInt("stage_star_"+ (prev_Stage - 1).ToString(), starCount);
                    PlayerPrefs.SetInt("total_star", now_star + starCount-stage_star);
                }


                PlayerPrefs.SetInt("Clear", 0);
            }
        }
    }
    public void Start()
    {

        
    }
    // ��� UI�� �ݴ� ���� �Լ��Դϴ�.
    private void closeAllUI()
    {
        MainUI.SetActive(false);
        StageUI.SetActive(false);
        OptionUI.SetActive(false);
        UpgradeUI.SetActive(false);
        KeyControlUI.SetActive(false);
        ResultUI.SetActive(false);
    }

    // ���� ��ŸƮ ��ư �̺�Ʈ �Լ��Դϴ�.
    public void StartButtonClicked()
    {
        closeAllUI();
        StageUI.SetActive(true);
    }
    // ���� ����ȭ������ ���� ��ư �̺�Ʈ �Լ��Դϴ�.
    public void returnToMainButtonClicked()
    {
        closeAllUI();
        MainUI.SetActive(true);
    }
    // Shop ��ư �̺�Ʈ �Լ��Դϴ�.
    public void UpgradeButtonClicked()
    {
        closeAllUI();
        UpgradeUI.SetActive(true);
    }
    // �ɼ� ��ư �̺�Ʈ �Լ��Դϴ�.
    public void OptionButtonClicked()
    {
        closeAllUI();
        OptionUI.SetActive(true);
    }

    public void KeyControlButtonClicked()
    {
        closeAllUI();
        KeyControlUI.SetActive(true);
    }

    // ���� ���� ��ư �̺�Ʈ �Լ��Դϴ�.
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void NextStageButtonClicked()
    {
        int nowstage = PlayerPrefs.GetInt("stage");
        SceneManager.LoadScene(nowstage + 1);
    }
}
