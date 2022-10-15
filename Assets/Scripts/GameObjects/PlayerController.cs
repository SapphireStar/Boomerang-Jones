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
        Player.Instance.Character = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.IsDead)
        {
            Shoot();
        }
        

    }
    private void FixedUpdate()
    {
        if (!Player.Instance.IsDead)
        {
            Movement();
        }
        
    }
    private void Movement()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.2)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * Player.Instance.Speed * Time.deltaTime, 0, 0);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2)
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical") * Player.Instance.Speed * Time.deltaTime, 0);
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
            go.GetComponent<Boomerang>().Shoot(new Vector2(position.x,position.y) - new Vector2(transform.position.x, transform.position.y), Player.Instance.Force, gameObject);

        }
    }
}
