using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private float duration = 0.2f;

    public void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

    public void Init(Vector3 pos,string text)
    {
        transform.position = pos;
        duration = 0.2f;
        GetComponent<Text>().text = text;
    }
    public void DestroySelf()
    {
        DamageUIManager.Instance.CollectDamageUI(gameObject);
    }
}
