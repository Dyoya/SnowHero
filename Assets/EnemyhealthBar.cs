using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyhealthBar : MonoBehaviour
{
    private EnemyFSM enemyFSM; // EnemyFSM ��ũ��Ʈ�� ������ ���� ����
    public Image healthBarImage; // HP�� ǥ���� Image ������Ʈ

    private void Start()
    {
        // EnemyFSM ��ũ��Ʈ�� ������ ��� ���� �θ� ���ӿ�����Ʈ�� ������Ʈ�� ������
        enemyFSM = GetComponentInParent<EnemyFSM>();
    }
    // Update is called once per frame
    void Update()
    {


        // Image�� fillAmount�� HP ������ �°� ����
        healthBarImage.fillAmount = enemyFSM.hp / (float)enemyFSM.maxhp;
    }
}


