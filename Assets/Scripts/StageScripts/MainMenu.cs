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

    // ��� UI�� �ݴ� ���� �Լ��Դϴ�.
    private void closeAllUI()
    {
        MainUI.SetActive(false);
        StageUI.SetActive(false);
        OptionUI.SetActive(false);
        UpgradeUI.SetActive(false);
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

    // ���� ���� ��ư �̺�Ʈ �Լ��Դϴ�.
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
