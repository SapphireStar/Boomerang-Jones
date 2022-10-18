using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangReturnState : State
{
    private float force;
    private Vector2 massCenter;
    private Rigidbody2D rigidbody;
    public override void onEnter(StateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        force = Player.Instance.Force;
        rigidbody = stateMachine.GetComponent<Rigidbody2D>();
        massCenter = Quaternion.AngleAxis(30, new Vector3(0, 0, 1)) * (new Vector2(Player.Instance.Character.transform.position.x, Player.Instance.Character.transform.position.y));

    }
    public override void onFixedUpdate()
    {
        if (stateMachine.GetComponent<Boomerang>().ReturnCycle > 0)
        {
            stateMachine.GetComponent<Boomerang>().ReturnCycle -= Time.deltaTime;
            base.onFixedUpdate();
            /*            float massCenterBoomerangDistance = Vector3.Distance(massCenter, stateMachine.transform.position);
                        rigidbody.AddForce((massCenter - new Vector2(stateMachine.transform.position.x, stateMachine.transform.position.y)
                                        ).normalized * 20 * force / massCenterBoomerangDistance);*/
            if (Vector3.Distance(rigidbody.position, Player.Instance.Character.transform.position) > 10)
                rigidbody.velocity = (Player.Instance.Character.transform.position - rigidbody.transform.position).normalized * Player.Instance.BoomerangSpeed;
            if (rigidbody.velocity.magnitude<Player.Instance.BoomerangSpeed)    
            rigidbody.AddForce(
                    (Player.Instance.Character.transform.position-rigidbody.transform.position).normalized
                    * 8 * force*Mathf.Max(1,1/Vector3.Distance(Player.Instance.Character.transform.position , rigidbody.transform.position)));
        }
        else
        {
            State newState = (State)new BoomerangGroundedState();
            stateMachine.SetNextState(newState);
        }

    }
}
