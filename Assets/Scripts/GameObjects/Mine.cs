using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float damage = 150f;
    public float MineRefreshCD = 10f;
    public float BombLatency = 1f;

    private float curMineRefreshCD;
    private float checkEnemyCD = 0.5f;
    private bool isInCD = false;
    void Start()
    {
        curMineRefreshCD = 10;
    }


    void FixedUpdate()
    {
        if (isInCD)
        {
            if(curMineRefreshCD > 0)
            {
                curMineRefreshCD -= Time.deltaTime;
                return;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                curMineRefreshCD = MineRefreshCD;
                isInCD = false;
            }
        }

        if (checkEnemyCD > 0)
            checkEnemyCD -= Time.deltaTime;
        else
        {
            checkBomb();
        }
    }
    void checkBomb()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("SpecialEnemy", "Enemy"));
        if (hit != null)
        {
            StartCoroutine(Bomb());
        }
    }

    IEnumerator Bomb()
    {
        //TODO:产生即将爆炸的特效和音效
        yield return new WaitForSeconds(BombLatency);
        //TODO:产生爆炸特效和音效
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 2, LayerMask.GetMask("SpecialEnemy", "Enemy"));
        if (hit.Length > 0)
        {
            foreach (var item in hit)
            {
                if (item != null)
                {
                    item.GetComponent<EnemyController>().GetHit(damage);
                }
            }
        }

        //禁用地雷，直到CD结束
        GetComponent<SpriteRenderer>().enabled = false;
        isInCD = true;

    }
}
