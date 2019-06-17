using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class setPanel : BasePanel {
    private Button returnButton;
    private CanvasGroup canvasGroup;
    private Button musicOnButton;
    private Button soundOnButton;
    private Slider musicSlider;
    private Slider soundSlider;
    private bool onShow;//表示用户是否在进行设置
    //private settingMessage setting;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        returnButton = transform.Find("returnButton").GetComponent<Button>();
        musicOnButton = transform.Find("music").Find("musicOnButton").GetComponent<Button>();
        soundOnButton = transform.Find("sound").Find("soundOnButton").GetComponent<Button>();
        musicSlider = transform.Find("music").Find("musicSlider").GetComponent<Slider>();
        soundSlider = transform.Find("sound").Find("soundSlider").GetComponent<Slider>();

        //绑定
        returnButton.onClick.AddListener(OnClose);
        musicOnButton.onClick.AddListener(OnMusic);
        soundOnButton.onClick.AddListener(OnSound);

        //统一变量
        musicSlider.value = settingMessage.Instance.getMusicVolume();Debug.Log("getvalue:"+musicSlider.value);
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
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        //进入动画
        Vector3 temp = transform.localPosition;
        temp.x = 300;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, .5f);
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
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
        onShow = true;
    }
    public override void OnExit()
    {
        //Debug.Log("style exit in");
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        onShow = false;
        //transform.DOLocalMoveX(500, .5f).OnComplete(() => canvasGroup.alpha = 0);//OnComplete使用了lambda表达式
        base.OnExit();
    }
    public void OnClose()
    {
        uiMng.PopPanel();
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
}
