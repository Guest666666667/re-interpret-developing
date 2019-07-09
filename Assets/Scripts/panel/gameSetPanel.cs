using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameSetPanel : BasePanel {
    private CanvasGroup canvasGroup;
    private Button returnButton;
    //private Button musicOnButton;
    //private Button soundOnButton;
    private Button backButton;
    private Button reBeginButton;
    //private Slider musicSlider;
    //private Slider soundSlider;
    private bool onShow;//用来判断音量是否需要更新
    private Image musicImage;
    private Image soundImage;

    // Use this for initialization
    private void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        returnButton = transform.Find("returnButton").GetComponent<Button>();
        backButton = transform.Find("backButton").GetComponent<Button>();
        reBeginButton = transform.Find("reBeginButton").GetComponent<Button>();
        musicImage = transform.Find("musicImage").GetComponent<Image>();
        soundImage = transform.Find("soundImage").GetComponent<Image>();
        //musicOnButton = transform.Find("music").Find("musicOnButton").GetComponent<Button>();
        //soundOnButton = transform.Find("sound").Find("soundOnButton").GetComponent<Button>();
        //musicSlider = transform.Find("music").Find("musicSlider").GetComponent<Slider>();
        //soundSlider = transform.Find("sound").Find("soundSlider").GetComponent<Slider>();

        //绑定
        returnButton.onClick.AddListener(OnClose);
        //musicOnButton.onClick.AddListener(OnMusic);
        //soundOnButton.onClick.AddListener(OnSound);
        backButton.onClick.AddListener(OnBack);
        reBeginButton.onClick.AddListener(OnAgain);
        //统一变量
        musicImage.fillAmount = settingMessage.Instance.getMusicVolume();
        soundImage.fillAmount = settingMessage.Instance.getSoundVolume();
        Debug.Log(settingMessage.Instance.getMusicVolume());
    }
    void Update()
    {
        //if (onShow)
        //{
        //    settingMessage.Instance.setMusicVolume(musicSlider.value);
        //    settingMessage.Instance.setSoundVolume(soundSlider.value);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnClose();
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
            if (temp.x > 775 * scale && temp.x < 1155 * scale)
            {
                if (temp.y > 358 * scale && temp.y < 438 * scale)
                {
                    //Debug.Log("mouse Funtion in!");
                    musicImage.fillAmount = (temp.x - 775 * scale) / (380 * scale);
                    settingMessage.Instance.setMusicVolume(musicImage.fillAmount);
                }
                else if (temp.y > 488 * scale && temp.y < 568 * scale)
                {
                    soundImage.fillAmount = (temp.x - 775 * scale) / (380 * scale);
                    settingMessage.Instance.setSoundVolume(soundImage.fillAmount);
                }
            }
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
    //public void OnMusic()
    //{
    //    musicSlider.value = 0;
    //}
    //public void OnSound()
    //{
    //    soundSlider.value = 0;//back.volume=soundSlider.value;
    //}
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
