using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class CharacterManager : MonoSingleton<CharacterManager>
{
    private GameObject characterPrefab;
    public CharacterManager()
    {
        
    }
    public void Awake()
    {
        characterPrefab = Resloader.Load<GameObject>("GameObjects/Player");
    }
    public void PlayerEnterScene()
    {
        Log.InfoFormat("PlayerEnterScene");
        GameObject character = Instantiate(characterPrefab);
        character.transform.position = Vector3.zero;
        Player.Instance.Character = character;
    }
}
