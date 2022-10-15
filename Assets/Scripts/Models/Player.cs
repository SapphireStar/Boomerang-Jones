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
    public Player()
    {
        Force = 5;
        Speed = 5;
    }
}
