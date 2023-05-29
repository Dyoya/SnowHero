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

    // ���� ũ��
    public float jumpPower = 4f;
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
            yVelocity = 0;
            isJumping = false;
        }
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }
        dir.y = yVelocity;  

        cc.Move(dir * speed * Time.deltaTime);
    }
}
