using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyDeathState : State
{
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        _stateMachine.gameObject.layer = LayerMask.NameToLayer("Default");//死亡之后不再是炮台的攻击目标
    }
}
