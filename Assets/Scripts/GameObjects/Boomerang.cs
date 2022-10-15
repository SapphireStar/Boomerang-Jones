using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float rotationSpeed = 5;
    

    private Vector2 direction;
    private float duration = 0.15f;   
    private float currentRotate = 0;
    private float force;
    private GameObject owner;
    private float maxSpeed;

    private Rigidbody2D rigidbody;
    
    // Start is called before the first frame update
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Rotate();
        
    }

    private void FixedUpdate()
    {
        Return();
    }

    private void Return()
    {
/*        if (transform.position.x < -8||transform.position.x>8)
        {
            rigidbody.velocity = new Vector2(-rigidbody.velocity.x/1.5f, rigidbody.velocity.y/1.5f);
        }
        if(transform.position.y > 4.8 || transform.position.y < -4.8)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x/ 1.5f, -rigidbody.velocity.y/ 1.5f);
        }*/
        if (duration > 0)
        {
            //一开始投出回旋镖后，向前飞行一段距离再返回
            rigidbody.AddForce(new Vector2(direction.x, direction.y).normalized * force*2);
            duration -= Time.deltaTime;
        }
        else if (Vector3.Distance(this.transform.position, owner.transform.position) > 0)
        {
            //向量归一化，防止速度过快
            float playerBoomerangDistance =Vector3.Distance(owner.transform.position, this.transform.position);
            // if (playerBoomerangDistance < 3) playerBoomerangDistance = 2;
            // if (playerBoomerangDistance > 6) playerBoomerangDistance = 8;
            rigidbody.AddForce((owner.transform.position - this.transform.position).normalized * force * playerBoomerangDistance);
            // rigidbody.AddForce((owner.transform.position - this.transform.position).normalized * 10 * force / playerBoomerangDistance);
        }

    }
    private void Rotate()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotate += rotationSpeed * Time.deltaTime * 150));
    }
    public void Shoot(Vector2 _direction,float _force,GameObject _owner)
    {
        direction = _direction;
        Debug.LogFormat("Shoot in{0} force:{1}", _direction, _force);
        rigidbody.AddForce(new Vector2(_direction.x, _direction.y).normalized*_force);
        this.force = _force;
        this.owner = _owner;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player.Instance.Character&&duration<=0)
        {
            Player.Instance.IsDead = true;
            EventManager.Instance.SendEvent("GameOver", new object[] { });
        }
    }
}
