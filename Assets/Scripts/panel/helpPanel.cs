using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class helpPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private bool onShow;
    public help h1;
    public help_2 h2;
    public help h3;
    public help h4;
    public help_2 h5;
    public GameObject help_1;
    public GameObject help_2;
    public GameObject help_3;
    public GameObject help_4;
    public GameObject help_5;
    public GameObject help_6;
    private bool help = false;
    // Start is called before the first frame update
    void Start()
    {
        h1 = transform.Find("help_1").GetComponent<help>();
        h2 = transform.Find("help_2").GetComponent<help_2>();
        h3 = transform.Find("help_3").GetComponent<help>();
        h4 = transform.Find("help_4").GetComponent<help>();
        h5 = transform.Find("help_5").GetComponent<help_2>();
        help_1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //h1退出
        if (!h1.isActive)
        {
            h1.shouldActive = false;
            h2.shouldActive = true;
            help_1.SetActive(false);
            help_2.SetActive(true);

        }
        if (!h2.isActive)
        {
            h2.shouldActive = false;
            h3.shouldActive = true;
            help_2.SetActive(false);
            help_3.SetActive(true);
        }
        if (!h3.isActive)
        {
            h3.shouldActive = false;
            h4.shouldActive = true;
            help_3.SetActive(false);
            help_4.SetActive(true);
        }
        if (!h4.isActive)
        {
            h4.shouldActive = false;
            h5.shouldActive = true;
            help_4.SetActive(false);
            help_5.SetActive(true);
        }
        if (!h5.isActive ) 
        {
            h5.shouldActive = false;
            help_5.SetActive(false);
            h5.shouldActive = false;
            //h2.shouldActive = true;
            help = true;
            help_6.SetActive(true);

        }
        if(help && onShow)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                help_6.SetActive(false);
                OnClose();
                help = false;

            }
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Time.timeScale = 1;
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        onShow = true;
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
        onShow = false;
    }
    public override void OnResume()
    {
        Time.timeScale = 1;
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
        onShow = true;
    }
    public override void OnExit()
    {
        //Debug.Log("style exit in");
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //transform.DOLocalMoveX(500, .5f).OnComplete(() => canvasGroup.alpha = 0);//OnComplete使用了lambda表达式
        base.OnExit();
        onShow = false;
    }
    public void OnClose()
    {
        uiMng.PopPanel();
        Time.timeScale = 1;

        //AudioManager.Instance.Resume();
        //Debug.Log("pop already");
    }

}
