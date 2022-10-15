using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class Player : Singleton<Player>
{
    public GameObject Character;

    private float force;
    public float Force { get { return force; } set { force = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
        set
        {
            isDead = value;
            if(value == true)
            {
                UIManager.Instance.Show<UIGameOver>();
            }
        }
    }

    private int killCount;
    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            EventManager.Instance.SendEvent("SetKillCount", new object[] { killCount });
        }
    }
    public Player()
    {
        Force = 5;
        Speed = 5;
        EventManager.Instance.Subscribe("RestartGame", Reset);
    }
    ~Player()
    {
        EventManager.Instance.Unsubscribe("RestartGame", Reset);
    }

    //在游戏重新开始时需要重设的值
    public void Reset(object[] param)
    {
        Force = 5;
        Speed = 5;
        KillCount = 0;
        IsDead = false;
    }
}
