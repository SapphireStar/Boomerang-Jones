using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElementManager : MonoSingleton<UIWorldElementManager>
{
	public GameObject NameBarPrefab;
	public GameObject npcStatusPrefab;

	public Dictionary<Transform, GameObject> elements = new Dictionary<Transform, GameObject>();
	public Dictionary<Transform, GameObject> elementStatus = new Dictionary<Transform, GameObject>();
	// Use this for initialization
	protected override void OnStart()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}



}
