using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float atk;
    private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckHitEnemy();
        transform.Translate(direction*speed*Time.deltaTime);
    }
    public void Init(Vector3 direction, float atk,float speed)
    {
        this.direction = direction.normalized;
        this.atk = atk;
        this.speed = speed;
    }
    void CheckHitEnemy()
    {

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Enemy","SpecialEnemy"));
        if (hit != null)
        {
            hit.GetComponent<EnemyController>().GetHit(atk);
            Destroy(gameObject);
        }
    }
}
