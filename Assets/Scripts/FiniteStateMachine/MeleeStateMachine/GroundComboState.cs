using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComboState : MeleeBaseState
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        attackIndex = 2;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("PlayerAttack" + attackIndex);
    }
    public override void onUpdate()
    {
        base.onUpdate();
        if (fixedTime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new GroundFinisherState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
