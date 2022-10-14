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



    public DataManager()
    {
        this.DataPath = "Data/";
        Debug.LogFormat("DataManager > DataManager()");
    }

    public void Load()
    {
/*        string json = File.ReadAllText(this.DataPath + "MapDefine.txt");
        this.Maps = JsonConvert.DeserializeObject<Dictionary<int, MapDefine>>(json);*/

    }


    public IEnumerator LoadData()
    {


        yield return null;

    }

#if UNITY_EDITOR


#endif
}
