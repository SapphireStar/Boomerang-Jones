using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStateMachine : StateMachine
{
    public void OnValidate()
    {
        if(mainStateType == null)
        {
            mainStateType = new IdleCombatState();
        }
    }
}
