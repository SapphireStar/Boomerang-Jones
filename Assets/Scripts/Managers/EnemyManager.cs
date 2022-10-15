using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyManager,负责刷新敌人
/// </summary>
public class EnemyManager : MonoSingleton<EnemyManager>
{
    public GameObject EnemyPrefab;

    private Vector3 playerPos;
    private float spawnDuration = 0.5f;
    private Vector2 spawnRange = new Vector2(-8, 8);

    private bool startSpawn = true;
    public void Awake()
    {
        EventManager.Instance.Subscribe("RestartGame", (objs) => startSpawn = true);
        EventManager.Instance.Subscribe("GameOver", (objs) => startSpawn = false);
    }
    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("RestartGame", (objs) => startSpawn = true);
        EventManager.Instance.Unsubscribe("GameOver", (objs) => startSpawn = false);
    }
    public void FixedUpdate()
    {
        if (startSpawn)
        {
            if (spawnDuration > 0)
                spawnDuration -= Time.deltaTime;
            else
            {
                SpawnEnemy();
                spawnDuration = 0.5f;
            }
        }
    }
    public void SpawnEnemy()
    {
        if(playerPos==null) playerPos = Player.Instance.Character.transform.position;
        GameObject go = Instantiate(EnemyPrefab);
        go.transform.position = new Vector3(Random.Range(spawnRange.x, spawnRange.y), Random.Range(spawnRange.x, spawnRange.y), 0);
    }
}
