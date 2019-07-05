using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scroll_2 : MonoBehaviour
{
    public float fillAmount;
    public Image content;
    private bool open = false;


    public static GameObject txt_11;
    public static GameObject txt_22;
    public static GameObject txt_23;
    public static GameObject txt_24;
    public static GameObject txt_25;
    public static GameObject txt_26;
    public static GameObject txt_27;


    // Start is called before the first frame update

    void Start()
    {
        txt_11 = GameObject.Find("Text_21");
        txt_22 = GameObject.Find("Text_22");
        txt_23 = GameObject.Find("Text_23");
        txt_24 = GameObject.Find("Text_24");
        txt_25 = GameObject.Find("Text_25");
        txt_26 = GameObject.Find("Text_26");
        txt_27 = GameObject.Find("Text_27");

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
            txt_11.SetActive(false);
            Debug.Log(txt_11.tag);
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
                    txt_11.SetActive(true);
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
