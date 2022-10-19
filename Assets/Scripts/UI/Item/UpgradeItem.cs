using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    public float Speed;
    public float Hp;
    public float Attack;
    public float Vampirism;

    public Image Background;
    public Image icon;
    public UIWaveEnd owner;
    public void Init(UpgradeDefine define)
    {
        Speed = define.Speed ;
        Hp = define.Health;
        Attack = define.Attack;

        
    }
    public void Unenable()
    {
        Background.color = new Color(160f / 255f, 160f / 255f, 160f / 255f, 183f / 255f);
    }
    public void Enable()
    {
        if (Player.Instance.UpgradePerk > 0)
        {
            Background.color = new Color(123f / 255f, 1, 112f / 255f, 183f / 255f);
            icon.color = new Color(236f / 236f, 236f / 236f, 236f / 255f, 1);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Player.Instance.UpgradePerk > 0)
        {
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
            Player.Instance.UpgradePerk--;
            //Apply upgrade
            Debug.Log("You Upgraded!");
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.Instance.UpgradePerk > 0)
        {
            Background.color = new Color(80f / 255f, 207f / 255f, 69f / 255f, 183f / 255f);
            icon.color = new Color(207f / 255f, 207f / 255f, 207f / 255f, 1);
        }
    }

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Player.Instance.UpgradePerk > 0)
        {
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Close);
            Background.color = new Color(179f / 255f, 1, 173f / 255f, 183f / 255f);
            icon.color = Color.white;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Player.Instance.UpgradePerk > 0)
        {
            Background.color = new Color(123f / 255f, 1, 112f / 255f, 183f / 255f);
            icon.color = new Color(236f / 236f, 236f / 236f, 236f / 255f, 1);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Player.Instance.UpgradePerk > 0)
        {
            Background.color = new Color(123f / 255f, 1, 112f / 255f, 183f / 255f);
            icon.color = new Color(236f / 236f, 236f / 236f, 236f / 255f, 1);
        }
    }
}
