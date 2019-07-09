using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class setPanel : BasePanel {
    private Button returnButton;
    private CanvasGroup canvasGroup;
    private Image musicImage;
    private Image soundImage;
    private bool onShow;//表示用户是否在进行设置
    //private settingMessage setting;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        returnButton = transform.Find("returnButton").GetComponent<Button>();
        musicImage= transform.Find("musicImage").GetComponent<Image>();
        soundImage = transform.Find("soundImage").GetComponent<Image>();

        //绑定
        returnButton.onClick.AddListener(OnClose);

        ////统一变量
        musicImage.fillAmount = settingMessage.Instance.getMusicVolume();
        soundImage.fillAmount = settingMessage.Instance.getSoundVolume();
    }
    void Update()
    {

        //if (onShow)
        //{
        //    settingMessage.Instance.setMusicVolume(musicSlider.value);
        //    settingMessage.Instance.setSoundVolume(soundSlider.value);
        //}
    }
    void OnGUI()
    {
        Event m_Event = Event.current;
        float width = Screen.width;
        float scale = width / 1920;
        if (Input.GetMouseButton(0))
        {
            Vector2 temp = m_Event.mousePosition;
            //Debug.Log(temp);
            if (temp.x > 720*scale && temp.x < 1210*scale)
            {
                if (temp.y > 415 * scale && temp.y < 515 * scale)
                {
                    //Debug.Log("mouse Funtion in!");
                    musicImage.fillAmount = (temp.x -720f * scale) / (490f * scale);
                    settingMessage.Instance.setMusicVolume(musicImage.fillAmount);
                }
                else if (temp.y > 585 * scale && temp.y < 685 * scale)
                {
                    soundImage.fillAmount = (temp.x -720f * scale) / (490f * scale);
                    settingMessage.Instance.setSoundVolume(soundImage.fillAmount);
                }
            }
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
    //public void OnMusic()
    //{
    //    musicSlider.value = 0;
    //}
    //public void OnSound()
    //{
    //    soundSlider.value = 0;//back.volume=soundSlider.value;
    //}
}
