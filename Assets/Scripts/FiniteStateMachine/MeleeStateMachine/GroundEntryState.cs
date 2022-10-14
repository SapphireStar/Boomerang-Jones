using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        attackIndex = 1;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("PlayerAttack" + attackIndex);
    }

    /// <summary>
    /// 连击判定，如果在当前状态duration时间内点击攻击按钮，让shouldCombo为true，则在duration时间后进入下一个连击
    /// 状态，duration就是指令接收时间和状态切换时间
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        if (fixedTime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new GroundComboState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
