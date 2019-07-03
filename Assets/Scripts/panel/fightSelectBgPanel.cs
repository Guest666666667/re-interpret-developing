using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fightSelectBgPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private Animator P1anim_m;
    private Animator P2anim_m;
    //双方是否点击了确定
    private bool P1Enter = false;
    private bool P2Enter = false;
    //随机天气
    private Image weather;
    private GameObject random;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        P1anim_m = transform.Find("farBG").Find("middleImage").GetComponent<Animator>();
        P2anim_m = transform.Find("nearBG").Find("middleImage").GetComponent<Animator>();
        GameObject.Find("/player1").transform.Translate(4f, 0, 0);
        GameObject.Find("/player2").transform.Translate(4f, 0, 0);

        weather = transform.Find("weather").GetComponent<Image>();
        random = transform.Find("weather/random").gameObject;
        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, 3);
            weather.sprite = random.transform.GetChild(index).GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (!P1anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.J)) P1Enter = true;
        if (!P2anim_m.IsInTransition(0) && Input.GetKeyDown(KeyCode.Keypad1)) P2Enter = true;
        if (P1Enter && P2Enter) OnMakeSure();
    }
    public override void OnEnter()
    {
        if (canvasGroup == null)
        {
            canvasGroup = transform.GetComponent<CanvasGroup>();
        }
        base.OnEnter();
    }
    public override void OnPause()
    {
        //transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
        gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
        //transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(1).gameObject.SetActive(true);
        gameObject.SetActive(true);
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
        P1Enter = false;
        P2Enter = false;
        Animation anim = transform.Find("weather").GetComponent<Animation>();
        anim.Play("turn-edge");
        int index = Random.Range(0, 3);
        weather.sprite = random.transform.GetChild(index).GetComponent<SpriteRenderer>().sprite;

        while (anim.IsPlaying("turn-edge"))
        {

        };
        uiMng.PushPanel(UIPanelType.fightMain);
    }
}
