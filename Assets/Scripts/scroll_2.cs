using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scroll_2 : MonoBehaviour
{
    public float fillAmount;
    public Image content;
    private bool open = false;


    public static GameObject txt_21;
    public static GameObject txt_22;
    public static GameObject txt_23;
    public static GameObject txt_24;
    public static GameObject txt_25;
    public static GameObject txt_26;
    public static GameObject txt_27;


    // Start is called before the first frame update

    void Start()
    {
        /*txt_21 = GameObject.Find("Text_21");
        txt_22 = GameObject.Find("Text_22");
        txt_23 = GameObject.Find("Text_23");
        txt_24 = GameObject.Find("Text_24");
        txt_25 = GameObject.Find("Text_25");
        txt_26 = GameObject.Find("Text_26");
        txt_27 = GameObject.Find("Text_27");*/

        Attribute a = GameObject.Find("attributeManager").GetComponent<Attribute>();
        txt_21 = GameObject.Find("Text_21");
        txt_21.GetComponent<Text>().text = "生命值:" + a.getP2Attribute()[0];
        txt_22 = GameObject.Find("Text_22");
        txt_22.GetComponent<Text>().text = "攻击力:" + a.getP2Attribute()[1];
        txt_23 = GameObject.Find("Text_23");
        txt_23.GetComponent<Text>().text = "头防:" + a.getP2Attribute()[2];
        txt_24 = GameObject.Find("Text_24");
        txt_24.GetComponent<Text>().text = "胸防:" + a.getP2Attribute()[3];
        txt_25 = GameObject.Find("Text_25");
        txt_25.GetComponent<Text>().text = "后臂防:" + a.getP2Attribute()[4];
        txt_26 = GameObject.Find("Text_26");
        txt_26.GetComponent<Text>().text = "跳跃高度:" + a.getP2Attribute()[5];
        txt_27 = GameObject.Find("Text_27");
        txt_27.GetComponent<Text>().text = "移速:" + a.getP2Attribute()[6];

    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
        if (Input.GetKeyDown(KeyCode.Keypad0))
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
        if (!open)
        {
            if (fillAmount > 0)
                fillAmount -= 0.1f;
            if (fillAmount < 0)
                fillAmount = 0f;
            txt_21.SetActive(false);
            Debug.Log(txt_21.tag);
            txt_22.SetActive(false);
            txt_23.SetActive(false);
            txt_24.SetActive(false);
            txt_25.SetActive(false);
            txt_26.SetActive(false);
            txt_27.SetActive(false);
        }
        if (open)
        {
            if (fillAmount < 1)
            {
                fillAmount += 0.1f;
                if (fillAmount > 0.15)
                {
                    txt_21.SetActive(true);
                }
                if (fillAmount > 0.3)
                {
                    txt_22.SetActive(true);
                }
                if (fillAmount > 0.45)
                {
                    txt_23.SetActive(true);
                }
                if (fillAmount > 0.6)
                {
                    txt_24.SetActive(true);
                }
                if (fillAmount > 0.75)
                {
                    txt_25.SetActive(true);
                }

                if (fillAmount > 0.9)
                {
                    txt_26.SetActive(true);
                }

            }
            if (fillAmount >= 1)
            {
                fillAmount = 1f;
               txt_27.SetActive(true);
            }

        }

    }
    private void HandleBar()
    {
        content.fillAmount = fillAmount;
    }
}
