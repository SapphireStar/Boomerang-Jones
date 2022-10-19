using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System;
using System.IO;

using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    public string DataPath;
    public Dictionary<int, EnemyDefine> Enemies;
    public Dictionary<int, WaveDefine> Waves;


    public DataManager()
    {
        this.DataPath = "Data/";
        Debug.LogFormat("DataManager > DataManager()");
    }

    public void Load()
    {
        string json = File.ReadAllText(this.DataPath + "EnemyDefine.txt");
        this.Enemies = JsonConvert.DeserializeObject<Dictionary<int, EnemyDefine>>(json);

    }


    public IEnumerator LoadData()
    {

        string json = File.ReadAllText(this.DataPath + "EnemyDefine.txt");
        this.Enemies = JsonConvert.DeserializeObject<Dictionary<int, EnemyDefine>>(json);
        yield return null;

        json = File.ReadAllText(this.DataPath + "WaveDefine.txt");
        this.Waves = JsonConvert.DeserializeObject<Dictionary<int, WaveDefine>>(json);
        yield return null;

    }

#if UNITY_EDITOR


#endif
}
