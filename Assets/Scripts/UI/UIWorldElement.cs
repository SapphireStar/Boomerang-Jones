using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElement : MonoBehaviour
{
    public Transform owner;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        height = 2.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (owner != null)
        {
            this.transform.position = owner.transform.position + Vector3.up * height;
        }
        if (Camera.main != null)
            this.transform.LookAt(Camera.main.transform);

    }
}
