using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : State
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        State nextState = (State)new GroundEntryState();
        stateMachine.SetNextState(nextState);
    }
}
