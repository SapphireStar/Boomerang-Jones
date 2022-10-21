using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boomerangPrefab;
    public GameObject Up;
    public GameObject Down;
    public GameObject Right;
    public GameObject Left;
    public Animator animator;

    private float catchCD;
    private float autorecoverCD = 1;
    private MeshRenderer catchAreaMeshRenderer;
    private float bonusTime;//抓到回旋镖后的奖励时间
    private int comboMultipler; //连"接"加成



    void Start()
    {
        Player.Instance.Character = gameObject;
        EventManager.Instance.Subscribe("EnemyAttacked", Vampirism);

        catchCD = 0;
        catchAreaMeshRenderer = GetComponentInChildren<MeshRenderer>();
        animator = GetComponent<Animator>();

        
    }

    void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("EnemyAttacked", Vampirism);
    }
    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.IsDead)
        {
            Shoot();
            Catch();
        }
        

    }
    private void FixedUpdate()
    {
        if (!Player.Instance.IsDead)
        {
            Movement();
            checkCatchCD();
            AutoRecover();
            bonusTime -= Time.deltaTime;
        }

    }
    public bool Raycast(Vector2 pos, Vector2 dir, float dist, string tag, int layermask)
    {
        RaycastHit2D raycast = Physics2D.Raycast(pos, dir, dist, layermask);
        if (raycast.collider != null)
        {
            Debug.Log("detect:" + raycast.collider.transform.tag);
            if (raycast.transform.tag == tag)
            {
                return true;
            }
        }

        return false;
    }
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX=true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.SetFloat("Walk", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));

        if (Mathf.Abs(horizontal) >0.1&&
            ((horizontal > 0 && !Raycast(Right.transform.position, Vector2.right, 0.1f, "Wall", LayerMask.GetMask("Wall")))||
            (horizontal < 0&& !Raycast(Left.transform.position, Vector2.left, 0.1f, "Wall", LayerMask.GetMask("Wall"))))
            )
        {

                transform.Translate(new Vector3(horizontal * Player.Instance.Speed * Time.deltaTime, 0, 0));
        }
        if (Mathf.Abs(vertical) > 0.1&&
            ((vertical > 0 && !Raycast(Up.transform.position, Vector2.up, 0.1f, "Wall", LayerMask.GetMask("Wall")))||
            (vertical < 0&& !Raycast(Down.transform.position, Vector2.down, 0.1f, "Wall", LayerMask.GetMask("Wall"))))
            )
        {
                transform.Translate(new Vector3(0, vertical * Player.Instance.Speed * Time.deltaTime, 0));
        }
    }
    private void Shoot()
    {
        if (Game.Instance.IsPause) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)&&Player.Instance.Boomerangs.Count<Player.Instance.MaxBoomerang)
        {
            Debug.Log("Shoot Boomerang");
            Vector3 mouseposition = Input.mousePosition;
            Vector3 position = Camera.main.ScreenToWorldPoint(mouseposition);
            Debug.LogFormat("MousePosition:{0} {1}", position.x, position.y);
            GameObject go = Instantiate(boomerangPrefab);
            go.transform.position = transform.position + (position - transform.position).normalized;
            //若处于接到回旋镖的奖励时间中，得到双倍伤害
            go.GetComponent<Boomerang>().Shoot(
                new Vector2(position.x,position.y) - new Vector2(transform.position.x, transform.position.y),
                //Player.Instance.Force, bonusTime>0? Player.Instance.Attack*2: Player.Instance.Attack);
                Player.Instance.Force, bonusTime > 0 ?
                Player.Instance.Attack * ((comboMultipler > 2 ? 2 : 1 + 0.5f * comboMultipler)) : Player.Instance.Attack);
            //玩家丢出回旋镖后，将其加入玩家的回旋镖列表中，用于收回和限制回旋镖最大数量
            Player.Instance.Boomerangs.Add(go.GetComponent<Boomerang>());

            //如果处于双倍伤害，产生屏幕震动
            if (bonusTime > 0)
            {
                SoundManager.Instance.PlaySound(SoundDefine.SFX_Battle_bonusBoomerang);
                EventManager.Instance.SendEvent("ShakeCamera");
                bonusTime *= 0.5f;
            }
            else
            {
                SoundManager.Instance.PlaySound(SoundDefine.SFX_Battle_normalBoomerang);
            }
        }
    }

    private void Catch()
    {
        if (Game.Instance.IsPause) return;
        //若catchCD完成，则允许抓取回旋镖
        if (Input.GetKeyDown(KeyCode.Mouse1)&&catchCD<=0)
        {
            bool caughtAny = false;
            Vector3 mouseposition = Input.mousePosition;
            Vector3 position = Camera.main.ScreenToWorldPoint(mouseposition);
            position.z = 0;
            Vector2 direction = (position - transform.position).normalized;
            for( int i = 0; i < Player.Instance.Boomerangs.Count; i++)
            {
                //判断回旋镖距离和是否处在插在地上的状态
                if (Vector2.Distance(Player.Instance.Boomerangs[i].transform.position, transform.position) > Player.Instance.CatchDistance|| Player.Instance.Boomerangs[i].ReturnCycle<=0) continue;
                //这里必须对Vector2的dir进行归一化，否则vector3的z分量会让计算结果错误
                Vector2 dir = Player.Instance.Boomerangs[i].transform.position - transform.position;
                Vector2 boomDir = dir.normalized;
                Debug.LogFormat("Boomerang Dir: {0},{1}", boomDir.x, boomDir.y);
                float num1 = direction.x * boomDir.x + direction.y * boomDir.y;
                float cos = num1 / 1.0f;
                float angle = Mathf.Acos(cos) * (180 / Mathf.PI);
                Debug.LogFormat("Calculate catch angle:{0}", angle);
                //如果能够接到回旋镖，将其销毁，并从玩家的回旋镖列表中移出
                if (angle <= Player.Instance.CatchAngle / 2)
                {
                    Boomerang remove = Player.Instance.Boomerangs[i];
                    Player.Instance.Boomerangs.Remove(remove);
                    Destroy(remove.gameObject);
                    SoundManager.Instance.PlaySound(SoundDefine.SFX_Battle_grab);
                    i--;
                    //重置接回旋镖的CD
                    //增加bonusTime
                    caughtAny = true;
                    comboMultipler++;
                    bonusTime = Player.Instance.BonusTime;
                    EventManager.Instance.SendEvent("ShakeCamera");
                }
            }
            if (!caughtAny)
            {
                comboMultipler = 0;
                catchCD = Player.Instance.CatchCD;
                SoundManager.Instance.PlaySound(SoundDefine.SFX_Message_Error);
            }
        }

    }

    void checkCatchCD()
    {
        if (catchCD < 0)
        {
            catchAreaMeshRenderer.material.SetColor("_Color", new Color(0, 1, 0, 0.4f));
        }
        else
        {
            catchCD -= Time.deltaTime;
            catchAreaMeshRenderer.material.SetColor("_Color", new Color(1, 0, 0, 0.4f));
        }
    }

    public void GetExp(float exp)
    {
        int curLevel = Player.Instance.Level;
        float curExp = Player.Instance.Experience;
        curExp += exp;
        while (curExp >= Player.Instance.NextLevelExpArray[curLevel])
        {
            curExp -= Player.Instance.NextLevelExpArray[curLevel];
            curLevel++;
            Player.Instance.UpgradePerk+=1;//升级的时候增加perk

            //当玩家到达满级，停止升级
            if (curLevel == Player.Instance.NextLevelExpArray.Length)
            {
                EventManager.Instance.SendEvent("ReachMaxLevel", new object[] { });
            }

            //播放升级音效
            SoundManager.Instance.PlaySound(SoundDefine.SFX_Battle_Player_LevelUp);

            EventManager.Instance.SendEvent("LevelUp", new object[] { });
        }
        Player.Instance.Level = curLevel;
        Player.Instance.Experience = curExp;
    }

    public void Vampirism(object[] param)
    {
        Player.Instance.Health += (Player.Instance.Vampirism*Player.Instance.Attack);
    }

    public void AutoRecover()
    {
        if (Player.Instance.AutoRecover>0&& autorecoverCD <= 0)
        {
            Player.Instance.Health += Player.Instance.AutoRecover;
            autorecoverCD = 1;
        }
        else autorecoverCD -= Time.deltaTime;

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exp")
        {
            GetExp(collision.GetComponent<Exp>().exp);

            Destroy(collision.gameObject);
        }
    }
}
