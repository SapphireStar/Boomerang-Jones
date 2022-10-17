using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float exp;
    public float Speed = 10;

    private void FixedUpdate()
    {
        Vector3 playerPos = Player.Instance.Character.transform.position;
        if(Vector3.Distance(playerPos,transform.position)>0&& Vector3.Distance(playerPos, transform.position)<3)
            transform.Translate((playerPos - transform.position).normalized * Speed * Time.deltaTime);
    }
    public void SetExp(float _exp)
    {
        exp = _exp;
    }
}
