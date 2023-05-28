using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{

    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState m_State;

    public float findDistance = 30f; // 플레이어 발견 범위
    public float attackDistance = 4f; // 플레이어 공격 범위
    public float moveSpeed = 9f; // Enemy 이동 속도

    Transform player;
    CharacterController cc;

    float currentTime = 0;
    float attackDelay = 1.5f; //

    public int attackPower = 3; // Enemy 공격력

    Vector3 originPos;
    Quaternion originRot;
    public float moveDistance = 40f;

    public int hp;
    public int maxhp = 15;
    public float healDelay = 1f;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;

        cc = GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;

        hp = maxhp;

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime; //

        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:

                break;
        }
    }

    void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            agent.enabled = true;
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move !!");
        }
        if(currentTime > healDelay) //
        {
            hp += 2;
            currentTime = 0;
            if (hp > maxhp)
                hp = maxhp;
        }
        
    }
    void Move()
    {
        if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환 : Move -> Return !!");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack !!");
            //currentTime = attackDelay;
        }
    }

    IEnumerator AttackProcess() //
    {
        yield return new WaitForSeconds(0.5f);
        //player.GetComponent<PlayerMove>().DamageAction(attackPower); // 플레이어에게 데미지 주기
        print("공격!");
    }
    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            //currentTime += Time.deltaTime;
            if (currentTime >= attackDelay)
            {
                StartCoroutine(AttackProcess());
                currentTime = 0;
            }
        }
        else
        {
            agent.enabled = true;
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move !!");
            currentTime = 0;
        }
    }
    void Return()
    {
        if(Vector3.Distance(transform.position, originPos) > 0.5f)
        {
            agent.SetDestination(originPos);
        }
        else
        {
            transform.position = originPos;
            transform.rotation = originRot;
            //hp = maxhp;
            agent.enabled = false;
            m_State = EnemyState.Idle;
            print("상태 전환 : Return -> Idle");
        }
    }
    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    //코루틴 함수
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
        m_State = EnemyState.Move;
    }

    public void HitEnemy(int hitPower)
    {
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        hp -= hitPower;
        if(hp > 0)
        {
            m_State = EnemyState.Damaged;
            Damaged();
            print("상태 전환: Any State -> Damaged");
        }
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any State -> Die");
            Die();
        }
    }
    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        cc.enabled = false;
        //player.GetComponent<PlayerFire>().KillUp(); //플레이어 킬 카운트 업
        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }
}
