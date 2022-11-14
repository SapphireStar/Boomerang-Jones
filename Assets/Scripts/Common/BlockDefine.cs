using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Battery,
    Mine
}
public class BlockDefine
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public int Type { get; set; }
}
