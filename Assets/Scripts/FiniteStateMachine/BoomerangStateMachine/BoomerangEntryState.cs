using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangEntryState : State
{
    private float duration = 0.2f;
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
    }

    public override void onFixedUpdate()
    {
        base.onFixedUpdate();
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            stateMachine.GetComponent<Boomerang>().ReturnCycle -= Time.deltaTime;
        }
        else
        {
            State newState = (State)new BoomerangReturnState();
            stateMachine.SetNextState(newState);
        }

    }
}
