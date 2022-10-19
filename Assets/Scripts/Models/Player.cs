using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class Player : Singleton<Player>
{
    public GameObject Character;

    public float[] NextLevelExpArray = new float[] { 0, 50, 100, 200, 400, 800, 1600, 3200 };

    private float experience;
    /// <summary>
    /// 向MainUI发送消息，更新经验条
    /// </summary>
    public float Experience { get { return experience; } 
        set 
        { 
            experience = value;
            EventManager.Instance.SendEvent("UpdateExperience", new object[] { level, experience });
        } 
    }

    private int level;
    public int Level { get { return level; } 
        set
        {
            level = value;
            EventManager.Instance.SendEvent("UpdateExperience", new object[] { level, experience });
        } 
    }

    private float force;
    public float Force { get { return force; } set { force = value; } }

    private float boomerangSpeed;
    public float BoomerangSpeed
    {
        get { return boomerangSpeed; }
        set
        {
            boomerangSpeed = value;
        }
    }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    private float attack;
    public float Attack { get { return attack; }set { attack = value; } }

    private float health;
    /// <summary>
    /// 生命值变化时，通知UI
    /// </summary>
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            EventManager.Instance.SendEvent("SetHealth", new object[] { health });
            if (health <= 0)
            {
                IsDead = true;
            }
        }
    }
    private float maxHealth;
    /// <summary>
    /// 最大生命变化时，通知UI
    /// </summary>
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            EventManager.Instance.SendEvent("SetHealth", new object[] { Health });
        }
    }
    private float catchDistance;
    public float CatchDistance
    {
        get { return catchDistance; }
        set
        {
            catchDistance = value;
        }
    }

    private float catchAngle;
    public float CatchAngle
    {
        get { return catchAngle; }
        set
        {
            catchAngle = value;
        }
    }

    private float catchCD;
    public float CatchCD
    {
        get { return catchCD; }
        set 
        { 
            catchCD = value;

        }
    }

    private int maxBoomerang;
    public int MaxBoomerang
    {
        get { return maxBoomerang; }
        set
        {
            maxBoomerang = value;
        }
    }

    private float bonusTime;
    public float BonusTime
    {
        get { return bonusTime; }
        set { bonusTime = value; }
    }

    public List<Boomerang> Boomerangs = new List<Boomerang>();


    private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
        set
        {
            isDead = value;
            if(value == true)
            {
                EventManager.Instance.SendEvent("GameOver", new object[] { });
                UIManager.Instance.Show<UIGameOver>();
            }
        }
    }

    private int killCount;
    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            EventManager.Instance.SendEvent("SetKillCount", new object[] { killCount });
        }
    }

    private int upgradePerk;
    public int UpgradePerk
    {
        get { return upgradePerk; }
        set
        {
            upgradePerk = value;
            EventManager.Instance.SendEvent("SetUpgradePerk", new object[] { upgradePerk });
        }
    }
    public Player()
    {
        Force = 3;
        Speed = 5;
        Health = 100;
        MaxHealth = 100;
        Attack = 50;
        CatchDistance = 3f;
        MaxBoomerang = 3;
        CatchAngle = 90;
        BoomerangSpeed = 10;
        CatchCD = 3;
        BonusTime = 2;

        Experience = 0;
        Level = 1;
        EventManager.Instance.Subscribe("RestartGame", Reset);
    }
    ~Player()
    {
        EventManager.Instance.Unsubscribe("RestartGame", Reset);
    }

    //在游戏重新开始时需要重设的值
    public void Reset(object[] param)
    {
        Force = 3;
        Speed = 5;
        KillCount = 0;
        Health = 100;
        MaxHealth = 100;
        Attack = 50;
        CatchDistance = 3f;
        CatchAngle = 90;
        MaxBoomerang = 3;
        BoomerangSpeed = 10;
        CatchCD = 3;
        BonusTime = 2;
        IsDead = false;
        Boomerangs.Clear();

        Experience = 0;
        Level = 1;
    }

    public void GetAttacked(float atk)
    {
        Health -= atk;
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Open);
    }
}
