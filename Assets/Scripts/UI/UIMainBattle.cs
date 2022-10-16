using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainBattle : MonoBehaviour
{
    public Text KillCount;
    public Slider HealthBar;

    public Slider ExpBar;
    public Text Level;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Subscribe("SetKillCount", UpdateKillCount);
        EventManager.Instance.Subscribe("SetHealth", UpdateHealthBar);
        EventManager.Instance.Subscribe("UpdateExperience", UpdateExpBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("SetKillCount", UpdateKillCount);
        EventManager.Instance.Unsubscribe("SetHealth", UpdateHealthBar);
        EventManager.Instance.Unsubscribe("UpdateExperience", UpdateExpBar);
    }
    public void UpdateKillCount(object[] param)
    {
        if(KillCount!=null)
            KillCount.text = param[0].ToString();
    }
    public void UpdateHealthBar(object[] param)
    {
        if(HealthBar!=null)
        {
            HealthBar.value = Player.Instance.Health / Player.Instance.MaxHealth;
        }
    }

    public void UpdateExpBar(object[] param)
    {
        if (ExpBar != null && Level != null)
        {
            Level.text = string.Format("Level: {0}", param[0].ToString());
            ExpBar.value = (float)param[1] / Player.Instance.NextLevelExpArray[Player.Instance.Level];
        }

    }
}
