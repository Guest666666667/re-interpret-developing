using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingMessage : UnitySingleton<settingMessage> {

    private static float musicVolume = 1f;
    private static float soundVolume = 1f;
    //TODO 没有做到切换统一

    private settingMessage()
    {
        Debug.Log("new again!!!!");
        musicVolume = 1f;
        soundVolume = 1f;
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public float getMusicVolume()
    {
        return musicVolume;
    }
    public float getSoundVolume()
    {
        return soundVolume;
    }
    public void setMusicVolume(float vol)
    {
        musicVolume = vol;
        //Debug.Log("set right!volume:"+musicVolume);
    }
    public void setSoundVolume(float vol)
    {
        soundVolume = vol;
    }
}
