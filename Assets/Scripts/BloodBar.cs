using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    public float fillAmount;//血量
    public Image content;

    public Image content_2;//缓冲条
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
        Debug.Log("fillAmount:" + fillAmount);
    }
    /// <summary>
    /// 界面更新血条
    /// </summary>
    private void HandleBar()
    {
        if (fillAmount <= 0)
            fillAmount = 0;
        content.fillAmount = fillAmount;
        if(content_2.fillAmount!=fillAmount)
        {
            content_2.fillAmount -= speed * Time.deltaTime;
        }
        if(content_2.fillAmount<=fillAmount)
        {
            content_2.fillAmount = fillAmount;
        }
    }
    public float Map(float value,float inMin,float inMax,float outMin,float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    /// <summary>
    /// 血条扣血函数
    /// </summary>
    public void changeBlood(float para)
    {
        Debug.Log("Hitted");
        if(fillAmount>=0)
        {
            fillAmount -= para;
        }
    }

}
