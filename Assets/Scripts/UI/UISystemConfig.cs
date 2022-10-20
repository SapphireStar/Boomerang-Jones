using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystemConfig : UIWindow
{
    public Image musicOff;
    public Image soundOff;

    public Toggle toggleMusic;
    public Toggle toggleSound;

    public Slider sliderMusic;
    public Slider sliderSound;

    private void Start()
    {
        this.toggleMusic.isOn = Config.MusicOn;
        this.toggleSound.isOn = Config.SoundOn;
        this.sliderMusic.value = Config.MusicVolume;
        this.sliderSound.value = Config.SoundVolume;
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnYesClick();
            
        }
    }
    public override void OnYesClick()
    {
        //在最底层的父类调用ResumeGame，否则无法继续
        base.OnYesClick();
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        PlayerPrefs.Save();
    }

    public void MusicToggle()
    {
        musicOff.enabled = !this.toggleMusic.isOn;
        Config.MusicOn = this.toggleMusic.isOn;
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
    }

    public void SoundToggle()
    {
        soundOff.enabled = !this.toggleSound.isOn;
        Config.SoundOn = this.toggleSound.isOn;
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
    }

    public void MusicVolume()
    {
        Config.MusicVolume = (int)this.sliderMusic.value;
        PlaySound();
    }

    public void SoundVolume()
    {
        Config.SoundVolume = (int)this.sliderSound.value;
        PlaySound();
    }
    float lastplay = 0.1f;

    public void PlaySound()
    {
        if (Time.realtimeSinceStartup - lastplay > 0.1)
        {
            lastplay = Time.realtimeSinceStartup;
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        }
    }

    public void OnBackToMainClick()
    {
        GameManager.Instance.ResumeGame();
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        SceneManager.Instance.LoadScene("SampleScene");
    }
}
