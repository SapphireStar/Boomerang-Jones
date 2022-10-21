using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyIdleState : State
{
    private float duration;
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        duration = 0.5f;
    }
    public override void onFixedUpdate()
    {
        base.onFixedUpdate();
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            State nextState = (State)new SpiderEnemyTraceState();
            stateMachine.SetNextState(nextState);
            stateMachine.GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}
