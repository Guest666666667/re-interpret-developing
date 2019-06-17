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
    private Button beginButton;
    private CanvasGroup canvasGroup;
    private bool firstOrSecond;//fight为true,story为false

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        firstStoryButton = transform.Find("runAfterSunButton").GetComponent<Button>();
        secondStoryButton = transform.Find("fightWithGodButton").GetComponent<Button>();
        backButton = transform.Find("backButton").GetComponent<Button>();
        beginButton = transform.Find("beginButton").GetComponent<Button>();
        beginButton.gameObject.SetActive(false);//未选择前不可用

        //绑定响应函数
        firstStoryButton.onClick.AddListener(OnTurnFirstClick);
        secondStoryButton.onClick.AddListener(OnTurnSecondClick);
        backButton.onClick.AddListener(OnBackClick);
        beginButton.onClick.AddListener(OnBeginClick);
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

    public void OnTurnFirstClick()
    {
        firstOrSecond = true;
        beginButton.gameObject.SetActive(true);
    }

    public void OnTurnSecondClick()
    {
        firstOrSecond = false;
        beginButton.gameObject.SetActive(true);
    }
    public void OnBackClick()
    {
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
            //for (int i = 0; i < count; i++)
            //{
            //    uiMng.PopPanel(); uiMng.clearDict();
            //    Debug.Log("pop successfully!!!");
            //}
            //SceneManager.LoadScene("Fight");
        }               //选择了夸父逐日
        else
        {
            Debug.Log("second in");
            //DontDestroyOnLoad(this);
            //进入下一个场景前将所有面板出栈
            Debug.Log(count);
            for (int i = 0; i < count; i++)
            {
                uiMng.PopPanel(); uiMng.clearDict();
                Debug.Log("pop successfully!!!");
            }
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }               //选择了大闹天宫
    }
}