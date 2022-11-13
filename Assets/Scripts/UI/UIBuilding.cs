using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuilding : UIWindow
{
    public Text text;
    public float interval = 0.5f;

    private float curInterval;
    private int currentDot = 0;
    private double timestamp = 0;
    public void Awake()
    {
        curInterval = interval;
    }
    void OnEnable()
    {
        curInterval = interval;
    }
    void LateUpdate()//在所有脚本的Update函数之后调用
    {
        if (Time.realtimeSinceStartup - timestamp < interval)
            return;
        else
        {
            timestamp = Time.realtimeSinceStartup;
            switch (currentDot)
            {
                case 0:
                    text.text = "Building new terrain";
                    break;
                case 1:
                    text.text = "Building new terrain.";
                    break;
                case 2:
                    text.text = "Building new terrain..";
                    break;
                case 3:
                    text.text = "Building new terrain...";
                    break;
            }
            currentDot = (currentDot + 1) % 4;
        }
    }

}
