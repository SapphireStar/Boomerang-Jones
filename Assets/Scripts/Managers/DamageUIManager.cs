using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUIManager : MonoSingleton<DamageUIManager>
{
    private GameObject DamageUI;
    Stack<GameObject> ObjectPool = new Stack<GameObject>();
    public Transform WorldCanvas;
    // Start is called before the first frame update
    public void Awake()
    {
        DamageUI = Resloader.Load<GameObject>("GameObjects/DamageUI");

    }
    void Start()
    {

    }

    public void ShowDamageUI(Vector3 pos,int damage)
    {
        if (ObjectPool.Count > 0)
        {
            GameObject go = ObjectPool.Pop();
            go.GetComponent<DamageUI>().Init(pos, damage.ToString());
            go.SetActive(true);
        }
        else
        {
            GameObject go = Instantiate(DamageUI, WorldCanvas);
            go.GetComponent<DamageUI>().Init(pos, damage.ToString());
        }
    }

    public void CollectDamageUI(GameObject go)
    {
        ObjectPool.Push(go);
        go.SetActive(false);
    }
}
