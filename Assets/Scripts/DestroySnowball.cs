using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnowball : MonoBehaviour
{
    int damage;
    public int minDamage = 2;
    public int maxDamage = 4;
    int attackLevel;
    public LayerMask terrainLayer; // Transform enemy; // ´«À» ÆÄ±«½ÃÅ³ ·¹ÀÌ¾î

    private void Start()
    {
        attackLevel = PlayerPrefs.GetInt("AttackPower");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (terrainLayer == (terrainLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyFSM enemy = other.gameObject.GetComponent<EnemyFSM>();
            if (enemy != null)
            {
                damage = Random.Range(minDamage, maxDamage);

                damage += attackLevel;

                enemy.HitEnemy(damage);
            }
            Destroy(gameObject);
        }
    }
}
