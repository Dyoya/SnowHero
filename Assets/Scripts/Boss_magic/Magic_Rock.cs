using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Rock : MonoBehaviour
{
    public int damage;
    Rigidbody rb;
    Transform player;
    bool isFalling = false;
    float acceleration = 40f; // 떨어지는 속도에 적용할 가속도 값
    bool isTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<Player>().Damage(damage);
            isTriggered = true;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (isFalling)
        {
            rb.velocity += Vector3.down * acceleration * Time.deltaTime;
        }
    }

    public void StartFalling()
    {
        isFalling = true;
    }
}

