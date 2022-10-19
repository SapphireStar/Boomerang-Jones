using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyManager,负责刷新敌人
/// </summary>
public class EnemyManager : MonoSingleton<EnemyManager>
{
    public GameObject NormalEnemyPrefab;
    public GameObject SpiderEnemyPrefab;

    private Vector3 playerPos;
    private float spawnDuration = Game.Instance.SpawnDuration;
    private Vector2 spawnRange = new Vector2(-8, 8);

    private bool startSpawn = true;
    public void Awake()
    {
        EventManager.Instance.Subscribe("RestartGame", OnRestartGame);
        EventManager.Instance.Subscribe("GameOver", OnGameOver);
    }
    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("RestartGame", OnRestartGame);
        EventManager.Instance.Unsubscribe("GameOver", OnGameOver);
    }
    void OnRestartGame(object[] param)
    {
        startSpawn = true;
    }
    void OnGameOver(object[] param)
    {
        startSpawn = false;
    }
    public void FixedUpdate()
    {

    }

    public void Spawn(float rate,Vector3 pos)
    {
        if (DataManager.Instance.Waves[Game.Instance.Wave].SpiderEnemyRate>=rate)
        {
            SpawnSpiderEnemy(pos);
            
        }
        else
        {
            SpawnNormalEnemy(pos);
        }
    }
    public void SpawnNormalEnemy(Vector3 pos)
    {
        GameObject go = Instantiate(NormalEnemyPrefab);
        go.transform.position = pos;
    }

    public void SpawnSpiderEnemy(Vector3 pos)
    {
        GameObject go = Instantiate(SpiderEnemyPrefab);
        go.transform.position = pos;
    }
}
