using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class hint : MonoBehaviour
{
    private bool isProcessing = false;
    private string Key;
    private float timeLast = 10000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLast -= Time.deltaTime;
        /*if (Input.GetButtonDown(Key) && isProcessing)//按下对应按键且蓝圈在显示过程中则结束判定
        {
            complete(true);
        }*/
        if (isProcessing)//若正在显示过程中则蓝圈随时间逐渐缩小
        {
            transform.Find("process").localScale -= new Vector3(0.6f / (0.5f + 3 * Time.fixedDeltaTime) * Time.deltaTime, 0.6f / (0.5f + 3 * Time.fixedDeltaTime) * Time.deltaTime, 0.6f / (0.5f + 3 * Time.fixedDeltaTime) * Time.deltaTime);//3个fixedDeltaTime为残影加速所耗时间，在0.5f+残影加速时间内缩小0.6f算出来的速度即为精确缩小速度
        }
        if (transform.Find("process").localScale.x < 0.4f)//蓝圈缩小到一定程度则自动消失
        {
            transform.Find("process").GetComponent<CanvasGroup>().alpha = 0;
            transform.Find("process").GetComponent<CanvasGroup>().interactable = false;
            transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        /*if (timeLast < (-0.1f))//超过最晚完美判定点则自动结束判定
        {
            complete(false);
        }*/
    }

    public void begin()//开始出现蓝圈
    {
        timeLast = 0.5f;
        isProcessing = true;
        transform.Find("process").GetComponent<CanvasGroup>().alpha = 1;
        transform.Find("process").GetComponent<CanvasGroup>().interactable = true;
        transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.Find("process").localScale = new Vector3(1f,1f,1f);
        //GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_AlphaScale"), x => GetComponentInChildren<Image>().material.SetFloat("_AlphaScale", x), 1f, 0.2f);
        GetComponentInChildren<Image>().material.SetFloat("_Offset", 0.05f);
        DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_Offset"), x => GetComponentInChildren<Image>().material.SetFloat("_Offset",x), 0f, 0.5f);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void complete(bool isKeyDown, bool isPerfect)//结束判定，隐藏蓝圈，参数为消失是否为按键触发，按键是否完美
    {
        isProcessing = false;
        transform.Find("process").GetComponent<CanvasGroup>().alpha = 0;
        transform.Find("process").GetComponent<CanvasGroup>().interactable = false;
        transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.Find("process").localScale = new Vector3(1f, 1f, 1f);
        /*if (isKeyDown)
        {
            transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f);
        }*/
        //Tweener tweener = GetComponent<CanvasGroup>().DOFade(0, 0.4f);
        DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_Offset"), x => GetComponentInChildren<Image>().material.SetFloat("_Offset", x), 0.05f, 0.4f);
        Tweener tweener = DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_AlphaScale"), x => GetComponentInChildren<Image>().material.SetFloat("_AlphaScale", x), 0f, 0.4f);
        transform.Find("keyHint/Text").GetComponent<Text>().text = (isPerfect ? "善" : "负");
        tweener.onComplete = drop;
    }
    public void drop()
    {
        Destroy(gameObject);
    }
    public void setUp(string Key)
    {
        this.Key = Key;
        transform.Find("keyHint/Text").GetComponent<Text>().text = Key.ToUpper();
        if (Key == "a" || Key == "d")
        {
            transform.localPosition -= new Vector3(0f, 165f, 0f);
        }
        if (Key == "e" || Key == "d")
        {
            transform.localPosition += new Vector3(540f, 0f, 0f);
        }
        begin();
    }
}
