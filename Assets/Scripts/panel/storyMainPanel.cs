using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class storyMainPanel : BasePanel {
    private Button stopButton;
    private CanvasGroup canvasGroup;

    // Use this for initialization
    private void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        stopButton = transform.Find("stopButton").GetComponent<Button>();
        stopButton.onClick.AddListener(OnStopClick);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStopClick();
        }
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
        Time.timeScale = 1;
    }
    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //UIManager.Instance.PopPanel();
    }
    public void OnStopClick()
    {
        //uiMng.PushPanel(UIPanelType.select);
        UIManager.Instance.PushPanel(UIPanelType. gameSet);
        Time.timeScale = 0;
        //GameObject.Find("AudioManager").GetComponent<AudioManager>().Pause();
        AudioManager.Instance.Pause();
    }
}
