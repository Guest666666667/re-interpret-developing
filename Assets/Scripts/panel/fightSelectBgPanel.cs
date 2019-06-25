﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fightSelectBgPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private Animator P1anim_m;
    private Animator P2anim_m;
    //双方是否点击了确定
    private bool P1Enter = false;
    private bool P2Enter = false;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        P1anim_m = transform.Find("farBG").Find("middleImage").GetComponent<Animator>();
        P2anim_m = transform.Find("nearBG").Find("middleImage").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (!P1anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.J)) P1Enter = true;
        if (!P2anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.Keypad1)) P2Enter = true;
        if (P1Enter && P2Enter) OnMakeSure();
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
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
        uiMng.PushPanel(UIPanelType.fightMain);
    }
}
