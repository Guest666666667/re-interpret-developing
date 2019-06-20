using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class selectPart : MonoBehaviour
{
    private RawImage bottom;
    private RawImage up;
    private RawImage down;
    private Text bottomMessage;
    private Text upMessage;
    private Text downMessage;//响应按键
    private Animator anim;

    private CanvasGroup buttonUp;
    private CanvasGroup buttonDown;
    private CanvasGroup buttonLeft;
    private CanvasGroup buttonRight;//通过更改透明度达到选择反馈的效果
    //public Texture2D turnUP;
    //public Texture2D turnDown;
    //public Texture2D turnLeft;
    //public Texture2D turnRight;

    public float defalutAlpha = 0.8f;
    enum Direction {UP,DOWN,LEFT,RIGHT};
    private Direction dir = Direction.DOWN;//初始为向下

    private void Awake()
    {
        bottom = transform.Find("middleShow").Find("middleShowImage").GetComponent<RawImage>();
        up= transform.Find("upShow").Find("upShowImage").GetComponent<RawImage>();
        down = transform.Find("downShow").Find("downShowImage").GetComponent<RawImage>();
        bottomMessage = transform.Find("middleShow").Find("message").GetComponent<Text>();
        upMessage = transform.Find("upShow").Find("message").GetComponent<Text>();
        downMessage = transform.Find("downShow").Find("message").GetComponent<Text>();
        anim = transform.parent.GetChild(1).GetComponent<Animator>();

        buttonUp = transform.Find("Button").Find("up").GetComponent<CanvasGroup>();
        buttonDown = transform.Find("Button").Find("down").GetComponent<CanvasGroup>();
        buttonLeft = transform.Find("Button").Find("left").GetComponent<CanvasGroup>();
        buttonRight = transform.Find("Button").Find("right").GetComponent<CanvasGroup>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //highLight = transform.GetComponent<Button>();
        //highLight.Select();

    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (Input.GetButtonDown("w")) OnEnterUp();
        else if (Input.GetButtonDown("s")) OnEnterDown();
        else if (Input.GetButtonDown("a")) OnEnterLeft();
        else if (Input.GetButtonDown("d")) OnEnterRight();
    }
    public void OnEnterUp()
    {
        buttonUp.alpha = 1.0f;
        switch (dir)
        {
            case Direction.UP:break;
            case Direction.DOWN: buttonDown.alpha = defalutAlpha;break;
            case Direction.LEFT:buttonLeft.alpha = defalutAlpha;break;
            case Direction.RIGHT: buttonRight.alpha = defalutAlpha; break;
            default:break;
        }dir = Direction.UP;
        if (anim.GetFloat("state") < 2.0f)
        {
            anim.SetFloat("state", anim.GetFloat("state") + 1);
            Debug.Log("++ in");
        }
        else//状态机状态改变
        {
            anim.SetFloat("state", anim.GetFloat("state") -2);
        }


    }
    public void OnEnterDown()
    {
        buttonDown.alpha = 1.0f;
        switch (dir)
        {
            case Direction.UP: buttonUp.alpha = defalutAlpha;break;
            case Direction.DOWN:  break;
            case Direction.LEFT: buttonLeft.alpha = defalutAlpha; break;
            case Direction.RIGHT: buttonRight.alpha = defalutAlpha; break;
            default: break;
        }
        dir = Direction.DOWN;
        if (anim.GetFloat("state") > 1.0f) 
        {
            anim.SetFloat("state", anim.GetFloat("state") - 1);
        }
        else//状态机状态改变
        {
            anim.SetFloat("state", anim.GetFloat("state") + 2);
        }
    }
    public void OnEnterLeft()
    {
        buttonLeft.alpha = 1.0f;
        switch (dir)
        {
            case Direction.UP:  buttonUp.alpha = defalutAlpha;break;
            case Direction.DOWN: buttonDown.alpha = defalutAlpha; break;
            case Direction.LEFT: break;
            case Direction.RIGHT: buttonRight.alpha = defalutAlpha; break;
            default: break;
        }
        dir = Direction.LEFT;
        anim.SetTrigger("left");
    }
    public void OnEnterRight()
    {
        buttonRight.alpha = 1.0f;
        switch (dir)
        {
            case Direction.UP: buttonUp.alpha = defalutAlpha;break;
            case Direction.DOWN: buttonDown.alpha = defalutAlpha; break;
            case Direction.LEFT: buttonLeft.alpha = defalutAlpha; break;
            case Direction.RIGHT:  break;
            default: break;
        }
        dir = Direction.RIGHT;
    }
}
