using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightSetPosePanel : BasePanel
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnEnter()//不用enter，一开始就在   
    {
        base.OnEnter();
    }
    public override void OnPause()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
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
        //TODO change the position of player
        uiMng.PushPanel(UIPanelType.fightSelectBg);
    }
}
