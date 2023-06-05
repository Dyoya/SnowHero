using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Wizard : MonoBehaviour
{
    public EnemyFSM fsm;
    Transform player;

    List<BossSkill> skills;

    public float inCooldown = 5f;
    public float currentTime;
    public float floatHeight = 10f; //보스가 공중에 뜨는 높이
    public float floatDuration = 1.5f; // 보스가 공중으로 날아오르는 시간
    public GameObject Magic_ball;
    public GameObject Magic_spear;
    public GameObject Magic_rock;
    private Coroutine coroutine;
    public Animator anime;
    public Transform Ballport;

    BossSkill nextSkill;

    public bool isSkill;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        fsm = GetComponent<EnemyFSM>();
        player = fsm.player;

        skills = new List<BossSkill>
        {
            new BossSkill("매직스피어", 6f, 1),
            new BossSkill("매직볼", 15f, 2),
            new BossSkill("매직락", 30f, 3)
        };

        isSkill = false;
        StartCoroutine(Attack_Magic_ball());
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
    }
    public void useSkill()
    {
        //스킬 사용
        if (nextSkill != null && currentTime > 5f)
        {
            isSkill = true;
            currentTime = 0;

            ExecuteSkill(nextSkill);
            nextSkill.currentCooldown = nextSkill.cooldown;
            isSkill = false;
        }
    }

    void ExecuteSkill(BossSkill skill)
    {
        switch (skill.name)
        {
            case "매직스피어":
                StartCoroutine(Attack_Magic_spear());
                break;
            case "매직볼":
                StartCoroutine(Attack_Magic_ball());
                break;
            case "매직락":
                StartCoroutine(Attack_Magic_rock());
                break;
        }

        Debug.Log("스킬 사용 : " + skill.name);
    }
    IEnumerator Attack_Magic_spear()
    {
        anime.SetTrigger("Magic_spear");
        yield return new WaitForSeconds(0.2f);
        GameObject Spear = Instantiate(Magic_spear, transform.position, transform.rotation);
        Spear.SetActive(true);
        yield return new WaitForSeconds(2.5f);
    }

    IEnumerator Attack_Magic_ball()
    {
        Vector3 startPos = transform.position;
        anime.SetTrigger("Fly_Start");
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

        GameObject Ball = Instantiate(Magic_ball, startPos + Vector3.up * 2.3f, Quaternion.identity);
        Ball.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        anime.SetTrigger("Magic_Ball");
        yield return new WaitForSeconds(0.3f);
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
    }


    IEnumerator Attack_Magic_rock()
    {
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
        anime.SetTrigger("Magic_rock");
        Rock.transform.parent = null;
        Rock.GetComponent<Magic_Rock>().enabled = true; ;
        Rock.GetComponent<Magic_Rock>().StartFalling(); // Magic_Rock이 떨어지도록 호출

        yield return new WaitForSeconds(dropDelay);
    }
}
