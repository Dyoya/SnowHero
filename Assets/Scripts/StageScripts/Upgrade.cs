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

    private int moveSpeed = 0;
    private int attackSpeed = 0;
    private int health = 0;
    private int attackPower = 0;
    private int SpecialSkill = 0;

    public const int maxValue = 4; // 최대 값

    void Start()
    {
        LoadStats();
        UpdateUI();
    }

    // 공격력 증가 버튼 이벤트 입니다.
    public void IncreaseMoveSpeed()
    {
        if (moveSpeed < maxValue)
        {
            moveSpeed += 1;
            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseAttackSpeed()
    {
        if (attackSpeed < maxValue)
        {
            attackSpeed += 1;
            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseHealth()
    {
        if (health < maxValue)
        {
            health += 1;
            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseAttackPower()
    {
        if(attackPower < maxValue)
        {
            attackPower += 1;
            SaveStats();
            UpdateUI();
        }
    }

    public void IncreaseSpecialSkill()
    {
        if(SpecialSkill < maxValue)
        {
            SpecialSkill += 1;
            SaveStats();
            UpdateUI();
        }
    }

    private void LoadStats()
    {
        moveSpeed = PlayerPrefs.GetInt("MoveSpeed");
        attackSpeed = PlayerPrefs.GetInt("AttackSpeed");
        health = PlayerPrefs.GetInt("Health");
        attackPower = PlayerPrefs.GetInt("AttackPower");
        SpecialSkill = PlayerPrefs.GetInt("SpecialSkill");
    }

    private void SaveStats()
    {
        PlayerPrefs.SetFloat("MoveSpeed", moveSpeed);
        PlayerPrefs.SetFloat("AttackSpeed", attackSpeed);
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("AttackPower", attackPower);
        PlayerPrefs.SetInt("SpecialSkill", SpecialSkill);
    }
    private void UpdateUI()
    {
        moveSpeedText.text = "(" + moveSpeed.ToString() + "/" + maxValue.ToString() + ")";
        attackSpeedText.text = "(" + attackSpeed.ToString() + "/" + maxValue.ToString() + ")";
        healthText.text = "(" + health.ToString() + "/" + maxValue.ToString() + ")";
        attackPowerText.text = "(" + attackPower.ToString() + "/" + maxValue.ToString() + ")";
        specialSkillText.text = "(" + SpecialSkill.ToString() + "/" + maxValue.ToString() + ")";
    }
}
