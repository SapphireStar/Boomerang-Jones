using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : UIWindow
{
    public void OnClickRestart()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        SceneManager.Instance.LoadScene("BattleScene");
        Player.Instance.IsDead = false;
    }
}
