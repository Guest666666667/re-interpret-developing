using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBar : MonoBehaviour
{
    public float fillAmount = 0f;//蓝量
    public Image content;
    public float speed = 0.1f;//蓝量增加的速度
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
        changeBlueBar();
    }
    /// <summary>
    /// 蓝条更新
    /// </summary>
    private void HandleBar()
    {
        content.fillAmount = fillAmount;
    }
    public void changeBlueBar()
    {
        fillAmount += Time.deltaTime*speed;
        if(fillAmount>=1f)
        {
            fillAmount = 1.0f;
        }
    }
    public void releaseSkill()
    {
        fillAmount = 0f;
    }   

    public float get()
    {
        return fillAmount;
    }
}
