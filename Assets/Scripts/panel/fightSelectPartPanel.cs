using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class fightSelectPartPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private Rigidbody2D P1;
    private Rigidbody2D P2;

    private Animator P1anim_m;
    private Animator P2anim_m;
    //双方是否点击了确定
    private bool P1Enter = false;
    private bool P2Enter = false;
    // Start is called before the first frame update
    void Start()
    {
        P1 = GameObject.Find("/player1").GetComponent<Rigidbody2D>();
        P2 = GameObject.Find("/player2").GetComponent<Rigidbody2D>();
        P1.gravityScale = 0;P2.gravityScale = 0;

        canvasGroup = transform.GetComponent<CanvasGroup>();
        P1anim_m = transform.Find("P1").Find("P1Child").Find("middleShow").GetComponent<Animator>();
        P2anim_m = transform.Find("P2").Find("P2Child").Find("middleShow").GetComponent<Animator>();
        transform.Find("P1").gameObject.SetActive(true);
        transform.Find("P2").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (!P1anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.J)) P1Enter = true;
        if (!P2anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.Keypad1)) P2Enter = true;
        if (P1Enter && P2Enter) OnMakeSure();
    }
    public override void OnEnter()//不用enter，一开始就在   
    {
        base.OnEnter();
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
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnMakeSure()
    {
        P1Enter = false;
        P2Enter = false;
        //TODO change the position of player
        uiMng.PushPanel(UIPanelType.fightSetPose);
    }

}
