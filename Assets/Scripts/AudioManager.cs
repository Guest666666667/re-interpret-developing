using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : UnitySingleton<AudioManager>
{
    //public static AudioManager Instance;
    public AudioSource MusicPlayer;
    public AudioSource SoundPlayer;
 
	// Use this for initialization
	void Start () {
       
        //Instance = new AudioManager();
        //Instance = this;
    }

    void Update()
    {
        
    }
    //// Update is called once per frame
    //void Update()
    //{
    //}
    //播放音乐
    public void PlayMusic(string name)
    {
        if(MusicPlayer.isPlaying==false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
    }
    public void Pause()
    {
        MusicPlayer.Pause();
    }
    public void Resume()
    {
        MusicPlayer.Play();
    }
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.clip = clip;
        SoundPlayer.PlayOneShot(clip);
    }
    //改变音量
    public void changeMusicVolume(float vol)
    {
        MusicPlayer.volume = vol;
    }
    public void changeSoundVolume(float vol)
    {
        SoundPlayer.volume = vol;
    }
}
