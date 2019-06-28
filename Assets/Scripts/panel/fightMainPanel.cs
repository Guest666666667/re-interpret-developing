using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fightMainPanel : BasePanel
{
    private Button stopButton;
    private CanvasGroup canvasGroup;
    private Rigidbody2D p1Rigid;
    private BoxCollider2D p1Bc;
    private Animator p1Animator;
    private PlayerControl p1c;
    private AnimationCreator p1Ac;

    private Rigidbody2D p2Rigid;
    private BoxCollider2D p2Bc;
    private Animator p2Animator;
    private Player2Control p2c;
    private AnimationCreator p2Ac;
    // Start is called before the first frame update
    void Start()
    {
        p1Rigid = GameObject.Find("/player1").GetComponent<Rigidbody2D>();
        p2Rigid = GameObject.Find("/player2").GetComponent<Rigidbody2D>();
        //p1Rigid.WakeUp(); p2Rigid.WakeUp();
        p1Rigid.gravityScale = 1;
        p2Rigid.gravityScale = 1;
        p1Bc = GameObject.Find("/player1").GetComponent<BoxCollider2D>();p1Bc.enabled = true;
        p1Animator = GameObject.Find("/player1").GetComponent<Animator>();p1Animator.enabled = true;
        p1c = GameObject.Find("/player1").GetComponent<PlayerControl>();p1c.enabled = true;
        p1Ac = GameObject.Find("/player1").GetComponent<AnimationCreator>();p1Ac.enabled = true;

        p2Bc = GameObject.Find("/player2").GetComponent<BoxCollider2D>(); p2Bc.enabled = true;
        p2Animator = GameObject.Find("/player2").GetComponent<Animator>(); p2Animator.enabled = true;
        p2c = GameObject.Find("/player2").GetComponent<Player2Control>(); p2c.enabled = true;
        p2Ac = GameObject.Find("/player2").GetComponent<AnimationCreator>(); p2Ac.enabled = true;
        GameObject.Find("/Grass").GetComponent<SpriteRenderer>().sortingOrder = 0;

        canvasGroup = GetComponent<CanvasGroup>();
        stopButton = transform.Find("stopButton").GetComponent<Button>();
        stopButton.onClick.AddListener(OnStopClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnEnter()
    {
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
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
        Time.timeScale = 1;
    }
    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //UIManager.Instance.PopPanel();
    }
    public void OnStopClick()
    {
        //uiMng.PushPanel(UIPanelType.select);
        uiMng.PushPanel(UIPanelType.gameSet);
        Time.timeScale = 0;
        //GameObject.Find("AudioManager").GetComponent<AudioManager>().Pause();
        //AudioManager.Instance.Pause();
    }
}
