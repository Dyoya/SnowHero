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

    public float findDistance = 30f; // �÷��̾� �߰� ����
    public float attackDistance = 4f; // �÷��̾� ���� ����
    public float moveSpeed = 9f; // Enemy �̵� �ӵ�

    public Transform player;
    CharacterController cc;

    //���� �ӵ� ����
    float currentTime = 0;
    float attackDelay = 1.5f; 

    public int attackPower = 3; // Enemy ���ݷ�

    Vector3 originPos;
    Quaternion originRot;
    public float maxDistanceFromPlayer = 40f; //�÷��̾� �߰� �ִ� ����

    //���� ü�� ����
    public int hp;
    public int maxhp = 15;
    public float healDelay = 1f;

    //�׺���̼�
    NavMeshAgent agent;

    //�ִϸ�����
    Animator anim;

    //���� ���� ��ũ��Ʈ
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

        //enemy �̸��� Boss_Wizard�̸�
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
            print("���� ��ȯ: Idle -> Move !!");

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
        //�÷��̾ �߰� ������ ���
        if(Vector3.Distance(transform.position, player.position) > maxDistanceFromPlayer)
        {
            m_State = EnemyState.Return;
            print("���� ��ȯ : Move -> Return !!");
            anim.SetTrigger("MoveToReturn");
        }
        //�÷��̾ �߰� ���� �ȿ� ����
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            agent.SetDestination(player.position);
        }
        //�÷��̾ ���� ���� �ȿ� ����
        else
        {
            agent.enabled = false;
            m_State = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack !!");
            anim.SetTrigger("MoveToAttackdelay");
        }
    }

    IEnumerator AttackProcess() //
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("StartAttack");
        //player.GetComponent<Player>().Damage(attackPower);
        transform.forward = (player.position - transform.position).normalized;
        print("����!");
    }
    public void AttackAction()
    {
        player.GetComponent<Player>().Damage(attackPower);
    }
    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            //��� �÷��̾� ���� �ٶ󺸵���
            transform.forward = (player.position - transform.position).normalized;

            if (currentTime >= attackDelay)
            {
                StartCoroutine(AttackProcess());
                currentTime = 0;
            }
            //������ ������� ���� �ǽ�
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
            print("���� ��ȯ: Attack -> Move !!");
            anim.SetTrigger("AttackToMove");
            currentTime = 0;
        }
    }

    void Return()
    {
        if(Vector3.Distance(transform.position, player.position) < maxDistanceFromPlayer)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Return -> Move !!");
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
            print("���� ��ȯ : Return -> Idle");
            anim.SetTrigger("ReturnToIdle");
        }
    }
    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    //�ڷ�ƾ �Լ�
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
            print("���� ��ȯ: Any State -> Damaged");
            anim.SetTrigger("Damaged");
        }
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ: Any State -> Die");
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
        //player.GetComponent<PlayerFire>().KillUp(); //�÷��̾� ų ī��Ʈ ��
        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }
}
