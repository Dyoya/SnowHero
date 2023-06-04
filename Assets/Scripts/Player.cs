using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp = 20;
    int maxHp = 20;
    public GameObject hitEffect;
    public GameObject GameOverUI;

    public GameObject healBarUIPrefab;
    public Transform healBarParent;

    private Image[] healBars;

    private PlayerMove PlayerMoveCP;
    private PlayerRotate PlayerRotateCP;
    private CamRotate CamRotateCP;
    private SnowballThrower SnowballThrowerCP;

    void Start()
    {
        PlayerMoveCP = GetComponent<PlayerMove>();
        PlayerRotateCP = GetComponent<PlayerRotate>();
        SnowballThrowerCP = GetComponent<SnowballThrower>();
        CamRotateCP = GetComponentInChildren<CamRotate>();
        maxHp += PlayerPrefs.GetInt("Health"); // maxHP를 업그레이한 만큼 증가시킵니다.
        hp = maxHp;
        // HealthBar UI 초기상태입니다.
        healBars = new Image[maxHp];
        for (int i = 0; i < healBars.Length; i++)
        {
            GameObject healBarObject = Instantiate(healBarUIPrefab, healBarParent);
            healBars[i] = healBarObject.GetComponent<Image>();
        }

    }

    private void UpdateHealBarUI()
    {
        for (int i = 0; i < healBars.Length; i++)
        {
            if (i < hp)
                healBars[i].enabled = true;
            else
                healBars[i].enabled = false;
        }
    }

    public void Damage(int damage)
    {
        hp -= damage;

        if(hp > 0)
        {
            StartCoroutine(PlayHitEffect());
            UpdateHealBarUI();
        }
        if(hp < 0)
        {
            hp = 0;
            UpdateHealBarUI();
            GameOverUI.SetActive(true);
            PlayerMoveCP.enabled = false;
            PlayerRotateCP.enabled = false;
            SnowballThrowerCP.enabled = false;
            CamRotateCP.enabled = false;

        }
        print(hp);
    }
    IEnumerator PlayHitEffect()
    {

        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitEffect.SetActive(false);
    }
   
}
