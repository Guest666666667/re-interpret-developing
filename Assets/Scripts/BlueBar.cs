using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BattlePara;

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

        if(tag.Equals("BlueBar_1"))
        {
            speed = 0.1f * BattlePara.chargeSpeed1;
        }
        if (tag.Equals("BlueBar_2"))
        {
            speed = 0.1f * BattlePara.chargeSpeed2;
        }
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
    public void releaseSkill(float cost)
    {
        fillAmount = Mathf.Max(0,fillAmount-cost);
    }
    public void chargeFull()
    {
        fillAmount = 1f;
    }

    public float get()
    {
        return fillAmount;
    }
}
