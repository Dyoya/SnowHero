using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnowball : MonoBehaviour
{
    public int damage;
    // Transform enemy; // ´«À» ÆÄ±«½ÃÅ³ ·¹ÀÌ¾î

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
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
                enemy.HitEnemy(damage);
            }
            Destroy(gameObject);
        }
    }
}
