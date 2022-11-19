using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySlowTraceState : State
{
    private float speed;
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        _stateMachine.gameObject.layer = LayerMask.NameToLayer("Enemy");//进入追踪状态后正式成为敌人，成为炮台的攻击目标
        /// <summary>
        /// 该状态是普通敌人的状态，从配置表中获取普通敌人的速度
        /// </summary>
        speed = DataManager.Instance.Enemies[1].Speed * Game.Instance.Difficulty * 0.5f;

    }
    public override void onFixedUpdate()
    {
        base.onFixedUpdate();
        if (Vector3.Distance(stateMachine.transform.position, Player.Instance.Character.transform.position) > 0.1f)
        {
            stateMachine.transform.Translate((Player.Instance.Character.transform.position - stateMachine.transform.position).normalized * Time.deltaTime * speed);
        }
        checkIsInSlowZone();
    }
    float checkCD = 0.5f;
    void checkIsInSlowZone()
    {
        if (checkCD > 0)
        {
            checkCD -= Time.deltaTime;
        }
        else
        {
            checkCD = 0.5f;
            Collider2D hit = Physics2D.OverlapCircle(stateMachine.transform.position, 0.2f, LayerMask.GetMask("SlowZone"));
            if (hit == null)
            {
                stateMachine.SetNextState(new NormalEnemyTraceState());
            }
        }

    }
}
