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

    public Transform player;
    CharacterController cc;

    //공격 속도 관리
    float currentTime = 0;
    float attackDelay = 1.5f; 

    public int attackPower = 3; // Enemy 공격력

    Vector3 originPos;
    Quaternion originRot;
    public float maxDistanceFromPlayer = 40f; //플레이어 추격 최대 범위

    //몬스터 체력 정보
    public int hp;
    public int maxhp = 15;
    public float healDelay = 1f;

    //네비게이션
    NavMeshAgent agent;

    //애니메이터
    Animator anim;

    //보스 패턴 스크립트
    Boss_Wizard bw;

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

        anim = transform.GetComponentInChildren<Animator>();

        //enemy 이름이 Boss_Wizard이면
        if(this.gameObject.name == "Boss_Wizard")
        {
            bw = GetComponent<Boss_Wizard>();
            
        }
    }


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

            anim.SetTrigger("IdleToMove");
        }
        if(currentTime > healDelay) 
        {
            hp += 2;
            currentTime = 0;
            if (hp > maxhp)
                hp = maxhp;
        }
        
    }
    void Move()
    {
        //플레이어가 추격 범위를 벗어남
        if(Vector3.Distance(transform.position, player.position) > maxDistanceFromPlayer)
        {
            m_State = EnemyState.Return;
            print("상태 전환 : Move -> Return !!");
            anim.SetTrigger("MoveToReturn");
        }
        //플레이어가 추격 범위 안에 있음
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            agent.SetDestination(player.position);
        }
        //플레이어가 공격 범위 안에 있음
        else
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack !!");
            anim.SetTrigger("MoveToAttackdelay");
        }
    }

    IEnumerator AttackProcess() //
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("StartAttack");
        //player.GetComponent<Player>().Damage(attackPower);
        transform.forward = (player.position - transform.position).normalized;
        print("공격!");
    }
    public void AttackAction()
    {
        player.GetComponent<Player>().Damage(attackPower);
    }
    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            //계속 플레이어 방향 바라보도록
            transform.forward = (player.position - transform.position).normalized;

            if (currentTime >= attackDelay)
            {
                StartCoroutine(AttackProcess());
                currentTime = 0;
            }
            //마법사 보스라면 패턴 실시
            else if (this.gameObject.name == "Boss_Wizard")
            {
                //bw.useSkill();
                //while (!bw.isSkill) ;
            }
        }
        else
        {
            agent.enabled = true;
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move !!");
            anim.SetTrigger("AttackToMove");
            currentTime = 0;
        }
    }

    void Return()
    {
        if(Vector3.Distance(transform.position, player.position) < maxDistanceFromPlayer)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Return -> Move !!");
            anim.SetTrigger("ReturnToMove");
        }
        else if (Vector3.Distance(transform.position, originPos) > 0.5f)
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
            anim.SetTrigger("ReturnToIdle");
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
            anim.SetTrigger("Damaged");
        }
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any State -> Die");
            anim.SetTrigger("Die");
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
