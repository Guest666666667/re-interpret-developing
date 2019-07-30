using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class storyMainPanel : BasePanel {
    private Button stopButton;
    private CanvasGroup canvasGroup;
    private bool onShow = false;

    // Use this for initialization
    private void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        //stopButton = transform.Find("stopButton").GetComponent<Button>();
        //stopButton.onClick.AddListener(OnStopClick);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onShow) 
        {
            OnStopClick();
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        onShow = true;
    }
    public override void OnPause()
    {
        onShow = false;
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
        onShow = true;
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
        Time.timeScale = 1;
    }
    public override void OnExit()
    {
        onShow = false;
        base.OnExit();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //UIManager.Instance.PopPanel();
    }
    public void OnStopClick()
    {
        //uiMng.PushPanel(UIPanelType.select);
        uiMng.PushPanel(UIPanelType. gameSet);
        Time.timeScale = 0;
        //GameObject.Find("AudioManager").GetComponent<AudioManager>().Pause();
        AudioManager.Instance.Pause();
    }
}
