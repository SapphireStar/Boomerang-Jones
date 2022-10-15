using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public GameObject Character;
    public float force;
    public float speed;
    public Player()
    {
        force = 5;
        speed = 5;
    }
}
