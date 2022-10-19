using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    private float difficulty;
    public float Difficulty { get { return difficulty; } set { difficulty = value; } }

    private float spawnDuration;
    public float SpawnDuration { get { return spawnDuration; } set { spawnDuration = value; } }

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
        }
    }
    private bool isPause;
    public bool IsPause { 
        get { return isPause; }
        set
        {
            isPause = value;
        }
    }
    public Game()
    {
        Difficulty = 1;
        SpawnDuration = 1.5f;
        Wave = 1;

        EventManager.Instance.Subscribe("RestartGame", Reset);
    }
    ~Game()
    {
        EventManager.Instance.Unsubscribe("RestartGame", Reset);
    }
    public void Reset(object[] param)
    {
        Difficulty = 1;
        SpawnDuration = 1.5f;
        Wave = 1;
    }
}
