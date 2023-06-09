using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PostProcessVolume m_Volume;
    Vignette m_Vignette;
    public int hp = 8;
    int maxHp = 8;

    public GameObject healBarUIPrefab;
    public Transform healBarParent;

    private Image[] healBars;

    private PlayerMove PlayerMoveCP;
    private PlayerRotate PlayerRotateCP;
    private CamRotate CamRotateCP;
    private SnowballThrower SnowballThrowerCP;

    void Start()
    {
        m_Volume.profile.TryGetSettings(out m_Vignette);
         PlayerMoveCP = GetComponent<PlayerMove>();
        PlayerRotateCP = GetComponent<PlayerRotate>();
        SnowballThrowerCP = GetComponent<SnowballThrower>();
        CamRotateCP = GetComponentInChildren<CamRotate>();
        maxHp += PlayerPrefs.GetInt("Health") * 4; // maxHP를 업그레이드한 만큼 증가시킵니다.
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
            PlayerMoveCP.enabled = false;
            PlayerRotateCP.enabled = false;
            SnowballThrowerCP.enabled = false;
            CamRotateCP.enabled = false;
            SceneManager.LoadScene(0);

        }
        print(hp);
    }
    IEnumerator PlayHitEffect()
    {

        //hitEffect.SetActive(true);
        m_Vignette.intensity.Override(0.8f);
        yield return new WaitForSeconds(0.5f);
        m_Vignette.intensity.Override(0.0f);
        //hitEffect.SetActive(false);
    }
   
}
