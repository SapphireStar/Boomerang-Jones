using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    public float duration;
    protected Animator animator;
    protected bool shouldCombo;
    protected int attackIndex;

    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        animator = GetComponent<Animator>();
    }

    public override void onUpdate()
    {
        base.onUpdate();
        if (Input.GetMouseButtonDown(0))
        {
            shouldCombo = true;
        }
    }

    public override void onExit()
    {
        base.onExit();
    }
}
