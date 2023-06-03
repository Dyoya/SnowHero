using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject StageUI;
    public GameObject OptionUI;
    public GameObject UpgradeUI;

    // 모든 UI를 닫는 내부 함수입니다.
    private void closeAllUI()
    {
        MainUI.SetActive(false);
        StageUI.SetActive(false);
        OptionUI.SetActive(false);
        UpgradeUI.SetActive(false);
    }

    // 게임 스타트 버튼 이벤트 함수입니다.
    public void StartButtonClicked()
    {
        closeAllUI();
        StageUI.SetActive(true);
    }
    // 게임 메인화면으로 가는 버튼 이벤트 함수입니다.
    public void returnToMainButtonClicked()
    {
        closeAllUI();
        MainUI.SetActive(true);
    }
    // Shop 버튼 이벤트 함수입니다.
    public void UpgradeButtonClicked()
    {
        closeAllUI();
        UpgradeUI.SetActive(true);
    }
    // 옵션 버튼 이벤트 함수입니다.
    public void OptionButtonClicked()
    {
        closeAllUI();
        OptionUI.SetActive(true);
    }

    // 게임 종료 버튼 이벤트 함수입니다.
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
