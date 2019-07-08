using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroll : MonoBehaviour
{
    public float fillAmount;
    public Image content;
    private bool open = false;


    public static GameObject txt_1;
    public static GameObject txt_2;
    public static GameObject txt_3;
    public static GameObject txt_4;
    public static GameObject txt_5;
    public static GameObject txt_6;
    public static GameObject txt_7;

   
    // Start is called before the first frame update

    void Start()
    {
        Attribute a = GameObject.Find("attributeManager").GetComponent<Attribute>();
        txt_1 = GameObject.Find("Text_1");
        txt_1.GetComponent<Text>().text = "生命值:" + a.getP1Attribute()[0];
        txt_2 = GameObject.Find("Text_2");
        txt_2.GetComponent<Text>().text = "攻击力:" + a.getP1Attribute()[1];
        txt_3 = GameObject.Find("Text_3");
        txt_3.GetComponent<Text>().text = "头防:" + a.getP1Attribute()[2];
        txt_4 = GameObject.Find("Text_4");
        txt_4.GetComponent<Text>().text = "胸防:" + a.getP1Attribute()[3];
        txt_5 = GameObject.Find("Text_5");
        txt_5.GetComponent<Text>().text = "后臂防:" + a.getP1Attribute()[4];
        txt_6 = GameObject.Find("Text_6");
        txt_6.GetComponent<Text>().text = "跳跃高度:" + a.getP1Attribute()[5];
        txt_7 = GameObject.Find("Text_7");
        txt_7.GetComponent<Text>().text = "移速:" + a.getP1Attribute()[6];

    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (open)
            {
                open = false;
            }
            else
            {
                open = true;
            }
            
        }
        if(!open)
        {
            if (fillAmount > 0)
                fillAmount -= 0.1f;
            if (fillAmount < 0)
                fillAmount = 0f;
            txt_1.SetActive(false);
            txt_2.SetActive(false);
            txt_3.SetActive(false);
            txt_4.SetActive(false);
            txt_5.SetActive(false);
            txt_6.SetActive(false);
            txt_7.SetActive(false);
        }
        if (open)
        {
            if(fillAmount<1)
            {
                fillAmount += 0.1f;
                if(fillAmount>0.15)
                {
                    txt_1.SetActive(true);
                }
                if (fillAmount > 0.3)
                {
                    txt_2.SetActive(true);
                }
                if (fillAmount > 0.45)
                {
                    txt_3.SetActive(true);
                }
                if (fillAmount > 0.6)
                {
                    txt_4.SetActive(true);
                }
                if (fillAmount > 0.75)
                {
                    txt_5.SetActive(true);
                }

                if (fillAmount > 0.9)
                {
                    txt_6.SetActive(true);
                }
                
            }
            if(fillAmount>=1)
            {
                fillAmount = 1f;
                txt_7.SetActive(true);
            }
           
        }

    }
    private void HandleBar()
    {
        content.fillAmount = fillAmount;
    }

}
