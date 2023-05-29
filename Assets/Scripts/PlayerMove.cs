using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed = 5;
    // CharacterController ������Ʈ
    CharacterController cc;

    // �߷� ���ӵ��� ũ��
    public float gravity = -20;
    // ���� �ӵ�
    float yVelocity = 0;

    // ����
    public float jumpPower = 5;
    public bool isJumping = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity += gravity * Time.deltaTime;

        if (cc.isGrounded)
        {
            isJumping = false;
            yVelocity = 0;
        }
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && !isJumping)
        {
            isJumping = true;
            yVelocity = jumpPower;
        }
        dir.y = yVelocity;  

        cc.Move(dir * speed * Time.deltaTime);
    }
}
