using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManagement : MonoBehaviour
{
    private Transform valueParent;
    private int value = 5;
    public int maxValue = 5;

    // Start is called before the first frame update
    void Start()
    {
        valueParent = transform.Find("lifeValue");
        value = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void substract(int value)
    {
        if (this.value - value <= 0)
        {
            this.value = 0;
        }
        else
        {
            this.value -= value;
        }
        for (int i = 0; i < maxValue; i++)
        {
            Color temp = valueParent.Find("point"+i).GetComponent<Image>().color;
            if (i < this.value)
            {
                if (this.value <= 2)
                {
                    valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, 0f, 0f, temp.a);
                }
                else if (this.value > 2)
                {
                    valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, 1f, 1f, temp.a);
                }
                temp = valueParent.Find("point" + i).GetComponent<Image>().color;
                valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 1f);
            }
            else
            {
                valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 0f);
            }
        }
        
    }
    public void plus(int value)
    {
        if ((this.value = this.value+value)>maxValue)
        {
            this.value = maxValue;
        }
        for (int i = 0; i < maxValue; i++)
        {
            Color temp = valueParent.Find("point" + i).GetComponent<Image>().color;
            if (i < this.value)
            {
                if (this.value > 2)
                {
                    valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, 1f, 1f, temp.a);
                }
                else if (this.value <= 2)
                {
                    valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, 0f, 0f, temp.a);
                }
                temp = valueParent.Find("point" + i).GetComponent<Image>().color;
                valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 1f);
            }
            else
            {
                valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 0f);
            }
        }
    }
}
