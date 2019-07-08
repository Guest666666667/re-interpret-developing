using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class fightSelectBgPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private Animator anim;
    private Attribute attributeManager;
    private Animator P1anim_m;
    private Animator P2anim_m;
    //双方是否点击了确定
    private bool P1Enter = false;
    private bool P2Enter = false;
    //随机天气
    private Image weather;
    private Text weatherMessge;
    private GameObject random;
    private Animator weatherAnim;
    private string[] message = { "云", "月", "蚀", "阳" };

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        anim = GetComponent<Animator>();
        P1anim_m = transform.Find("farBG").Find("middleImage").GetComponent<Animator>();
        P2anim_m = transform.Find("nearBG").Find("middleImage").GetComponent<Animator>();
        //GameObject.Find("/player1").transform.Translate(4f, 0, 0);
        //GameObject.Find("/player2").transform.Translate(4f, 0, 0);
        GameObject.Find("/player1").transform.DOLocalMoveX(-6, 0.8f);
        GameObject.Find("/player2").transform.DOLocalMoveX(6, 0.8f);

        weather = transform.Find("weather").GetComponent<Image>();
        random = transform.Find("weather/random").gameObject;
        weatherMessge = transform.Find("weather/bgMessage").GetComponent<Text>();
        attributeManager = GameObject.Find("/attributeManager").GetComponent<Attribute>();
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (!P1anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.J))
        {
            P1Enter = true;
            transform.Find("farConfirmMessage").gameObject.SetActive(true);
            transform.Find("farBG").GetComponent<selectBgUnit>().enabled = false;
            transform.Find("farBG").GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        if (!P2anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.Keypad1))
        {
            P2Enter = true;
            transform.Find("nearConfirmMessage").gameObject.SetActive(true);
            transform.Find("nearBG").GetComponent<selectBgUnit>().enabled = false;
            transform.Find("nearBG").GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        if (P1Enter && P2Enter)
        {
            P1Enter = false;
            P2Enter = false;
            weatherAnim = transform.Find("weather").GetComponent<Animator>();
            weatherAnim.Play("turn-edge");
            Invoke("TurnSprite", 0.5f);
            Invoke("slideOut", 1.5f);
            Invoke("OnMakeSure", 2.5f);
            //OnMakeSure();
        }
    }
    public override void OnEnter()
    {
        if (canvasGroup == null)
        {
            canvasGroup = transform.GetComponent<CanvasGroup>();
        }
        base.OnEnter();
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetInteger("state", 1);
        //canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnPause()
    {
        //transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
        gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
        //transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(1).gameObject.SetActive(true);
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
        anim.SetInteger("state", 2);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnMakeSure()
    {
        uiMng.PushPanel(UIPanelType.fightMain);
    }
    public void TurnSprite()
    {
        //Debug.Log("Invoke in!!!");
        int index = Random.Range(0, 3);
        weather.sprite = random.transform.GetChild(index).GetComponent<SpriteRenderer>().sprite;
        transform.Find("weather/描述垫底").gameObject.SetActive(true);
        weatherMessge.text = message[index];
        attributeManager.setSceneArray(1, index);
    }
    public void slideOut()
    {
        anim.SetInteger("state", 2);
    }
}
