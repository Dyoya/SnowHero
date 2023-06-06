using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public float Timer;
    public int HP;

    private void Awake()
    {
        var obj = FindObjectsOfType<GameData>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHP(int hp)
    {
        HP = hp;
    }
    public void SetTimer(float timer)
    {
        Timer = timer;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
