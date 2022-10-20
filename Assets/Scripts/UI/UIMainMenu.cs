using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickSystemConfig()
    {
        UIManager.Instance.Show<UISystemConfig>();
    }

    public void OnClickShowNormalMessageBox()
    {
        MessageBox.Show("测试普通消息框", "Test", MessageBoxType.Confirm, "同意", "拒绝");
    }
    public void OnClickShowErrorMessageBox()
    {
        MessageBox.Show("测试错误消息框", "Test", MessageBoxType.Error, "确认", "取消");
    }

    public void OnClickNextScene()
    {
        SceneManager.Instance.LoadScene("BattleScene");

        SoundManager.Instance.PlayMusic(SoundDefine.Music_Select);
        EventManager.Instance.SendEvent("RestartGame");
    }

    public void OnClickExit()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif

    }
}
