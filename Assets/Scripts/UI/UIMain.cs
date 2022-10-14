using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class UIMain : MonoBehaviour
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

}
