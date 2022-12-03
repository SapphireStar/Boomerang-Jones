using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Assets.Scripts.Managers;
using UnityEngine;

public class UIGameOver : UIWindow
{
    /// <summary>
    /// 游戏重新开始，发送重开事件
    /// </summary>
    public void Update()
    {

    }
    public void OnClickRestart()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        SceneManager.Instance.LoadScene("BattleScene");
        EventManager.Instance.SendEvent("RestartGame",new object[] { });
    }


    public void OnClickChangeTerrain()
    {
        GameManager.Instance.ChangeTerrain();

    }

}
