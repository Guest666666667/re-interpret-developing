using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class selectStoryPanel : BasePanel
{
    private Button firstStoryButton;//夸父逐日
    private Button secondStoryButton;//大闹天宫
    private Button backButton;
    //private Button beginButton;
    private CanvasGroup canvasGroup;
    private bool firstOrSecond;//fight为true,story为false
    private Animator anim;
    private bool OnShow = true;
    private Animator turnScene;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        firstStoryButton = transform.Find("runAfterSunButton").GetComponent<Button>();
        secondStoryButton = transform.Find("fightWithGodButton").GetComponent<Button>();
        backButton = transform.Find("backButton").GetComponent<Button>();
        turnScene = GameObject.Find("/Canvas/RawImage").GetComponent<Animator>();

        anim = transform.GetComponent<Animator>();
        //beginButton = transform.Find("beginButton").GetComponent<Button>();
        //beginButton.gameObject.SetActive(false);//未选择前不可用

        //firstStoryButton.Select();
        Invoke("OnSelect", 1.1f);
        //绑定响应函数
        firstStoryButton.onClick.AddListener(OnTurnFirstClick);
        secondStoryButton.onClick.AddListener(OnTurnSecondClick);
        backButton.onClick.AddListener(OnBackClick);
        //beginButton.onClick.AddListener(OnBeginClick);
    }
    void Update()
    {
        if (OnShow)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                AudioManager.Instance.PlaySound("Music/Sound/UI/turn");
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                AudioManager.Instance.PlaySound("Music/Sound/UI/sure");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                OnShow = false;
                OnBackClick();
            }
        }
    }
    public void OnSelect()
    {
        firstStoryButton.Select();
    }
    public override void OnEnter()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetInteger("state", 1);
        //canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if (firstStoryButton == null)
            firstStoryButton = transform.Find("runAfterSunButton").GetComponent<Button>();
        firstStoryButton.Select();

        OnShow = true;
    }
    public override void OnPause()
    {
        base.OnPause();
        OnShow = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        firstStoryButton.Select();
        OnShow = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        //anim.enabled = false;
        anim.SetInteger("state", 2);
        OnShow = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("已调用ONEXIT");
        //transform.DOLocalMoveX(500, .5f).OnComplete(() => canvasGroup.alpha = 0);//OnComplete使用了lambda表达式
    }
    public void OnClose()
    {
        uiMng.PopPanel();
    }

    public void OnTurnFirstClick()
    {
        firstOrSecond = true;
        //beginButton.gameObject.SetActive(true);
        Debug.Log("first in");
        int count = UIManager.Instance.getStackCount();
        //淡出动画
        turnScene.SetBool("state", true);
        //进入下一个场景前将所有面板出栈
        for (int i = 0; i < count; i++)
        {
            uiMng.PopPanel(); uiMng.clearDict();
            Debug.Log("pop successfully!!!");
        }
        AudioManager.Instance.Stop();
        Invoke("OnTurn", 0.8f);
    }
    public void OnTurn()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnTurnSecondClick()
    {
        firstOrSecond = false;
        //beginButton.gameObject.SetActive(true);

    }
    public void OnBackClick()
    {
        AudioManager.Instance.PlaySound("Music/Sound/UI/return");
        uiMng.PopPanel();
        //uiMng.PushPanel(UIPanelType.select);
    }
    public void OnBeginClick()
    {
        int count = UIManager.Instance.getStackCount();
        if (firstOrSecond)
        {
            Debug.Log("first in");
            //进入下一个场景前将所有面板出栈
            for (int i = 0; i < count; i++)
            {
                uiMng.PopPanel(); uiMng.clearDict();
                Debug.Log("pop successfully!!!");
            }
            AudioManager.Instance.Stop();
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }               //选择了夸父逐日
        else
        {
            Debug.Log("second in");
            //进入下一个场景前将所有面板出栈
            //Debug.Log(count);
            //for (int i = 0; i < count; i++)
            //{
            //    uiMng.PopPanel(); uiMng.clearDict();
            //    Debug.Log("pop successfully!!!");
            //}
            //SceneManager.LoadScene(1, LoadSceneMode.Single);
        }               //选择了大闹天宫
    }
}