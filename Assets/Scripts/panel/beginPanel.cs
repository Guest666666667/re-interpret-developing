using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class beginPanel : BasePanel {
    private Button firstButton;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        firstButton = transform.Find("Button").GetComponent<Button>();
        firstButton.onClick.AddListener(OnSelectClick);
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.J))
    //    {
    //        OnSelectClick();
    //    }//键盘操作
    //}
    public override void OnEnter()//不用enter，一开始就在   
    {
        base.OnEnter();
        if (firstButton == null)
        {
            firstButton = transform.Find("Button").GetComponent<Button>();
            firstButton.onClick.AddListener(OnSelectClick);
        }
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        canvasGroup.interactable = false;
        base.OnPause();
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnSelectClick()
    {
        //uiMng.PushPanel(UIPanelType.select);
        gameObject.SetActive(false);
        UIManager.Instance.PushPanel(UIPanelType.select);
    }
    public void PushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)Enum.Parse(typeof(UIPanelType), panelTypeString);
        uiMng.PushPanel(panelType);
    }
}
