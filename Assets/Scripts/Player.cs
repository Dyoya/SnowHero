using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 20;
    int maxHp = 20;
    public GameObject hitEffect;

    void Start()
    {
        hp = maxHp;
    }

    public void Damage(int damage)
    {
        hp -= damage;

        if(hp > 0)
        {
            //피격 이펙트
        }
        if(hp < 0)
        {
            hp = 0;
        }
        print(hp);
    }
    IEnumerable PlayHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitEffect.SetActive(false);
    }
}
