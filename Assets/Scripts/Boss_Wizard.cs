using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Wizard : MonoBehaviour
{
    public EnemyFSM fsm;
    Transform player;

    List<BossSkill> skills;

    public float inCooldown = 5f;
    public float currentTime = 0;

    public float floatHeight = 10f; //보스가 공중에 뜨는 높이
    public float floatDuration = 1.5f; // 보스가 공중으로 날아오르는 시간

    public GameObject Magic_ball;
    public GameObject Magic_spear;
    public GameObject Magic_rock;
    public Animator anime;
    public Transform Ballport;

    public BossSkill nextSkill;

    public bool isSkill; //스킬을 사용할 수 있는 상태인가

    public bool is_ing; //스킬을 사용 중인가

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        fsm = GetComponent<EnemyFSM>();
        player = fsm.player;

        skills = new List<BossSkill>
        {
            new BossSkill("매직스피어", 20f, 1),
            new BossSkill("매직볼", 30f, 2),
            new BossSkill("매직락", 60f, 3)
        };

        isSkill = false;
        is_ing = false;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        //쿨타임 업데이트
        foreach (BossSkill skill in skills)
        {
            skill.UpdataCooldown(Time.deltaTime);
        }

        nextSkill = null;
        int highestPriority = int.MaxValue;

        //다음에 사용할 스킬 선택
        foreach (BossSkill skill in skills)
        {
            if (skill.IsReady() && skill.priority < highestPriority)
            {
                highestPriority = skill.priority;
                nextSkill = skill;
            }
        }

        if(currentTime >= inCooldown && nextSkill != null)
        {
            isSkill = true;
        }
        else
        {
            isSkill = false;
        }
    }
    public IEnumerator useSkill()
    {
        //스킬 사용
        if (nextSkill != null && nextSkill.IsReady() && currentTime >= inCooldown)
        {
            currentTime = 0;

            yield return StartCoroutine(ExecuteSkill(nextSkill));

            nextSkill = null;
        }
    }

    IEnumerator ExecuteSkill(BossSkill skill)
    {
        if (is_ing != true)
        {
            is_ing = true;
            switch (skill.name)
            {
                case "매직스피어":
                    yield return StartCoroutine(Attack_Magic_spear());
                    break;
                case "매직볼":
                    fsm.agent.enabled = false;
                    yield return StartCoroutine(Attack_Magic_ball());
                    fsm.agent.enabled = true;
                    break;
                case "매직락":
                    yield return StartCoroutine(Attack_Magic_rock());
                    break;
            }
        }

        skill.currentCooldown = skill.cooldown;
        is_ing = false;

        Debug.Log("스킬 사용 : " + skill.name);
    }
    IEnumerator Attack_Magic_spear()
    {
        anime.SetTrigger("StartMagicSpear");
        yield return new WaitForSeconds(0.2f);
        GameObject Spear = Instantiate(Magic_spear, transform.position, transform.rotation);
        Spear.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        anime.SetTrigger("MagicSpearToMove");
    }

    IEnumerator Attack_Magic_ball()
    {
        Vector3 startPos = transform.position;
        anime.SetTrigger("StartMagicBall");
        yield return new WaitForSeconds(0.4f);
        anime.SetTrigger("Fly");
        yield return new WaitForSeconds(0.2f);

        Vector3 targetPos = startPos + Vector3.up * floatHeight; // 원하는 높이로 상승
        float currentTime = 0f;

        while (currentTime < floatDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / floatDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        anime.SetTrigger("Incant_Magic_Ball");
        yield return new WaitForSeconds(0.1f);

        print("공 생성 했음");

        GameObject Ball = Instantiate(Magic_ball, startPos + Vector3.up * 2.3f, Quaternion.identity);
        Ball.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        anime.SetTrigger("Magic_Ball");
        yield return new WaitForSeconds(0.7f);

        anime.SetTrigger("Desending");
        currentTime = 0f;
        while (currentTime < floatDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / floatDuration;
            transform.position = Vector3.Lerp(targetPos, startPos, t);
            yield return null;
        }
        anime.SetTrigger("Landing");
        yield return new WaitForSeconds(0.5f);
        anime.SetTrigger("MagicBallToMove");
    }


    IEnumerator Attack_Magic_rock()
    {
        anime.SetTrigger("StartMagicRock");

        anime.SetTrigger("incant_Magic_rock");
        yield return new WaitForSeconds(0.2f);

        float offsetHeight = 20f; // 플레이어 머리 위로의 오프셋 높이

        Vector3 spawnPosition = player.position + Vector3.up * offsetHeight;

        GameObject Rock = Instantiate(Magic_rock, spawnPosition, Quaternion.identity);
        Rock.SetActive(true);
        Rock.GetComponent<Magic_Rock>().enabled = false; // Magic_Rock 스크립트 비활성화
        Rock.transform.parent = player; // 플레이어의 자식 오브젝트로 설정하여 플레이어와 함께 이동

        float followDuration = 2.0f; // 플레이어 위에 고정되는 시간
        float dropDelay = 1.0f; // 떨어지기 전 딜레이

        yield return new WaitForSeconds(followDuration);

        anime.SetTrigger("Magic_Rock");

        Rock.transform.parent = null;
        Rock.GetComponent<Magic_Rock>().enabled = true; ;
        Rock.GetComponent<Magic_Rock>().StartFalling(); // Magic_Rock이 떨어지도록 호출

        yield return new WaitForSeconds(dropDelay);
        anime.SetTrigger("MagicRockToMove");
    }
}
