using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangGroundedState : State
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        Destroy(stateMachine.GetComponent<Rigidbody2D>());
        foreach (var item in stateMachine.GetComponents<PolygonCollider2D>())
        {
            Destroy(item);
        }
        stateMachine.GetComponent<Animator>().SetTrigger("Unenable");
        Debug.Log("");
    }
    public override void onFixedUpdate()
    {
        base.onFixedUpdate();
        stateMachine.GetComponent<Boomerang>().LifeCycle -= Time.deltaTime;
        
    }
}
