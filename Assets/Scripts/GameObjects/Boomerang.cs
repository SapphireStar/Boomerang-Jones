using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float rotationSpeed = 5;
    public Transform RotateCenter;
    public float LifeCycle = 3;
    public float ReturnCycle = 3;
    

    private Vector2 direction;
    private float duration = 0.2f;
    private float atkDuration = 0.1f;
    private float currentRotate = 0;
    private float force;
    private GameObject owner;
    private float maxSpeed;


    private Rigidbody2D rigidbody;
    private StateMachine stateMachine;
    
    // Start is called before the first frame update
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<StateMachine>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Return();
        //回旋镖落地则停止转动
        if(ReturnCycle>0)
            Rotate();
        
    }

    private void FixedUpdate()
    {
        if (atkDuration > 0) atkDuration -= Time.deltaTime;

        //当CD完成，删除对象
        if (LifeCycle <= 0)
        {
            removeThis();
        }
    }

    private void removeThis()
    {
        Player.Instance.Boomerangs.Remove(this);
        Destroy(gameObject);
    }

    private void Return()
    {
        /*        float playerBoomerangDistance = Vector3.Distance(owner.transform.position, this.transform.position);
                if (duration > 0)
                {
                    //一开始投出回旋镖后，向前飞行一段距离再返回
                    rigidbody.AddForce(new Vector2(direction.x, direction.y).normalized * force*10/playerBoomerangDistance);

                    duration -= Time.deltaTime;
                }
                else if (Vector3.Distance(this.transform.position, owner.transform.position) > 0)
                {
                    //向量归一化，防止速度过快
                    // if (playerBoomerangDistance < 3) playerBoomerangDistance = 2;
                    // if (playerBoomerangDistance > 6) playerBoomerangDistance = 8;
        *//*            rigidbody.AddForce((owner.transform.position - this.transform.position
                        ).normalized * force * (playerBoomerangDistance));*//*
                    rigidbody.AddForce((owner.transform.position - this.transform.position).normalized * 5 * force);
                    rigidbody.velocity = rigidbody.velocity.normalized * force*3;

                }*/
        //transform.RotateAround(rotateCenter, new Vector3(0, 0, 1), force);


        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }


    }
    private void Rotate()
    {
        this.transform.RotateAround(RotateCenter.position,new Vector3(0,0,1),rotationSpeed * Time.deltaTime * 150);

    }
    public void Shoot(Vector2 _direction,float _force,GameObject _owner)
    {
        direction = new Vector2(_direction.x,_direction.y).normalized;
        Debug.LogFormat("Shoot in{0} force:{1}", _direction, _force);
        this.force = _force;
        this.owner = _owner;
        rigidbody.velocity = direction * 3 * force;
        stateMachine.SetNextStateToMain();

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.Character && duration <= 0&&atkDuration<=0)
        {
            Player.Instance.GetAttacked(Player.Instance.Attack);
            atkDuration = 0.1f;

        }
    }

}
