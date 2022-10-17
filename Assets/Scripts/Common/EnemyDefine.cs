using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefine
{
    public int ID { get; set; }
    public string Name { get; set; }
    public float Speed { get; set; }
    public float Attack { get; set; }
    public float Health { get; set; }

    public float AtkCD { get; set; }

    public string Path { get; set; }
    public float Exp { get; set; }
}
