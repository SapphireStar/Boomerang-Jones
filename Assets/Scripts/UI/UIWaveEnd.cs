using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveEnd : UIWindow
{
    public Button NextWave;

    public void Start()
    {
        
    }
    public void EnterNextWave()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        GameManager.Instance.EnterNextWave();
    }



}
