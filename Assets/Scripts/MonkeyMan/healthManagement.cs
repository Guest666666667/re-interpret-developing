using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        foreach (Image t in valueParent.GetComponentsInChildren<Image>())
        {
            t.material = new Material(Resources.Load<Material>("UI source/circleMaterial"));
            DOTween.To(() => t.material.GetFloat("_CurrentAngle"), x => t.material.SetFloat("_CurrentAngle", x), -1f, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void substract(int value)
    {
        int tmpVal = this.value;
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
                valueParent.Find("point" + i).GetComponent<Image>().material.SetFloat("_AlphaScale", 1);
            }
            else if (i >= this.value && i < tmpVal) 
            {
                Material t = valueParent.Find("point" + i).GetComponent<Image>().material;
                DOTween.To(() => t.GetFloat("_CurrentAngle"), x => t.SetFloat("_CurrentAngle", x), 0f, 0.5f);
            }
            else
            {
                valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 0f);
                valueParent.Find("point" + i).GetComponent<Image>().material.SetFloat("_AlphaScale", 0);
            }
        }
        if (this.value==0)
        {
            UIManager.Instance.PushPanel(UIPanelType.defeat);
            Time.timeScale = 0;
            AudioManager.Instance.Stop();
        }
    }
    public void plus(int value)
    {
        int tmpVal = this.value;
        if ((this.value = this.value+value)>maxValue)
        {
            this.value = maxValue;
        }
        for (int i = 0; i < maxValue; i++)
        {
            Color temp = valueParent.Find("point" + i).GetComponent<Image>().color;
            if (i < tmpVal)
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
            else if (i < this.value && i >= tmpVal)
            {
                Material t = valueParent.Find("point" + i).GetComponent<Image>().material;
                t.SetFloat("_AlphaScale", 1);
                DOTween.To(() => t.GetFloat("_CurrentAngle"), x => t.SetFloat("_CurrentAngle", x), -1f, 0.5f);
            }
            else
            {
                valueParent.Find("point" + i).GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 0f);
            }
        }
    }
}
