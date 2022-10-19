using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType {Survive,Attack,Move }
public class UpgradeDefine
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
    public float Health { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
}
