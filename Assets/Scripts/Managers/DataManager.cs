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
    public Dictionary<int, EnemyDefine> Enemies = null;
    public Dictionary<int, WaveDefine> Waves = null;
    public Dictionary<int, UpgradeDefine> Upgrades = null;
    public Dictionary<int, LevelDefine> Levels = null;
    public Dictionary<int, BlockDefine> Blocks = null;


    public DataManager()
    {
        this.DataPath = "Data/";
        Debug.LogFormat("DataManager > DataManager()");
    }

    public void Load()
    {
        string json = File.ReadAllText(this.DataPath + "EnemyDefine.txt");
        this.Enemies = JsonConvert.DeserializeObject<Dictionary<int, EnemyDefine>>(json);


        json = File.ReadAllText(this.DataPath + "WaveDefine.txt");
        this.Waves = JsonConvert.DeserializeObject<Dictionary<int, WaveDefine>>(json);


        json = File.ReadAllText(this.DataPath + "UpgradeDefine.txt");
        this.Upgrades = JsonConvert.DeserializeObject<Dictionary<int, UpgradeDefine>>(json);


    }


    public IEnumerator LoadData()
    {

        string json = File.ReadAllText(this.DataPath + "EnemyDefine.txt");
        this.Enemies = JsonConvert.DeserializeObject<Dictionary<int, EnemyDefine>>(json);
        yield return null;

        json = File.ReadAllText(this.DataPath + "WaveDefine.txt");
        this.Waves = JsonConvert.DeserializeObject<Dictionary<int, WaveDefine>>(json);
        yield return null;

        json = File.ReadAllText(this.DataPath + "UpgradeDefine.txt");
        this.Upgrades = JsonConvert.DeserializeObject<Dictionary<int, UpgradeDefine>>(json);
        yield return null;

        json = File.ReadAllText(this.DataPath + "LevelDefine.txt");
        this.Levels = JsonConvert.DeserializeObject<Dictionary<int, LevelDefine>>(json);
        yield return null;


    }

    public IEnumerator LoadBlocks()
    {
        string json = File.ReadAllText(this.DataPath + "BlockDefine.txt");
        this.Blocks = JsonConvert.DeserializeObject<Dictionary<int, BlockDefine>>(json);
        yield return null;
    }

#if UNITY_EDITOR


#endif
}
