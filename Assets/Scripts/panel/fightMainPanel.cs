using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fightMainPanel : BasePanel
{
    private Button stopButton;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        stopButton = transform.Find("stopButton").GetComponent<Button>();
        stopButton.onClick.AddListener(OnStopClick);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        uiMng.PushPanel(UIPanelType.gameSet);
        Time.timeScale = 0;
        //GameObject.Find("AudioManager").GetComponent<AudioManager>().Pause();
        AudioManager.Instance.Pause();
    }
}
