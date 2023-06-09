using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyhealthBar : MonoBehaviour
{
    private EnemyFSM enemyFSM; // EnemyFSM 스크립트의 참조를 받을 변수
    public Image healthBarImage; // HP를 표시할 Image 컴포넌트

    private void Start()
    {
        // EnemyFSM 스크립트의 참조를 얻기 위해 부모 게임오브젝트의 컴포넌트를 가져옴
        enemyFSM = GetComponentInParent<EnemyFSM>();
    }
    // Update is called once per frame
    void Update()
    {


        // Image의 fillAmount를 HP 비율에 맞게 설정
        healthBarImage.fillAmount = enemyFSM.hp / (float)enemyFSM.maxhp;
    }
}


