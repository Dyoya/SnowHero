using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Magic_spear : MonoBehaviour
{
    public int damage;
    public Transform target;
    Transform player;
    NavMeshAgent agent;
    bool isTriggered = false;
    float time = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.tag == "Player") // 한 번만 처리되도록 isTriggered 변수를 확인
        {
            player.GetComponent<Player>().Damage(damage);
            Destroy(gameObject);
            isTriggered = true; // 처리된 상태로 변경
        }
    }

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        agent.SetDestination(target.position);

        if (time >= 3)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
    }
}
