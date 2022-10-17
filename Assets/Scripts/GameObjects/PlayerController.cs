using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boomerangPrefab;
    
    void Start()
    {
        Player.Instance.Character = gameObject;

    }

    void OnDestroy()
    {
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
        }
        
    }
    private void Movement()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.2)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * Player.Instance.Speed * Time.deltaTime, 0, 0);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2)
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical") * Player.Instance.Speed * Time.deltaTime, 0);
        }
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&Player.Instance.Boomerangs.Count<Player.Instance.MaxBoomerang)
        {
            Debug.Log("Shoot Boomerang");
            Vector3 mouseposition = Input.mousePosition;
            Vector3 position = Camera.main.ScreenToWorldPoint(mouseposition);
            Debug.LogFormat("MousePosition:{0} {1}", position.x, position.y);
            GameObject go = Instantiate(boomerangPrefab);
            go.transform.position = transform.position + (position - transform.position).normalized;
            go.GetComponent<Boomerang>().Shoot(new Vector2(position.x,position.y) - new Vector2(transform.position.x, transform.position.y), Player.Instance.Force, gameObject);
            //玩家丢出回旋镖后，将其加入玩家的回旋镖列表中，用于收回和限制回旋镖最大数量
            Player.Instance.Boomerangs.Add(go.GetComponent<Boomerang>());
        }
    }

    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
                    SoundManager.Instance.PlaySound(SoundDefine.SFX_Message_Error);
                    i--;
                }
            }
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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exp")
        {
            GetExp(collision.GetComponent<Exp>().exp);

            Destroy(collision.gameObject);
        }
    }
}
