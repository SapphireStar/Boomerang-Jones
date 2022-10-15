using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainBattle : MonoBehaviour
{
    public Text KillCount;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Subscribe("SetKillCount", UpdateKillCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("SetKillCount", UpdateKillCount);
    }
    public void UpdateKillCount(object[] param)
    {
        if(KillCount!=null)
            KillCount.text = param[0].ToString();
    }
}
