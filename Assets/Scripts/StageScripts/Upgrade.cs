using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public TMP_Text moveSpeedText;
    public TMP_Text attackSpeedText;
    public TMP_Text healthText;
    public TMP_Text attackPowerText;
    public TMP_Text specialSkillText;

    public TMP_Text totalStarText;

    private int moveSpeed = 0;
    private int attackSpeed = 0;
    private int health = 0;
    private int attackPower = 0;
    private int SpecialSkill = 0;
    private int totalStar = 0;
    public const int maxValue = 4; // �ִ� ��

    void Start()
    {
        LoadStats();
        UpdateUI();
    }

    // ���ݷ� ���� ��ư �̺�Ʈ �Դϴ�.
    public void IncreaseMoveSpeed()
    {
        if (moveSpeed < maxValue)
        {
            moveSpeed += 1;
            totalStar -= 1;
            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseAttackSpeed()
    {
        if (attackSpeed < maxValue)
        {
            attackSpeed += 1;
            totalStar -= 1;

            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseHealth()
    {
        if (health < maxValue)
        {
            health += 1;
            totalStar -= 1;
            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseAttackPower()
    {
        if(attackPower < maxValue)
        {
            attackPower += 1;
            totalStar -= 1;

            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseSpecialSkill()
    {
        if(SpecialSkill < maxValue)
        {
            SpecialSkill += 1;
            totalStar -= 1;

            SaveStats();
            UpdateUI();
        }
    }

    private void LoadStats()
    {
        totalStar = PlayerPrefs.GetInt("total_star");

        moveSpeed = PlayerPrefs.GetInt("MoveSpeed");
        attackSpeed = PlayerPrefs.GetInt("AttackSpeed");
        health = PlayerPrefs.GetInt("Health");
        attackPower = PlayerPrefs.GetInt("AttackPower");
        SpecialSkill = PlayerPrefs.GetInt("SpecialSkill");
    }

    private void SaveStats()
    {
        PlayerPrefs.SetInt("total_star", totalStar);
        PlayerPrefs.SetInt("MoveSpeed", moveSpeed);
        PlayerPrefs.SetInt("AttackSpeed", attackSpeed);
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("AttackPower", attackPower);
        PlayerPrefs.SetInt("SpecialSkill", SpecialSkill);
    }
    private void UpdateUI()
    {
        totalStarText.text = totalStar.ToString();
        moveSpeedText.text = "(" + moveSpeed.ToString() + "/" + maxValue.ToString() + ")";
        attackSpeedText.text = "(" + attackSpeed.ToString() + "/" + maxValue.ToString() + ")";
        healthText.text = "(" + health.ToString() + "/" + maxValue.ToString() + ")";
        attackPowerText.text = "(" + attackPower.ToString() + "/" + maxValue.ToString() + ")";
        specialSkillText.text = "(" + SpecialSkill.ToString() + "/" + maxValue.ToString() + ")";
    }
}
