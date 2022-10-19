using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyEntryState : State
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
    }

    public void OnAnimationFinish()
    {
        State newstate = (State)new NormalEnemyTraceState();
        stateMachine.SetNextState(newstate);
        stateMachine.GetComponent<SpriteRenderer>().enabled = true;
        //TODO:设置动画状态机trigger，进入下一个动画
    }
}
