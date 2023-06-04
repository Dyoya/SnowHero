using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    EnemyFSM fsm;
    
    void Start()
    {
        fsm = GetComponent<EnemyFSM>();
    }

    public void PlayerHit()
    {
        fsm.AttackAction();
    }
}
