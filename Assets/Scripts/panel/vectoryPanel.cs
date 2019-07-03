using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class vectoryPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private Button backButton;
    private Button reBeginButton;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        backButton = transform.Find("backButton").GetComponent<Button>();
        reBeginButton = transform.Find("reBeginButton").GetComponent<Button>();

        backButton.onClick.AddListener(OnBack);
        reBeginButton.onClick.AddListener(OnAgain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Time.timeScale = 1;
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
        Time.timeScale = 1;
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
    }
    public override void OnExit()
    {
        //Debug.Log("style exit in");
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //transform.DOLocalMoveX(500, .5f).OnComplete(() => canvasGroup.alpha = 0);//OnComplete使用了lambda表达式
        base.OnExit();
    }
    public void OnAgain()
    {
        Time.timeScale = 1;
        //进入下一个场景前将所有面板出栈
        int count = uiMng.getStackCount();
        for (int i = 0; i < count; i++)
        {
            uiMng.PopPanel(); uiMng.clearDict();
            Debug.Log("pop successfully!!!");
        }
        DontDestroyOnLoad(settingMessage.Instance);
        //判断是重新开始哪一个游戏
        if (gameObject.scene.name == "storyGame")
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        else if (gameObject.scene.name == "fightGame")
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
    public void OnBack()
    {
        Time.timeScale = 1;
        //进入下一个场景前将所有面板出栈
        int count = uiMng.getStackCount();
        for (int i = 0; i < count; i++)
        {
            uiMng.PopPanel(); uiMng.clearDict();
            Debug.Log("pop successfully!!!");
        }
        DontDestroyOnLoad(settingMessage.Instance);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //SceneManager.LoadScene();
    }
}
