using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class hint : MonoBehaviour
{
    private bool isProcessing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("q") && isProcessing)//按下对应按键且蓝圈在显示过程中则结束判定
        {
            complete();
        }
        if (isProcessing)//若正在显示过程中则蓝圈随时间逐渐缩小
        {
            transform.Find("process").localScale -= new Vector3(1.2f * Time.deltaTime, 1.2f * Time.deltaTime, 1.2f * Time.deltaTime);
        }
        if (transform.Find("process").localScale.x < 0.4f)//蓝圈缩小到一定程度则自动结束判定
        {
            complete();
        }
    }

    public void begin()//开始出现蓝圈
    {
        isProcessing = true;
        transform.Find("process").GetComponent<CanvasGroup>().alpha = 1;
        transform.Find("process").GetComponent<CanvasGroup>().interactable = true;
        transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.Find("process").localScale = new Vector3(1f,1f,1f);
        GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void complete()//结束判定，隐藏蓝圈
    {
        isProcessing = false;
        transform.Find("process").GetComponent<CanvasGroup>().alpha = 0;
        transform.Find("process").GetComponent<CanvasGroup>().interactable = false;
        transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.Find("process").localScale = new Vector3(1f, 1f, 1f);
        transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f);
        GetComponent<CanvasGroup>().DOFade(0, 0.4f);
    }
    
}
