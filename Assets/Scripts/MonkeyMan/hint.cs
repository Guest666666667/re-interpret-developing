using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class hint : MonoBehaviour
{
    public float existTime = 1.2f;
    private bool isProcessing = false;
    public string Key;
    private float timeLast = 10000f;
    private float expectScale = 1f;
    private Material mat;
    private int coe;

    // Start is called before the first frame update
    void Awake()
    {
        coe = Random.value < 0.5f ? -1 : 1; //速度系数
        expectScale = 1f ;
        mat = new Material(Resources.Load<Material>("UI source/blurMaterial"));
        //transform.Find("bg").GetComponent<Image>().material = mat;
        transform.Find("miss").GetComponent<Image>().material = mat;
        transform.Find("keyHint").GetComponent<Image>().material = mat;
        //transform.Find("keyHint/evaluate").GetComponent<Image>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("miss").RotateAroundLocal(new Vector3(0f, 0f, 1f), coe * Time.deltaTime * Mathf.PI * 4f / (existTime * existTime) * timeLast);//停止时正好旋转2PI
        timeLast -= Time.deltaTime;
        /*if (Input.GetButtonDown(Key) && isProcessing)//按下对应按键且蓝圈在显示过程中则结束判定
        {
            complete(true);
        }*/
        if (isProcessing)//若正在显示过程中则蓝圈随时间逐渐缩小
        {
            expectScale -= 0.6f / (existTime+3*Time.fixedDeltaTime) * Time.deltaTime;//在existTime内缩小0.6f算出来的速度即为精确缩小速度
            transform.Find("process").localScale = new Vector3(expectScale/transform.localScale.x, expectScale / transform.localScale.y, expectScale / transform.localScale.z);
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
        
        timeLast = existTime;
        isProcessing = true;
        transform.Find("process").GetComponent<CanvasGroup>().alpha = 1;
        transform.Find("process").GetComponent<CanvasGroup>().interactable = true;
        transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = true;
        expectScale = 1f;
        //Debug.Log(transform.localScale);
        //transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), existTime);
        //GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_AlphaScale"), x => GetComponentInChildren<Image>().material.SetFloat("_AlphaScale", x), 1f, 0.2f);
        GetComponentInChildren<Image>().material.SetFloat("_Offset", 0.05f);
        DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_Offset"), x => GetComponentInChildren<Image>().material.SetFloat("_Offset",x), 0f, existTime);
        //transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void complete(bool isKeyDown, bool isPerfect)//结束判定，隐藏蓝圈，参数为消失是否为按键触发，按键是否完美
    {
        isProcessing = false;
        transform.Find("process").GetComponent<CanvasGroup>().alpha = 0;
        transform.Find("process").GetComponent<CanvasGroup>().interactable = false;
        transform.Find("process").GetComponent<CanvasGroup>().blocksRaycasts = false;
        expectScale = 1f;
        /*if (isKeyDown)
        {
            transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f);
        }*/
        //Tweener tweener = GetComponent<CanvasGroup>().DOFade(0, 0.4f);
        transform.Find("keyHint/evaluate").GetComponent<Image>().material = mat;
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
        DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_Offset"), x => GetComponentInChildren<Image>().material.SetFloat("_Offset", x), 0.05f, 0.4f);
        Tweener tweener = DOTween.To(() => GetComponentInChildren<Image>().material.GetFloat("_AlphaScale"), x => GetComponentInChildren<Image>().material.SetFloat("_AlphaScale", x), 0f, 0.4f);
        /*transform.Find("keyHint/Text").GetComponent<Text>().text = (isPerfect ? "善" : "误");
        if (!isPerfect)
        {
            transform.Find("keyHint/Text").GetComponent<Text>().text = (isKeyDown ? "误" : "弃");
        }*/
        tweener.onComplete = drop;
        transform.Find("process/halo").GetComponent<CanvasGroup>().alpha = 0f;
        if (isKeyDown && isPerfect)//按键闪光
        {
            transform.Find("miss/halo").GetComponent<CanvasGroup>().alpha = 1f;
            transform.Find("keyHint/evaluate").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI source/w_perfect");
            Sequence flash = DOTween.Sequence();
            flash.Append(transform.Find("miss/halo").GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
            transform.parent.Find("score").GetComponent<scoreManagement>().addPerfect();
        }
        else
        {
            transform.parent.Find("score").GetComponent<scoreManagement>().fault(isKeyDown);
            transform.Find("keyHint/evaluate").GetComponent<Image>().sprite = Resources.Load<Sprite>(isKeyDown ? "UI source/w_premature" : "UI source/w_miss");
            //transform.Find("keyHint/Text").GetComponent<Text>().text = (isKeyDown ? "误" : "弃");
            transform.parent.Find("health").GetComponent<healthManagement>().substract(isKeyDown ? 1 : 2);
        }
    }
    public void drop()
    {
        Destroy(gameObject);
    }
    public void setUp(string Key,int type, int seq)
    {
        this.Key = Key;
        transform.Find("keyHint/evaluate").GetComponent<Image>().sprite = null;
        //transform.Find("keyHint/Text").GetComponent<Text>().text = "";//Key.ToUpper();
        if (Key == "a" || Key == "d")
        {
            transform.localPosition -= new Vector3(0f, 165f, 0f);
        }
        if (Key == "e" || Key == "d")
        {
            transform.localPosition += new Vector3(540f, 0f, 0f);
        }
        if (type == 1)
        {
            transform.Find("miss").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI source/judge1");
            transform.Find("keyHint").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI source/smallJudge1");
        }
        /*for (int i = 0; i < seq; i++)//代码设置连击提示旋转角度
        {
            GameObject gen = Instantiate(Resources.Load("UIPanel/seqHint") as GameObject, transform, false);
            gen.transform.RotateAroundLocal(new Vector3(0f, 0f, 1f), i*Mathf.PI*2f/seq);
        }*/
        if (seq != 0)
        {
            GameObject gen = Instantiate(Resources.Load("UIPanel/seqHint") as GameObject, transform.Find("process"), false);
            gen.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI source/" + seq);
            //transform.Find("miss").GetChild(0).RotateAroundLocal(new Vector3(0f, 0f, 1f), -0.03f * Mathf.PI);//微调
        }
        begin();
    }
}
