using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum Type { Normal, Boss };
    public Type enemyType;
    public GameObject Magic_ball;
    public GameObject Magic_spear;
    public GameObject Magic_rock;
    private Coroutine coroutine;
    public Animator anime;
    public Transform Ballport;
    Vector3 lookVec;
    bool isLook;

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

    Transform player;
    CharacterController cc;

    float currentTime = 0;
    float attackDelay = 1.5f; //

    public int attackPower = 3; // Enemy ���ݷ�

    Vector3 originPos;
    Quaternion originRot;
    public float maxDistanceFromPlayer = 40f; //�÷��̾� �߰� �ִ� ����

    public int hp;
    public int maxhp = 15;
    public float healDelay = 1f;

    NavMeshAgent agent;

    Animator anim;

    void Start()
    {
        isLook = true;
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;

        cc = GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;

        hp = maxhp;

        if (enemyType == Type.Boss)
        {
            hp = 300;
            attackDistance = 20f;
        }

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        agent.speed = moveSpeed;

        anim = transform.GetComponentInChildren<Animator>();
    }


    void Update()
    {
        currentTime += Time.deltaTime; //

        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3 (h, 0, v) * 5f;
            transform.LookAt(player.position + lookVec);
        }

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
        if(Vector3.Distance(transform.position, player.position) > maxDistanceFromPlayer)
        {
            m_State = EnemyState.Return;
            print("���� ��ȯ : Move -> Return !!");
            anim.SetTrigger("MoveToReturn");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            //transform.forward = (player.position - transform.position).normalized;
            agent.SetDestination(player.position);
        }
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
            //�̺κ� ���� ���� �ʿ�!
            if (currentTime >= attackDelay && enemyType == Type.Normal)
            {
                StartCoroutine(AttackProcess());
                currentTime = 0;
            }
            else if (enemyType == Type.Boss && currentTime >= attackDelay)
            {
                if (coroutine == null)
                coroutine = StartCoroutine(BossMagicProcess());
                currentTime = 0;
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

    IEnumerator BossMagicProcess()
    {
        yield return new WaitForSeconds(0.5f);

        int randAction = Random.Range(0, 3);
        switch (randAction)
        {
            case 0:
                StartCoroutine(Attack_Magic_spear());
                break;
            case 1:
                StartCoroutine(Attack_Magic_ball());
                break;
            case 2:
                StartCoroutine(Attack_Magic_rock());
                break;
        }
    }

    IEnumerator Attack_Magic_spear()
    {
        anime.SetTrigger("doMagic_spear");
        yield return new WaitForSeconds(0.2f);
        GameObject Spear = Instantiate(Magic_spear, transform.position, transform.rotation);
        Spear.SetActive(true);
        print("magic_spear!");
        yield return new WaitForSeconds(2.5f);

        StartCoroutine(BossMagicProcess());
    }

    IEnumerator Attack_Magic_ball()
    {
        anime.SetTrigger("doMagic_ball");
        yield return new WaitForSeconds(0.2f);
        GameObject Ball = Instantiate(Magic_ball, Ballport.position, Ballport.rotation);
        Ball.SetActive(true);
        print("magic_ball!");
        yield return new WaitForSeconds(2.5f);

        StartCoroutine(BossMagicProcess());
    }

    IEnumerator Attack_Magic_rock()
    {
        print("magic_rock!");
        anime.SetTrigger("doMagic_rock");
        yield return new WaitForSeconds(0.2f);

        float offsetHeight = 20f; // �÷��̾� �Ӹ� ������ ������ ����

        Vector3 spawnPosition = player.position + Vector3.up * offsetHeight;

        GameObject Rock = Instantiate(Magic_rock, spawnPosition, Quaternion.identity);
        Rock.SetActive(true);
        Rock.GetComponent<Magic_Rock>().enabled = false; // Magic_Rock ��ũ��Ʈ ��Ȱ��ȭ
        Rock.transform.parent = player; // �÷��̾��� �ڽ� ������Ʈ�� �����Ͽ� �÷��̾�� �Բ� �̵�

        float followDuration = 2.0f; // �÷��̾� ���� �����Ǵ� �ð�
        float dropDelay = 1.0f; // �������� �� ������

        yield return new WaitForSeconds(followDuration);
        anime.SetTrigger("doMagic_rock_fire");
        Rock.transform.parent = null;
        Rock.GetComponent<Magic_Rock>().enabled = true; ; 
        Rock.GetComponent<Magic_Rock>().StartFalling(); // Magic_Rock�� ���������� ȣ��

        yield return new WaitForSeconds(dropDelay);

        StartCoroutine(BossMagicProcess());
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
