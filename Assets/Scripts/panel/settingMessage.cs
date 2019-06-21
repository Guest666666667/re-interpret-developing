using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingMessage : UnitySingleton<settingMessage> {
    //作为整个游戏需要的数据交接平台
    //音乐音量调节
    private static float musicVolume = 1f;
    private static float soundVolume = 1f;

    //演义模式里的UI切换同步,true为上方整体切换，false为下方部件
    private bool totalOrUnit = true;

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

    public void setTotalOrUnit(bool b)
    {
        totalOrUnit = b;
    }
    public bool getTotalOrUnit()
    {
        return totalOrUnit;
    }
}
