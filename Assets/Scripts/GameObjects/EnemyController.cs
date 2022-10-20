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
    public Animator animator;
    StateMachine stateMachine;


    private float atkCD;
    private float curCD;
    private float atk;
    private float health;
    // Start is called before the first frame update
    /// <summary>
    /// 设置初始状态，从配置表获取敌人信息,根据敌人类型获取属性
    /// </summary>
    void Start()
    {
        
        atk = DataManager.Instance.Enemies[EnemyType].Attack;
        atkCD = DataManager.Instance.Enemies[EnemyType].AtkCD;
        health = DataManager.Instance.Enemies[EnemyType].Hp;

        animator = GetComponent<Animator>();

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
        GameObject go = Instantiate(ExpPrefeb, pos, Quaternion.identity);
        go.GetComponent<Exp>().SetExp(DataManager.Instance.Enemies[EnemyType].Exp);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (stateMachine.CurrentState == null) return;
        //TODO:将敌人的受击和死亡分为两个状态，加入有限状态机中
        if (collision.GetComponent<Boomerang>() != null)
        {
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Close);
            health -= collision.GetComponent<Boomerang>().Attack;
            //告诉相机控制器，敌人被击中
            EventManager.Instance.SendEvent("EnemyAttacked");
            //显示伤害UI
            DamageUIManager.Instance.ShowDamageUI(transform.position,(int) collision.GetComponent<Boomerang>().Attack);

            if (health <= 0)
            {
                SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
                Player.Instance.KillCount++;
                GenExp();
                animator.SetTrigger("Death");
                GetComponent<BoxCollider2D>().enabled = false;
                stateMachine.SetNextState((State)new NormalEnemyDeathState());
                
                
            }
            animator.SetTrigger("Hit");
        }

    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void OnEntryAnimationFinish()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.SetNextStateToMain();
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (stateMachine==null|| stateMachine.CurrentState == null) return;
        if (collision.gameObject == Player.Instance.Character)
        {
            Debug.LogFormat("Type of Enemy is:{0}", DataManager.Instance.Enemies[EnemyType].Name);
            Attack();
        }
    }


}
