using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 이동 속도
    public float speed = 3;
    float tempSpeed;
    // CharacterController 컴포넌트
    CharacterController cc;

    // 중력 가속도의 크기
    public float gravity = -20;
    // 수직 속도
    float yVelocity = 0;

    // 특수 스킬 정보
    public float skillTime = 3f;
    public int skillLevel;
    public float cooldownSkill = 10f;
    public bool isSkill = false;

    float currentTime;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        currentTime = 0;

        speed *= 1 + (PlayerPrefs.GetInt("MoveSpeed") * 0.2f);

        tempSpeed = speed;
    }
    void Update()
    {
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);

        // 중력
        yVelocity += gravity * Time.deltaTime;

        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        dir.y = yVelocity;

        // 스킬 사용
        skillLevel = PlayerPrefs.GetInt("SpecialSkill");

        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && !isSkill)
        {
            speedUpSkill();
        }

        currentTime += Time.deltaTime;

        if(currentTime > skillTime && isSkill)
        {
            isSkill = false;
            speed = tempSpeed;
            Debug.Log("스킬 종료!");
        }

        cc.Move(dir * speed * Time.deltaTime);
    }
    void speedUpSkill()
    {
        if (skillLevel == 0)
        {
            Debug.Log("스페셜 스킬의 레벨이 1 이상부터 스킬을 사용할 수 있습니다.");
        }
        else if (currentTime > cooldownSkill)
        {
            isSkill = true;
            currentTime = 0;
            tempSpeed = speed;
            speed *= 1 + (0.2f * skillLevel);
            Debug.Log("스킬 사용!");
        }
    }
}
