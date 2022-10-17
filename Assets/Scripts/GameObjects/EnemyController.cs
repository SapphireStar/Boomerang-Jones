using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy的属性从DataManager的配置表中获取
/// </summary>
public class EnemyController : MonoBehaviour
{
    public int EnemyType;
    public GameObject ExpPrefeb;
    StateMachine stateMachine;


    private float atkCD;
    private float curCD;
    private float atk;
    // Start is called before the first frame update
    /// <summary>
    /// 设置初始状态，从配置表获取敌人信息,根据敌人类型获取属性
    /// </summary>
    void Start()
    {
        
        atk = DataManager.Instance.Enemies[EnemyType].Attack;
        atkCD = DataManager.Instance.Enemies[EnemyType].AtkCD;
        stateMachine = GetComponent<StateMachine>();
        stateMachine.SetNextStateToMain();
        ExpPrefeb = Resloader.Load<GameObject>("GameObjects/Exp");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FixedUpdate()
    {
        if (curCD > 0) curCD -= Time.deltaTime;
    }

    public virtual void Attack()
    {
        if (curCD <= 0)
        {
            curCD = atkCD;
            Player.Instance.GetAttacked(atk);
        }
    }

    // generate Exp
    public void GenExp()
    {
        Vector3 pos = transform.position;
        Instantiate(ExpPrefeb, pos, Quaternion.identity);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Boomerang>() != null)
        {
            Player.Instance.KillCount++;
            GenExp();
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
            Destroy(gameObject);
        }
        if (collision.gameObject == Player.Instance.Character)
        {
            Debug.LogFormat("Type of Enemy is:{0}", DataManager.Instance.Enemies[EnemyType].Name);
            Attack();
        }
    }


}
