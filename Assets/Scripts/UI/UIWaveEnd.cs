using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveEnd : UIWindow
{
    public Button NextWave;
    public List<UpgradeItem> UpgradeItems;
    public Text RemainPerk;

    public void Start()
    {
        EventManager.Instance.Subscribe("SetUpgradePerk", UpdateUpgradeItem);
        UpdateUpgradeItem(null);
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
        RemainPerk.text = string.Format("剩余{0}技能点", Player.Instance.UpgradePerk.ToString());
        if (Player.Instance.UpgradePerk > 0)
        {
            Debug.Log("Update Upgrade Item");
            UpgradeItems[0].Init(DataManager.Instance.Upgrades[1]);
            UpgradeItems[1].Init(DataManager.Instance.Upgrades[2]);
            UpgradeItems[2].Init(DataManager.Instance.Upgrades[3]);
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



}
