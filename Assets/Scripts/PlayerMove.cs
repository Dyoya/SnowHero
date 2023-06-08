using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed = 5;
    float tempSpeed;
    // CharacterController ������Ʈ
    CharacterController cc;

    // �߷� ���ӵ��� ũ��
    public float gravity = -20;
    // ���� �ӵ�
    float yVelocity = 0;

    // Ư�� ��ų ����
    public float skillTime = 3f;
    public int skillLevel;
    public float cooldownSkill = 10f;
    public bool isSkill = false;

    float currentTime;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        skillLevel = PlayerPrefs.GetInt("SpecialSkill");

        currentTime = 0;

        tempSpeed = speed;
    }
    void Update()
    {
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);

        // �߷�
        yVelocity += gravity * Time.deltaTime;

        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        dir.y = yVelocity;

        // ��ų ���
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && !isSkill)
        {
            speedUpSkill();
        }

        currentTime += Time.deltaTime;

        if(currentTime > skillTime && isSkill)
        {
            isSkill = false;
            speed = tempSpeed;
            Debug.Log("��ų ����!");
        }

        cc.Move(dir * speed * Time.deltaTime);
    }
    void speedUpSkill()
    {
        if (skillLevel == 0)
        {
            Debug.Log("����� ��ų�� ������ 1 �̻���� ��ų�� ����� �� �ֽ��ϴ�.");
        }
        if (currentTime > cooldownSkill)
        {
            isSkill = true;
            currentTime = 0;
            tempSpeed = speed;
            speed *= 1 + (0.2f * skillLevel);
            Debug.Log("��ų ���!");
        }
    }
}
