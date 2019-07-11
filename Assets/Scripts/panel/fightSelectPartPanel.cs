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
    //确认动画
    //private Animator confirmAnim;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("Music/fightingBGM");
        P1 = GameObject.Find("/player1").GetComponent<Rigidbody2D>();
        P2 = GameObject.Find("/player2").GetComponent<Rigidbody2D>();
        P1.gravityScale = 0;P2.gravityScale = 0;

        canvasGroup = transform.GetComponent<CanvasGroup>();
        P1anim_m = transform.Find("P1").Find("P1Child").Find("middleShow").GetComponent<Animator>();
        P2anim_m = transform.Find("P2").Find("P2Child").Find("middleShow").GetComponent<Animator>();
        //confirmAnim = GameObject.Find("/成").GetComponent<Animator>();
        transform.Find("P1").gameObject.SetActive(true);
        transform.Find("P2").gameObject.SetActive(true);
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (!P1anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.J))
        {
            P1Enter = true;
            //confirmAnim.Play("P1confirm");
            transform.Find("P1confirmMessage").gameObject.SetActive(true);
            transform.Find("P1").GetComponent<selectTotalPart>().enabled = false;
            transform.Find("P1").Find("P1Child").GetComponent<selectUnitPart>().enabled = false;
            transform.Find("P1").GetComponent<CanvasGroup>().alpha = 0.5f;
            AudioManager.Instance.PlaySound("Music/Sound/UI/sure");
        }
        if (!P2anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.Keypad1))
        {
            P2Enter = true;
            //confirmAnim.Play("P2confirm");
            transform.Find("P2confirmMessage").gameObject.SetActive(true);
            transform.Find("P2").GetComponent<selectTotalPart>().enabled = false;
            transform.Find("P2").Find("P2Child").GetComponent<selectUnitPart>().enabled = false;
            transform.Find("P2").GetComponent<CanvasGroup>().alpha = 0.5f;
            AudioManager.Instance.PlaySound("Music/Sound/UI/sure");
        }
        if (P1Enter && P2Enter)
        {
            P1Enter = false;
            P2Enter = false;
            anim.SetInteger("state", 2);
            Invoke("OnMakeSure", 0.6f);
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetInteger("state", 1);
        //canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnPause()
    {
        //Invoke("set", 1);
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
        uiMng.PushPanel(UIPanelType.fightSetPose);
    }

}
