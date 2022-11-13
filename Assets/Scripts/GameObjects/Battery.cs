using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float ChangeTargetInterval = 2f;
    public float AtkCD = 0.5f;

    private GameObject Target;
    private float curChangeTargetInterval;
    private float curAtkCD;
    private GameObject bullet;
    
    void Start()
    {
        curChangeTargetInterval = ChangeTargetInterval;
        curAtkCD = AtkCD;
        bullet = Resources.Load<GameObject>("GameObjects/Blocks/Bullet");
    }

    
    void FixedUpdate()
    {
        if (curChangeTargetInterval > 0&&Target!=null)
        {
            curChangeTargetInterval -= Time.deltaTime;
        }
        else
        {
            CheckEnemy();
            curChangeTargetInterval = ChangeTargetInterval;
        }
        if (curAtkCD > 0)
        {
            curAtkCD -= Time.deltaTime;
        }
        else
        {
            curAtkCD = AtkCD;
            Shoot();
        }

    }
    void Shoot()
    {
        if (Target != null)
        {
            GameObject go = Instantiate<GameObject>(bullet,transform.position,Quaternion.identity);
            go.GetComponent<Bullet>().Init(Target.transform.position - transform.position,
                                            10f, 20f);
        }
    }
    void CheckEnemy()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 3, LayerMask.GetMask("SpecialEnemy","Enemy"));
        if (hit != null)
        {
            Target = hit.gameObject;
        }
        else Target = null;
    }
}
