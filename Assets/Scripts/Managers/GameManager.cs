using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum SpawnState { SPAWNING,WAITING,COUNTING}

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
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
                UIManager.Instance.Show<UIWaveEnd>();
                waveCountDown = timeBetweenWaves;
                PauseGame();
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
            Debug.Log("PauseGame");
            Time.timeScale = 0;
        }
    }
    public void ResumeGame()
    {

        Debug.Log("ResumeGame");
        Time.timeScale = 1;
        Game.Instance.IsPause = false;
    }
    /// <summary>
    /// 进入下一波敌人，更新玩家的上一次击杀数
    /// </summary>
    public void EnterNextWave()
    {
        ResumeGame();
        Game.Instance.Wave++;
        waveCountDown = timeBetweenWaves;
        playerLastKill = Player.Instance.KillCount;
        state = SpawnState.COUNTING;
    }

    void onGameOver(object[] param)
    {
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
}
