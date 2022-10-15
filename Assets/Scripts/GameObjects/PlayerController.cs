using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boomerangPrefab;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();

    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.1)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * Player.Instance.speed * Time.deltaTime, 0, 0);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical") * Player.Instance.speed * Time.deltaTime, 0);
        }
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouseposition = Input.mousePosition;
            Vector3 position = Camera.main.ScreenToWorldPoint(mouseposition);
            Debug.LogFormat("MousePosition:{0} {1}", position.x, position.y);
            GameObject go = Instantiate(boomerangPrefab);
            go.transform.position = transform.position + (position - transform.position).normalized;
            go.GetComponent<Boomerang>().Shoot(new Vector2(position.x,position.y) - new Vector2(transform.position.x, transform.position.y), Player.Instance.force, gameObject);

        }
    }
}
