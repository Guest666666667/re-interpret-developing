using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameSetPanel : BasePanel {
    private CanvasGroup canvasGroup;
    private Button returnButton;
    private Button musicOnButton;
    private Button soundOnButton;
    private Button backButton;
    private Slider musicSlider;
    private Slider soundSlider;
    private bool onShow;//用来判断音量是否需要更新

    // Use this for initialization
    private void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        returnButton = transform.Find("returnButton").GetComponent<Button>();
        backButton = transform.Find("backButton").GetComponent<Button>();
        musicOnButton = transform.Find("music").Find("musicOnButton").GetComponent<Button>();
        soundOnButton = transform.Find("sound").Find("soundOnButton").GetComponent<Button>();
        musicSlider = transform.Find("music").Find("musicSlider").GetComponent<Slider>();
        soundSlider = transform.Find("sound").Find("soundSlider").GetComponent<Slider>();

        //绑定
        returnButton.onClick.AddListener(OnClose);
        musicOnButton.onClick.AddListener(OnMusic);
        soundOnButton.onClick.AddListener(OnSound);
        backButton.onClick.AddListener(OnBack);
        //统一变量
        musicSlider.value = settingMessage.Instance.getMusicVolume();
        soundSlider.value = settingMessage.Instance.getSoundVolume();
    }
    void Update()
    {
        if (onShow)
        {
            settingMessage.Instance.setMusicVolume(musicSlider.value);
            settingMessage.Instance.setSoundVolume(soundSlider.value);
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        //if (returnButton == null)
        //{
        //    returnButton = transform.Find("returnButton").GetComponent<Button>();
        //    returnButton.onClick.AddListener(OnClose);
        //}
        //if (musicOnButton == null)
        //{
        //    musicOnButton = transform.Find("music").Find("musicOnButton").GetComponent<Button>();
        //    musicOnButton.onClick.AddListener(OnMusic);
        //}
        //if (backButton == null)
        //{
        //    backButton = transform.Find("backButton").GetComponent<Button>();
        //    backButton.onClick.AddListener(OnBack);
        //}
        Time.timeScale = 1;
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        //进入动画
        //Vector3 temp = transform.localPosition;
        //temp.x = 300;
        //transform.localPosition = temp;
        //transform.DOLocalMoveX(0, .5f);
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

        AudioManager.Instance.Resume();
        //Debug.Log("pop already");
    }
    public void OnMusic()
    {
        musicSlider.value = 0;
    }
    public void OnSound()
    {
        soundSlider.value = 0;//back.volume=soundSlider.value;
    }
    public void OnBack()
    {
        Time.timeScale = 1;
        //进入下一个场景前将所有面板出栈
        int count = uiMng.getStackCount();
        for (int i = 0; i < count; i++)
        {
            uiMng.PopPanel();uiMng.clearDict();
            Debug.Log("pop successfully!!!");
        }
        DontDestroyOnLoad(settingMessage.Instance);
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        //SceneManager.LoadScene();
    }
}
