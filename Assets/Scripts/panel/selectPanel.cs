﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectPanel : BasePanel {

    private Button fightButton;
    private Button storyButton;
    private Button setButton;
    private Button exitButton;
    //private Button beginButton;
    private CanvasGroup canvasGroup;
    private bool fightOrStory;//fight为true,story为false

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fightButton = transform.Find("fightButton").GetComponent<Button>();
        storyButton = transform.Find("storyButton").GetComponent<Button>();
        setButton = transform.Find("setButton").GetComponent<Button>();
        exitButton = transform.Find("exitButton").GetComponent<Button>();
        //beginButton = transform.Find("beginButton").GetComponent<Button>();
        //beginButton.gameObject.SetActive(false);//未选择前不可用

        //绑定响应函数
        fightButton.onClick.AddListener(OnTurnFightClick);
        storyButton.onClick.AddListener(OnTurnStoryClick);
        setButton.onClick.AddListener(OnSetClick);
        exitButton.onClick.AddListener(OnEndClick);
        //beginButton.onClick.AddListener(OnBeginClick);
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            //Debug.Log("esc enter");
            Application.Quit();
        }
    }
    public override void OnEnter()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        Vector3 temp = transform.localPosition;
        temp.x = 500;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, .5f);
    }
    public override void OnPause()
    {
        base.OnPause();
        //canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        transform.DOLocalMoveX(500, .5f).OnComplete(() => canvasGroup.alpha = 0);//OnComplete使用了lambda表达式
    }
    public void OnClose()
    {
        uiMng.PopPanel();
    }

    public void OnTurnFightClick()
    {
        fightOrStory = true;
        //beginButton.gameObject.SetActive(true);
        //uiMng.PushPanel(UIPanelType.selectChild);
    }

    public void OnTurnStoryClick()
    {
        fightOrStory = false;
        //beginButton.gameObject.SetActive(true);
        uiMng.PushPanel(UIPanelType.selectStory);
    }
    public void OnSetClick()
    {
        uiMng.PushPanel(UIPanelType.set);
    }
    public void OnEndClick()
    {
        Debug.Log("end in!");
        Application.Quit();
    }
    //public void OnBeginClick()
    //{
    //    if (fightOrStory)
    //    {
    //        Debug.Log("fight in");
    //        //进入下一个场景前将所有面板出栈
    //        //for (int i = 0; i < UIManager.Instance.getStackCount(); i++)
    //        //{
    //        //    uiMng.PopPanel();uiMng.clearDict();
    //        //    Debug.Log("pop successfully!!!");
    //        //}
    //        //SceneManager.LoadScene("Fight");
    //    }               //选择了演义模式
    //    else
    //    {
    //        Debug.Log("story in");
    //        //DontDestroyOnLoad(this);
    //        //进入下一个场景前将所有面板出栈
    //        Debug.Log(uiMng.getStackCount());
    //        for (int i = 0; i < UIManager.Instance.getStackCount(); i++)
    //        {
    //            uiMng.PopPanel();uiMng.clearDict();
    //            Debug.Log("pop successfully!!!");
    //        }
    //        SceneManager.LoadScene(1,LoadSceneMode.Single);
    //    }               //选择了志异模式
    //}
}
