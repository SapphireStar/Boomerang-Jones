using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        attackIndex = 3;
        duration = 0.75f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("PlayerAttack" + attackIndex);
    }
    /// <summary>
    /// 连击状态最后一击完成，回归原始状态
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        if (fixedTime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}
