using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected float time { get; set; }
    protected float fixedTime { get; set; }
    protected float lateTime { get; set; }

    public StateMachine stateMachine;

    public virtual void onEnter(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void onUpdate()
    {
        time += Time.deltaTime;
    }
    public virtual void onFixedUpdate()
    {
        fixedTime += Time.deltaTime;
    }
    public virtual void onLateUpdate()
    {
        lateTime += Time.deltaTime;
    }
    public virtual void onExit()
    {

    }

    #region Passthrough Methods

    protected static void Destroy(Object obj)
    {
        Object.Destroy(obj);
    }

    protected T GetComponent<T>() where T : Component { return stateMachine.GetComponent<T>(); }

    protected Component GetComponent(System.Type type) { return stateMachine.GetComponent(type); }

    protected Component GetComponent(string type) { return stateMachine.GetComponent(type); }
    #endregion

}
