using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Assets.Scripts.Managers;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum SpawnState { SPAWNING,WAITING,COUNTING}

    private Dictionary<int, WaveDefine> dict = DataManager.Instance.Waves;

    private int playerLastKill;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private float checkEnemyCountDown = 1;

    private SpawnState state = SpawnState.COUNTING;
    public void Start()
    {
       
    }
    Vector3[] offsets;
    public void Awake()
    {
        offsets = new Vector3[] { new Vector3(Random.Range(-5, -3), Random.Range(-5, -3), 0),
                                        new Vector3(Random.Range(3,5), Random.Range(-5, -3),0),
                                        new Vector3(Random.Range(-5,-3), Random.Range(3, -5),0),
                                         new Vector3(Random.Range(3,5), Random.Range(3, 5),0)};
        waveCountDown = timeBetweenWaves;

        //初始化方块
        StartCoroutine(updateBlock());
        StartCoroutine(WaveCountDown(UIManager.Instance.Show<UIWaveCountDown>()));

        EventManager.Instance.Subscribe("RestartGame", Reset);
        EventManager.Instance.Subscribe("RestartGame", onRestartGame);
        EventManager.Instance.Subscribe("GameOver", onGameOver);
    }
    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("RestartGame", Reset);
        EventManager.Instance.Unsubscribe("RestartGame", onRestartGame);
        EventManager.Instance.Unsubscribe("GameOver", onGameOver);
    }

    public void Update()
    {
        if(state == SpawnState.WAITING&&!Player.Instance.IsDead)
        {
            if (!enemyIsAlive())
            {
                //通关提示
                if (Game.Instance.Wave == 10)
                {
                    UIManager.Instance.Show<UIWin>();
                    PauseGame();
                }
                StartCoroutine(WaitForNextWave());
                return;
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

        if (Game.Instance.HitSoundCD > 0) Game.Instance.HitSoundCD -= Time.deltaTime;


        if (buildingFlag)//当地形加载完成时，关闭地形加载界面
        {
            BuildingComplete();
            buildingFlag = false;
        }
    }

    public CinemachineVirtualCameraBase Maincamera;
    public CinemachineVirtualCameraBase SubCamera;
    IEnumerator WaitForNextWave()
    {

        while (Player.Instance.Boomerangs.Count > 0)
        {
            yield return null;
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Exp"))
        {
            Destroy(item);
        }
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.Show<UIWaveEnd>();

        //发送切换摄像机事件
        EventManager.Instance.SendEvent("DisableMainCamera", new object[] { });
        //等待摄像机切换
        yield return new WaitForSeconds(0.5f);

        PauseGame();
    }

    bool enemyIsAlive()
    {
        checkEnemyCountDown -= Time.deltaTime;
        if (checkEnemyCountDown <= 0)
        {
            checkEnemyCountDown = 1;
            if (Player.Instance.KillCount - playerLastKill >= dict[Game.Instance.Wave].EnemyCount * dict[Game.Instance.Wave].SubWaveCount)
            {
                return false;
            }
        }
        return true;
    }


    IEnumerator SpawnWave()
    {
        playerLastKill = Player.Instance.KillCount;
        state = SpawnState.SPAWNING;
        for(int i = 0; i < dict[Game.Instance.Wave].SubWaveCount; i++)
        {
            //TODO:调整刷怪位置
            Vector3 offset = offsets[(int)Random.Range(0, 4)];
            SpawnEnemy(Player.Instance.Character.transform.position+offset);
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Vector3 _enemy)
    {
        for(int i = 0;i< dict[Game.Instance.Wave].EnemyCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
            EnemyManager.Instance.Spawn((float)Random.Range(0f,1f),_enemy + offset);
        }
    }

    public void PauseGame()
    {
        Game.Instance.IsPause = true;
        if (Time.timeScale != 0)
        {
            UnityEngine.Debug.Log("PauseGame");
            Time.timeScale = 0;
        }
    }
    public void ResumeGame()
    {

        UnityEngine.Debug.Log("ResumeGame");
        Time.timeScale = 1;
        Game.Instance.IsPause = false;

        EventManager.Instance.SendEvent("EnableMainCamera", new object[] { });
    }
    /// <summary>
    /// 进入下一波敌人，更新玩家的上一次击杀数
    /// </summary>
    public void EnterNextWave()
    {
        ResumeGame();
        Game.Instance.Wave++;
        StartCoroutine(WaveCountDown(UIManager.Instance.Show<UIWaveCountDown>()));
        waveCountDown = timeBetweenWaves;
        playerLastKill = Player.Instance.KillCount;

        //StartUpdateBlock();
    }
    public void StartUpdateBlock()
    {
        //开始更新方块
        StartCoroutine(updateBlock());
    }

    IEnumerator WaveCountDown(UIWaveCountDown ui)
    {
        while (waveCountDown >= 0)
        {
            ui.SetCountDown((int)waveCountDown);
            yield return null;
        }
        ui.OnCloseClick();
    }

    void onGameOver(object[] param)
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        //发送切换摄像机事件
        EventManager.Instance.SendEvent("DisableMainCamera", new object[] { });
        //等待摄像机切换
        yield return new WaitForSeconds(0.5f);
        PauseGame();
    }

    void onRestartGame(object[] param)
    {
        ResumeGame();
    }
    void Reset(object[] param)
    {
        playerLastKill = 0;
        StopAllCoroutines();
    }
    List<GameObject> blocks = new List<GameObject>();
    IEnumerator updateBlock()
    {
        //清除原有方块
        foreach (var item in blocks)
        {
            Destroy(item);
        }
        blocks.Clear();

        StartCoroutine(DataManager.Instance.LoadBlocks());
        yield return null;
        Dictionary<int, BlockDefine> dict = DataManager.Instance.Blocks;
        foreach (var key in dict.Keys)
        {
            BlockType type = (BlockType)dict[key].Type;
            GameObject prefab = Resources.Load<GameObject>("GameObjects/Blocks/" + type.ToString());
            blocks.Add(Instantiate(prefab, new Vector3(dict[key].X + 0.5f, dict[key].Y + 0.5f, dict[key].Z), Quaternion.identity));

        }

    }



    private static bool buildingFlag = false;

    static string sArguments = @"helloworld.py";
    static Thread thread;
    static UIBuilding go;
    public void ChangeTerrain()
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
    static void Out_RecvData(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("data:" + e.Data);
        thread.Abort();
        buildingFlag = true;
    }
}
