using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveCountDown : UIWindow
{
    public Text Wave;
    public Text CountDown;

    public void OnEnable()
    {
        Wave.text = string.Format("Wave {0}", Game.Instance.Wave);
    }
    public void SetCountDown(int countdown)
    {
        CountDown.text = countdown.ToString();
    }
    
}
