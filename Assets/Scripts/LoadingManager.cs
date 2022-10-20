using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using Assets.Scripts.Managers;

public class LoadingManager : MonoBehaviour {

    /*    public GameObject UITips;
        public GameObject UILoading;
        public GameObject UILogin;
        public GameObject UIBg;

        public Slider progressBar;
        public Text progressText;
        public Text progressNumber;*/

    // Use this for initialization
    IEnumerator Start()
    {
        Debug.Log("Start");
        yield return DataManager.Instance.LoadData();
        SoundManager.Instance.PlayMusic(SoundDefine.Music_Login);

    }


    // Update is called once per frame
    void Update () {

    }
}
