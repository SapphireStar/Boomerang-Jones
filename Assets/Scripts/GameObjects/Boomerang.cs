using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float rotationSpeed = 5;
    public Transform RotateCenter;
    
    

    private Vector2 direction;
    private float duration = 0.2f;
    private float atkDuration = 0.1f;
    private float currentRotate = 0;
    private float force;
    private GameObject owner;
    private float maxSpeed;

    private Vector3 rotateCenter;
    private Vector2 massCenter;

    private Rigidbody2D rigidbody;
    
    // Start is called before the first frame update
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rotateCenter = ( Player.Instance.Character.transform.position + new Vector3(-direction.y, direction.x, 0)*2) ;

    }

    // Update is called once per frame
    void Update()
    {

        //Rotate();
        
    }

    private void FixedUpdate()
    {
        if (atkDuration > 0) atkDuration -= Time.deltaTime;
        Return();
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

        float massCenterBoomerangDistance = Vector3.Distance(massCenter, this.transform.position);
        rigidbody.AddForce((massCenter - new Vector2(this.transform.position.x, this.transform.position.y)
                        ).normalized * 25 * force / massCenterBoomerangDistance);
        

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
        rigidbody.velocity = direction * 4 * force;
        massCenter = Quaternion.AngleAxis(30, new Vector3(0, 0, 1))* (new Vector2(Player.Instance.Character.transform.position.x, Player.Instance.Character.transform.position.y));


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
