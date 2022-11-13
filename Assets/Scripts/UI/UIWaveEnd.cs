using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveEnd : UIWindow
{
    public Button NextWave;
    public List<UpgradeItem> UpgradeItems;
    public Text RemainPerk;

    private static bool buildingFlag = false;

    public void Start()
    {
        EventManager.Instance.Subscribe("SetUpgradePerk", UpdateUpgradeItem);
        UpdateUpgradeItem(null);
    }
    public void Update()
    {
        if (buildingFlag)//当地形加载完成时，关闭地形加载界面
        {
            BuildingComplete();
            buildingFlag = false;
        }
    }
    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("SetUpgradePerk", UpdateUpgradeItem);
    }
    private void OnEnable()
    {
        UpdateUpgradeItem(null);
    }

    public void UpdateUpgradeItem(object[] param)
    {
        RemainPerk.text = string.Format("Remain {0} Perk", Player.Instance.UpgradePerk.ToString());
        if (Player.Instance.UpgradePerk > 0)
        {
            UnityEngine.Debug.Log("Update Upgrade Item");
            UpgradeItems[0].Init(DataManager.Instance.Upgrades[DataManager.Instance.Levels[Player.Instance.Level-Player.Instance.UpgradePerk].Upgrade1]);
            UpgradeItems[1].Init(DataManager.Instance.Upgrades[DataManager.Instance.Levels[Player.Instance.Level - Player.Instance.UpgradePerk].Upgrade2]);
            UpgradeItems[2].Init(DataManager.Instance.Upgrades[DataManager.Instance.Levels[Player.Instance.Level - Player.Instance.UpgradePerk].Upgrade3]);
        }
        else
        {
            foreach (var item in UpgradeItems)
            {
                item.Unenable();
            }
        }
    }
    public void EnterNextWave()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        GameManager.Instance.EnterNextWave();
    }

    static string sArguments = @"helloworld.py";
    static Thread thread;
    static UIBuilding go;
    public void OnClickChangeTerrain()
    {
        go = UIManager.Instance.Show<UIBuilding>();
        //StartCoroutine(ChangeTerrain());
        thread = new Thread(() => //使用多线程调用脚本，防止阻塞
        {
            Process p = new Process();
            string path = @"E:\GitHub\Game_Projects\BaseEnvironment\Assets\Scripts\Python\" + sArguments;
            UnityEngine.Debug.Log("hello python");
            p.StartInfo.FileName = @"python.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.Arguments = path;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.BeginOutputReadLine();
            p.OutputDataReceived += new DataReceivedEventHandler(Out_RecvData);
            p.WaitForExit();

            UnityEngine.Debug.Log("end process");
        });
        thread.IsBackground = true;
        thread.Start();

    }
    void BuildingComplete()
    {
        UIManager.Instance.Close(typeof(UIBuilding));
        //地形生成完成，让游戏管理器加载地形
        GameManager.Instance.StartUpdateBlock();
    }
/*    IEnumerator ChangeTerrain(string args = "")
    {
        Process p = new Process();
        string path = @"E:\GitHub\Game_Projects\BaseEnvironment\Assets\Scripts\Python\" + sArguments;
        UnityEngine.Debug.Log("hello python");
        p.StartInfo.FileName = @"python.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = path;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(Out_RecvData);
        p.WaitForExit();
        yield return null;
        UnityEngine.Debug.Log("end process");
    }*/

    static void Out_RecvData(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("data:" + e.Data);
        thread.Abort();
        buildingFlag = true;
    }



}
