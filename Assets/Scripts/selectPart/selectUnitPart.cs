using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class selectUnitPart : MonoBehaviour
{
    //图片及文字改变
    private RawImage mid;
    private RawImage up;
    private RawImage down;
    private Text midMessage;
    private Text upMessage;
    private Text downMessage;//响应按键
    public Texture temp;

    //控制UI动画
    private Animator anim_m;
    private Animator anim_up;
    private Animator anim_down;
    private bool ifFirstIn = true;
    private int upOrdown = 0;//用于标明上一个上下操作的是上还是下，无0，上1，下2
    private bool changeAlready = false;
    private bool UIlock = false;

    //控制button透明度反馈，实际上是四个rawimage
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
    private Direction dir = Direction.LEFT;//初始

    private void Awake()
    {
        mid = transform.Find("middleShow").Find("showImage").GetComponent<RawImage>();
        up= transform.Find("upShow").Find("showImage").GetComponent<RawImage>();
        down = transform.Find("downShow").Find("showImage").GetComponent<RawImage>();
        midMessage = transform.Find("middleShow").Find("message").GetComponent<Text>();
        upMessage = transform.Find("upShow").Find("message").GetComponent<Text>();
        downMessage = transform.Find("downShow").Find("message").GetComponent<Text>();

        anim_m = transform.Find("middleShow").GetComponent<Animator>();
        anim_up = transform.Find("upShow").GetComponent<Animator>();
        anim_down = transform.Find("downShow").GetComponent<Animator>();

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
        if (!anim_m.IsInTransition(0))//如果没有在切换状态
        {
            //接收按键事件
            if (gameObject.name == "P1Child")
            {
                if (Input.GetKeyDown(KeyCode.W)) OnEnterUp();
                else if (Input.GetKeyDown(KeyCode.S)) OnEnterDown();
                else if (Input.GetKeyDown(KeyCode.A)) OnEnterLeft();
                else if (Input.GetKeyDown(KeyCode.D)) OnEnterRight();
            }
            else//P2复用代码
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) OnEnterUp();
                else if (Input.GetKeyDown(KeyCode.DownArrow)) OnEnterDown();
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) OnEnterLeft();
                else if (Input.GetKeyDown(KeyCode.RightArrow)) OnEnterRight();//if(Input.GetButtonDown())
            }
        }
    }
    public void OnEnterUp()
    {
        //TODO chang showImage/message/playerImage
        //确定选中的是哪一个并进行替换

        buttonUp.alpha = 1.0f;
        if (upOrdown == 2)//如果上一次上下操作为下
        {
            anim_m.SetFloat("state", anim_m.GetFloat("state") - 5);
            anim_up.SetFloat("state", anim_up.GetFloat("state") - 5);
            anim_down.SetFloat("state", anim_down.GetFloat("state") - 5);
            changeAlready = true;
        }upOrdown = 1;//更新标记值
        switch (dir)
        {
            case Direction.UP:break;
            case Direction.DOWN:buttonDown.alpha = defalutAlpha;break;
            case Direction.LEFT:buttonLeft.alpha = defalutAlpha;break;
            case Direction.RIGHT: buttonRight.alpha = defalutAlpha; break;
            default:break;
        }dir = Direction.UP;

        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetFloat("state", 0.1f);
                anim_up.SetFloat("state", 1.1f);
                anim_down.SetFloat("state", 2.1f);
            }
            else
            {
                //middleeeeeeeeeeeeeeeeeeeee
                if (anim_m.GetFloat("state") < 2.0f)
                    anim_m.SetFloat("state", anim_m.GetFloat("state") + 1);
                else//状态机状态改变
                    anim_m.SetFloat("state", anim_m.GetFloat("state") - 2);

                //upppppppppppppppppppppppppp
                if (anim_up.GetFloat("state") < 2.0f)
                    anim_up.SetFloat("state", anim_up.GetFloat("state") + 1);
                else//状态机状态改变
                    anim_up.SetFloat("state", anim_up.GetFloat("state") - 2);

                //downnnnnnnnnnnnnnnnnnnnnnnnnn
                if (anim_down.GetFloat("state") < 2.0f)
                    anim_down.SetFloat("state", anim_down.GetFloat("state") + 1);
                else//状态机状态改变
                    anim_down.SetFloat("state", anim_down.GetFloat("state") - 2);
            }
        }
        else changeAlready = false;


    }
    public void OnEnterDown()
    {
        //TODO chang showImage/message/playerImage
        //确定选中的是哪一个并进行替换


        buttonDown.alpha = 1.0f;
        if (upOrdown == 1)//上一个操作为上
        {
            anim_m.SetFloat("state", anim_m.GetFloat("state") + 5);
            anim_up.SetFloat("state", anim_up.GetFloat("state") + 5);
            anim_down.SetFloat("state", anim_down.GetFloat("state") + 5);
            changeAlready = true;
        }upOrdown = 2;//标记值
        switch (dir)
        {
            //判断上一个选择的按键
            case Direction.UP:buttonUp.alpha = defalutAlpha;break;
            case Direction.DOWN:  break;
            case Direction.LEFT: buttonLeft.alpha = defalutAlpha; break;
            case Direction.RIGHT: buttonRight.alpha = defalutAlpha; break;
            default: break;
        }
        dir = Direction.DOWN;

        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetFloat("state", 7.1f);
                anim_up.SetFloat("state", 5.1f);
                anim_down.SetFloat("state", 6.1f);
            }
            else
            {
                //middleeeeeeeeeee
                if (anim_m.GetFloat("state") > 6.0f)
                    anim_m.SetFloat("state", anim_m.GetFloat("state") - 1);
                else//状态机状态改变
                    anim_m.SetFloat("state", anim_m.GetFloat("state") + 2);

                //upppppppppppppppp
                if (anim_up.GetFloat("state") > 6.0f)
                    anim_up.SetFloat("state", anim_up.GetFloat("state") - 1);
                else//状态机状态改变
                    anim_up.SetFloat("state", anim_up.GetFloat("state") + 2);

                //downnnnnnnnnnnnnn
                if (anim_down.GetFloat("state") > 6.0f)
                    anim_down.SetFloat("state", anim_down.GetFloat("state") - 1);
                else//状态机状态改变
                    anim_down.SetFloat("state", anim_down.GetFloat("state") + 2);
            }
        }
        else changeAlready = false;


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

        //TODO chang showImage/message
        if (anim_m.GetFloat("state") == 0.0f) mid.texture = temp;
        else
        {
            if (anim_m.GetFloat("state") == 2.1f || anim_m.GetFloat("state") == 5.1f)
                anim_m.transform.Find("showImage").GetComponent<RawImage>().texture = temp;
            else if (anim_up.GetFloat("state") == 2.1f || anim_up.GetFloat("state") == 5.1f)
                anim_up.transform.Find("showImage").GetComponent<RawImage>().texture = temp;
            else if (anim_down.GetFloat("state") == 2.1f || anim_down.GetFloat("state") == 5.1f)
                anim_down.transform.Find("showImage").GetComponent<RawImage>().texture = temp;
        }

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

        //TODO chang showImage/message
    }

}
