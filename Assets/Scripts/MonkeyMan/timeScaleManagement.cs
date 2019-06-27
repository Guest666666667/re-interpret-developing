using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeScaleManagement : MonoBehaviour
{
    private int level = 0;//同时发生的时间放慢层数
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
    }
    public void addSlow()//添加一层时间放慢
    {
        Time.timeScale = 0.3f;
        level++;
    }

    public int getLevel()
    {
        return level;
    }

    public void delSlow()
    {
        if (--level <= 0)
        {
            Time.timeScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
