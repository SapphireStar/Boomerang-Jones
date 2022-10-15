using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Boomerang>() != null)
        {
            Player.Instance.KillCount++;
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
            Destroy(gameObject);
        }
    }
}
