using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    private float difficulty;
    public float Difficulty { get { return difficulty; } set { difficulty = value; } }

    private float spawnDuration;
    public float SpawnDuration { get { return spawnDuration; } set { spawnDuration = value; } }

    public Game()
    {
        Difficulty = 1;
        SpawnDuration = 1.5f;
    }
    public void Reset()
    {
        Difficulty = 1;
        SpawnDuration = 1.5f;
    }
}
