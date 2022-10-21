using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UIWindow
{

    public void OnClickMainMenu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResumeGame();
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        SceneManager.Instance.LoadScene("SampleScene");
    }

    public void OnClickRestart()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        SceneManager.Instance.LoadScene("BattleScene");
        EventManager.Instance.SendEvent("RestartGame", new object[] { });
    }
}
