using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioMixer audioMixer;
    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;

    const string MusicPath = "Music/";
    const string SoundPath = "Sound/";

    private  bool musicOn;
    public  bool MusicOn
    {
        get
        {
            return musicOn;
        }
        set
        {
            musicOn = value;
            this.MusicMute(!musicOn);
        }
    }

    private bool soundOn;
    public bool SoundOn
    {
        get
        {
            return soundOn;
        }
        set
        {
            soundOn = value;
            this.SoundMute(!soundOn);
        }
    }
    private int musicVolume;
    public int MusicVolume
    {
        get
        {
            return MusicVolume;
        }
        set
        {
            musicVolume = value;
            this.SetVolume("MusicVolume", musicVolume);
        }
    }

    private int soundVolume;
    public int SoundVolume
    {
        get
        {
            return SoundVolume;
        }
        set
        {
            soundVolume = value;
            this.SetVolume("SoundVolume", soundVolume);
        }
    }
    private void Start()
    {
        MusicOn = Config.MusicOn;
        SoundOn = Config.SoundOn;
        SoundVolume = Config.SoundVolume;
        MusicVolume = Config.MusicVolume;

    }

    public void MusicMute(bool mute)
    {
        this.SetVolume("MusicVolume", mute ? 0 : musicVolume);
    }
    public void SoundMute(bool mute)
    {
        this.SetVolume("SoundVolume", mute ? 0 : soundVolume);
    }

    private void SetVolume(string name,int value)
    {
        float volume = value * 0.5f - 50f;
        this.audioMixer.SetFloat(name, volume);
    }

    public void PlayMusic(string name)
    {
        AudioClip clip = Resloader.Load<AudioClip>(MusicPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlayMusic:{0} not existed", name);
            return;
        }
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
        musicAudioSource.clip = clip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlaySound(string name)
    {
        AudioClip clip = Resloader.Load<AudioClip>(SoundPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlaySound:{0} not existed", name);
            return;
        }
        soundAudioSource.PlayOneShot(clip);

    }
}
