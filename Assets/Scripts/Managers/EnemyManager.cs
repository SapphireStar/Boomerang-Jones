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
        Spawn();
    }

    private void Spawn()
    {
        if (startSpawn)
        {
            if (spawnDuration > 0)
                spawnDuration -= Time.deltaTime;
            else
            {
                float random = Random.Range(0, 1.0f);
                if (random > 0.2f)
                    SpawnNormalEnemy();
                else SpawnSpiderEnemy();
                spawnDuration = Game.Instance.SpawnDuration;
            }
        }
    }
    public void SpawnNormalEnemy()
    {
        if(playerPos==null) playerPos = Player.Instance.Character.transform.position;
        GameObject go = Instantiate(NormalEnemyPrefab);
        go.transform.position = new Vector3(Random.Range(spawnRange.x, spawnRange.y), Random.Range(spawnRange.x, spawnRange.y), 0);
    }

    public void SpawnSpiderEnemy()
    {
        if (playerPos == null) playerPos = Player.Instance.Character.transform.position;
        GameObject go = Instantiate(SpiderEnemyPrefab);
        go.transform.position = new Vector3(Random.Range(spawnRange.x, spawnRange.y), Random.Range(spawnRange.x, spawnRange.y), 0);
    }
}
