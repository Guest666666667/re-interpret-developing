using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;
using UnityEditor;

public class selectUnitPart : MonoBehaviour
{
    //图片及文字改变
    private Image mid;
    private Image up;
    private Image down;
    private Text midMessage;
    private Text upMessage;
    private Text downMessage;//响应按键
    public Texture2D[] temp;
    private int[] currPartIndex= { 7,0,1};//1号位为左右改变时需要的参数，部件编号,上中下
    private int[] index = { 0, 0, 0, 0, 0, 0, 0, 0 };//8个部件的人物编号，即部件的序列编号，头一头二
    //private int[] myStyleIndex = { 0, 2, 1, 1, 2, 0, 1, 1 };//自定义的编号记录
    private string[] modelName = { "GuanYu","DaQiao", "MonkeyMan" };
    private string[] message = { "头", "胸", "腹", "前臂", "后臂", "手", "左腿", "右腿" };
    private float delayTime = 0.25f;//延时更改
    private bool delayBegin = false;
    private int ifChange = 0;//是否修改，给父对象判断其是否需要将目前状态转为“自定义”,1为需要修改，之后按忽略
    //private GameObject select;

    //人物组件
    private SpriteMeshInstance[] P1Sprite= new SpriteMeshInstance[11];
    private SpriteMeshInstance[] P2Sprite = new SpriteMeshInstance[11];
    //控制UI动画
    private Animator anim_m;
    private Animator anim_up;
    private Animator anim_down;
    private bool ifFirstIn = true;
    private int upOrdown = 0;//用于标明上一个上下操作的是上还是下，无0，上1，下2
    private bool changeAlready = false;

    //控制button透明度反馈，实际上是四个rawimage
    private CanvasGroup buttonUp;
    private CanvasGroup buttonDown;
    private CanvasGroup buttonLeft;
    private CanvasGroup buttonRight;//通过更改透明度达到选择反馈的效果

    public float defaultAlpha = 0.8f;
    enum Direction {UP,DOWN,LEFT,RIGHT};
    private Direction dir = Direction.LEFT;//初始

    private void Awake()
    {
        mid = transform.Find("middleShow").Find("showImage").GetComponent<Image>();
        up = transform.Find("upShow").Find("showImage").GetComponent<Image>();
        down = transform.Find("downShow").Find("showImage").GetComponent<Image>();
        midMessage = transform.Find("middleShow").Find("message").GetComponent<Text>();
        upMessage = transform.Find("upShow").Find("message").GetComponent<Text>();
        downMessage = transform.Find("downShow").Find("message").GetComponent<Text>();
        //select = transform.Find("select").GetComponent<GameObject>();

        anim_m = transform.Find("middleShow").GetComponent<Animator>();
        anim_up = transform.Find("upShow").GetComponent<Animator>();
        anim_down = transform.Find("downShow").GetComponent<Animator>();

        buttonUp = transform.Find("Button").Find("up").GetComponent<CanvasGroup>();
        buttonDown = transform.Find("Button").Find("down").GetComponent<CanvasGroup>();
        buttonLeft = transform.Find("Button").Find("left").GetComponent<CanvasGroup>();
        buttonRight = transform.Find("Button").Find("right").GetComponent<CanvasGroup>();

        for (int i = 0; i < 11; i++)
        {
            //P1Sprite[i] = transform.parent.parent.parent.Find("player1").Find("model").GetChild(i).GetComponent<SpriteMeshInstance>();
            P1Sprite[i] = GameObject.Find("/player1/model").transform.GetChild(i).GetComponent<SpriteMeshInstance>();
        }
        for (int i = 0; i < 11; i++)
        {
            //P2Sprite[i] = transform.parent.parent.parent.Find("player2").Find("model").GetChild(i).GetComponent<SpriteMeshInstance>();
            P2Sprite[i] = GameObject.Find("/player2/model").transform.GetChild(i).GetComponent<SpriteMeshInstance>();
        }
        //P1Sprite[0].spriteMesh= Resources.Load<SpriteMesh>("Model/DaQiao/SpriteMesh/头");
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
        //if(delayBegin)
        //delayTime -= Time.deltaTime;
        if (!anim_m.IsInTransition(0))//如果没有在切换状态
        {
            //接收按键事件
            if (gameObject.name == "P1Child")
            {
                if (Input.GetKeyDown(KeyCode.W)) OnEnterUp();
                else if (Input.GetKeyDown(KeyCode.S)) OnEnterDown();
                else if (Input.GetKeyDown(KeyCode.A)) OnEnterLeft(1);
                else if (Input.GetKeyDown(KeyCode.D)) OnEnterRight(1);
            }
            else//P2复用代码
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) OnEnterUp();
                else if (Input.GetKeyDown(KeyCode.DownArrow)) OnEnterDown();
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) OnEnterLeft(2);
                else if (Input.GetKeyDown(KeyCode.RightArrow)) OnEnterRight(2);//if(Input.GetButtonDown())
            }
        }
    }
    public void OnEnterUp()
    {
        //int tempIndex = currPartIndex[1];//上下相当于确定，在执行上下运动之前的中间位置为确定的对象
        buttonUp.alpha = 1.0f;
        if (upOrdown == 2)//如果上一次上下操作为下
        {
            anim_m.SetInteger("state", anim_m.GetInteger("state") - 5);
            anim_up.SetInteger("state", anim_up.GetInteger("state") - 5);
            anim_down.SetInteger("state", anim_down.GetInteger("state") - 5);
            changeAlready = true;
        }upOrdown = 1;//更新标记值
        switch (dir)
        {
            case Direction.UP:break;
            case Direction.DOWN:buttonDown.alpha = defaultAlpha; break;
            case Direction.LEFT:buttonLeft.alpha = defaultAlpha; break;
            case Direction.RIGHT: buttonRight.alpha = defaultAlpha; break;
            default:break;
        }dir = Direction.UP;

        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetInteger("state", 1);
                anim_up.SetInteger("state", 2);
                anim_down.SetInteger("state", 3);
            }
            else
            {
                //middleeeeeeeeeeeeeeeeeeeee
                if (anim_m.GetInteger("state") < 3)
                    anim_m.SetInteger("state", anim_m.GetInteger("state") + 1);
                else//状态机状态改变
                    anim_m.SetInteger("state", anim_m.GetInteger("state") - 2);

                //upppppppppppppppppppppppppp
                if (anim_up.GetInteger("state") < 3)
                    anim_up.SetInteger("state", anim_up.GetInteger("state") + 1);
                else//状态机状态改变
                    anim_up.SetInteger("state", anim_up.GetInteger("state") - 2);

                //downnnnnnnnnnnnnnnnnnnnnnnnnn
                if (anim_down.GetInteger("state") < 3)
                    anim_down.SetInteger("state", anim_down.GetInteger("state") + 1);
                else//状态机状态改变
                    anim_down.SetInteger("state", anim_down.GetInteger("state") - 2);
            }
        }
        else { changeAlready = false; }

        //TODO chang showImage/message
        //确定选中的是哪一个并进行替换,替换的应该是现在位置在最下面的
        int tempInt = 0;
        if (currPartIndex[0] + 3 <= 7)
        {
            currPartIndex[0] += 3;
            tempInt = currPartIndex[0];
            currPartIndex[0] = currPartIndex[1];
            currPartIndex[1] = currPartIndex[2];
            currPartIndex[2] = tempInt;
        }
        else
        {
            currPartIndex[0] = currPartIndex[0] + 3 - 8;
            tempInt = currPartIndex[0];
            currPartIndex[0] = currPartIndex[1];
            currPartIndex[1] = currPartIndex[2];
            currPartIndex[2] = tempInt;
        }
        //Debug.Log("index1:" + index[currPartIndex[2]] + "   index2:" + currPartIndex[2]);
        Sprite temp = transform.Find("select").GetChild(index[currPartIndex[2]]).GetChild(currPartIndex[2]).GetComponent<SpriteRenderer>().sprite;

        //选择更改的对象并更改
        //TODO 给他一个延迟，提高使用体验
        if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为下面位置
        { mid.sprite = temp; midMessage.text = message[currPartIndex[2]]; }
        else if (anim_up.GetInteger("state") == 2 || anim_up.GetInteger("state") == 8)
        { up.sprite = temp; upMessage.text = message[currPartIndex[2]]; }
        else if (anim_down.GetInteger("state") == 2 || anim_down.GetInteger("state") == 8)
        { down.sprite = temp; downMessage.text = message[currPartIndex[2]]; }

    }
    public void OnEnterDown()
    {
        buttonDown.alpha = 1.0f;
        if (upOrdown == 1)//上一个操作为上
        {
            anim_m.SetInteger("state", anim_m.GetInteger("state") + 5);
            anim_up.SetInteger("state", anim_up.GetInteger("state") + 5);
            anim_down.SetInteger("state", anim_down.GetInteger("state") + 5);
            changeAlready = true;
        }upOrdown = 2;//标记值
        switch (dir)
        {
            //判断上一个选择的按键
            case Direction.UP:buttonUp.alpha = defaultAlpha; break;
            case Direction.DOWN:  break;
            case Direction.LEFT: buttonLeft.alpha = defaultAlpha; break;
            case Direction.RIGHT: buttonRight.alpha = defaultAlpha; break;
            default: break;
        }
        dir = Direction.DOWN;

        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetInteger("state", 8);
                anim_up.SetInteger("state", 6);
                anim_down.SetInteger("state", 7);
            }
            else
            {
                //middleeeeeeeeeee
                if (anim_m.GetInteger("state") > 6)
                    anim_m.SetInteger("state", anim_m.GetInteger("state") - 1);
                else//状态机状态改变
                    anim_m.SetInteger("state", anim_m.GetInteger("state") + 2);

                //upppppppppppppppp
                if (anim_up.GetInteger("state") > 6)
                    anim_up.SetInteger("state", anim_up.GetInteger("state") - 1);
                else//状态机状态改变
                    anim_up.SetInteger("state", anim_up.GetInteger("state") + 2);

                //downnnnnnnnnnnnnn
                if (anim_down.GetInteger("state") > 6)
                    anim_down.SetInteger("state", anim_down.GetInteger("state") - 1);
                else//状态机状态改变
                    anim_down.SetInteger("state", anim_down.GetInteger("state") + 2);
            }
        }
        else changeAlready = false;

        int tempInt = 0;
        if (currPartIndex[2] - 3 >= 0)
        {
            currPartIndex[2] -= 3;
            tempInt = currPartIndex[2];
            currPartIndex[2] = currPartIndex[1];
            currPartIndex[1] = currPartIndex[0];
            currPartIndex[0] = tempInt;
        }
        else
        {
            currPartIndex[2] = currPartIndex[2] - 3 + 8;
            tempInt = currPartIndex[2];
            currPartIndex[2] = currPartIndex[1];
            currPartIndex[1] = currPartIndex[0];
            currPartIndex[0] = tempInt;
        }
        Sprite temp = transform.Find("select").GetChild(index[currPartIndex[0]]).GetChild(currPartIndex[0]).GetComponent<SpriteRenderer>().sprite;
        //选择更改的对象并更改图片和信息
        if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态在上面位置
        { mid.sprite = temp; midMessage.text = message[currPartIndex[0]]; }
        else if (anim_up.GetInteger("state") == 1 || anim_up.GetInteger("state") == 7)
        { up.sprite = temp; upMessage.text = message[currPartIndex[0]]; }
        else if (anim_down.GetInteger("state") == 1 || anim_down.GetInteger("state") == 7)
        { down.sprite = temp; downMessage.text = message[currPartIndex[0]]; }
    }
    public void OnEnterLeft(int player)
    {
        buttonLeft.alpha = 1.0f;
        switch (dir)
        {
            case Direction.UP:  buttonUp.alpha = defaultAlpha; break;
            case Direction.DOWN: buttonDown.alpha = defaultAlpha; break;
            case Direction.LEFT: break;
            case Direction.RIGHT: buttonRight.alpha = defaultAlpha; break;
            default: break;
        }
        dir = Direction.LEFT;

        //TODO chang showImage/message
        //记录更改图片
        int index1 = 0;
        if (index[currPartIndex[1]] - 1 >= 0)
        {
            index1 = --index[currPartIndex[1]];
        }
        else
        {
            index1 = 2; index[currPartIndex[1]] = 2;
        }
        Sprite temp= transform.Find("select").GetChild(index1).GetChild(currPartIndex[1]).GetComponent<SpriteRenderer>().sprite;
        //选择更改的对象并更改
        if (anim_m.GetInteger("state") == 0) mid.sprite = temp;//中间位置
        else
        {
            if (anim_m.GetInteger("state") == 3 || anim_m.GetInteger("state") == 6)
                mid.sprite = temp;
            else if (anim_up.GetInteger("state") == 3 || anim_up.GetInteger("state") == 6)
                up.sprite = temp;
            else if (anim_down.GetInteger("state") == 3 || anim_down.GetInteger("state") == 6)
                down.sprite = temp;
        }

        //修改了东西，要叫爸爸改成“自定义”
        ifChange++;
        //Debug.Log(ifChange);
        //更改SpriteMesh
        if (player == 1)
        {
            if (currPartIndex[1] < 3)//小于3的直接对应
                //P1Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/DaQiao/");
                P1Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/"+ modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            else if (currPartIndex[1] >= 3 && currPartIndex[1] <= 5)
            {
                P1Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
                P1Sprite[currPartIndex[1]+3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
            else if (currPartIndex[1] > 5)
            {
                P1Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
        }
        else
        {
            if (currPartIndex[1] < 3)//小于3的直接对应
                P2Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            else if (currPartIndex[1] >= 3 && currPartIndex[1] <= 5)
            {
                P2Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
                P2Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
            else if (currPartIndex[1] > 5)
            {
                P2Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
        }
    }
    public void OnEnterRight(int player)
    {
        buttonRight.alpha = 1.0f;
        switch (dir)
        {
            case Direction.UP: buttonUp.alpha = defaultAlpha; break;
            case Direction.DOWN: buttonDown.alpha = defaultAlpha; break;
            case Direction.LEFT: buttonLeft.alpha = defaultAlpha; break;
            case Direction.RIGHT:  break;
            default: break;
        }
        dir = Direction.RIGHT;

        //TODO chang showImage/message
        //记录更改图片
        int index1 = 0;
        if (index[currPartIndex[1]] + 1 <= 2)
        {
            index1 = ++index[currPartIndex[1]];
        }
        else
        {
            index1 = 0; index[currPartIndex[1]] = 0;
        }
        Sprite temp = transform.Find("select").GetChild(index1).GetChild(currPartIndex[1]).GetComponent<SpriteRenderer>().sprite;
        //选择更改的对象并更改
        if (anim_m.GetInteger("state") == 0) mid.sprite = temp;
        else
        {
            if (anim_m.GetInteger("state") == 3 || anim_m.GetInteger("state") == 6)
                mid.sprite = temp;
            else if (anim_up.GetInteger("state") == 3 || anim_up.GetInteger("state") == 6)
                up.sprite = temp;
            else if (anim_down.GetInteger("state") == 3 || anim_down.GetInteger("state") == 6)
                down.sprite = temp;
        }
        //修改了东西，要叫爸爸改成“自定义”
        ifChange++;

        //更改SpriteMesh
        if (player == 1)
        {
            if (currPartIndex[1] < 3)//小于3的直接对应
                P1Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            else if (currPartIndex[1] >= 3 && currPartIndex[1] <= 5)
            {
                P1Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
                P1Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
            else if (currPartIndex[1] > 5)
            {
                P1Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
        }
        else
        {
            if (currPartIndex[1] < 3)//小于3的直接对应
                P2Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            else if (currPartIndex[1] >= 3 && currPartIndex[1] <= 5)
            {
                P2Sprite[currPartIndex[1]].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
                P2Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
            else if (currPartIndex[1] > 5)
            {
                P2Sprite[currPartIndex[1] + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[index[currPartIndex[1]]] + "/SpriteMesh/" + message[currPartIndex[1]]);
            }
        }
    }
    public Texture2D spriteToTexture(Sprite sprite)
    {
        //sprite为图集中的某个子Sprite对象
        var targetTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height);
        targetTex.SetPixels(pixels);
        targetTex.Apply();
        return targetTex;
    }
    public int getIfChange()
    {
        return ifChange;
    }
    public void setIfChange(int a)
    {
        ifChange = a;
    }
    public void changeTotally(int totalIndex,int player)
    {
        Sprite temp;
        switch (totalIndex)
        {
            case 0://大乔
                //for (int j = 0; j < 8; j++)
                //{
                //    myStyleIndex[j] = index[j];//储存好自定义的
                //}
                for (int i = 0; i < 8; i++)
                {
                    index[i] = 1;//大乔下标
                }
                temp= transform.Find("select").GetChild(1).GetChild(currPartIndex[0]).GetComponent<SpriteRenderer>().sprite;
                up.sprite = temp;
                temp = transform.Find("select").GetChild(1).GetChild(currPartIndex[1]).GetComponent<SpriteRenderer>().sprite;
                mid.sprite = temp;
                temp = transform.Find("select").GetChild(1).GetChild(currPartIndex[2]).GetComponent<SpriteRenderer>().sprite;
                down.sprite = temp;

                if (player == 1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)//小于3的直接对应
                            P1Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                        else if (i >= 3 && currPartIndex[1] <= 5)
                        {
                            P1Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                            P1Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                        }
                        else if (i > 5)
                        {
                            P1Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)//小于3的直接对应
                            P2Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                        else if (i >= 3 && currPartIndex[1] <= 5)
                        {
                            P2Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                            P2Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                        }
                        else if (i > 5)
                        {
                            P2Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[1] + "/SpriteMesh/" + message[i]);
                        }
                    }
                }
                //回到最开始，ifChange改回0
                ifChange = 0;
                break;
            case 1://关羽
                for (int i = 0; i < 8; i++)
                {
                    index[i] = 0;//关羽下标
                }
                temp = transform.Find("select").GetChild(0).GetChild(currPartIndex[0]).GetComponent<SpriteRenderer>().sprite;
                up.sprite = temp;
                temp = transform.Find("select").GetChild(0).GetChild(currPartIndex[1]).GetComponent<SpriteRenderer>().sprite;
                mid.sprite = temp;
                temp = transform.Find("select").GetChild(0).GetChild(currPartIndex[2]).GetComponent<SpriteRenderer>().sprite;
                down.sprite = temp;

                if (player == 1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)//小于3的直接对应
                            P1Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                        else if (i >= 3 && currPartIndex[1] <= 5)
                        {
                            P1Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                            P1Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                        }
                        else if (i > 5)
                        {
                            P1Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)//小于3的直接对应
                            P2Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                        else if (i >= 3 && currPartIndex[1] <= 5)
                        {
                            P2Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                            P2Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                        }
                        else if (i > 5)
                        {
                            P2Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[0] + "/SpriteMesh/" + message[i]);
                        }
                    }
                }
                //回到最开始，ifChange改回0
                ifChange = 0;
                break;
            case 2://孙悟空
                for (int i = 0; i < 8; i++)
                {
                    index[i] = 2;//孙悟空下标
                }
                temp = transform.Find("select").GetChild(2).GetChild(currPartIndex[0]).GetComponent<SpriteRenderer>().sprite;
                up.sprite = temp;
                temp = transform.Find("select").GetChild(2).GetChild(currPartIndex[1]).GetComponent<SpriteRenderer>().sprite;
                mid.sprite = temp;
                temp = transform.Find("select").GetChild(2).GetChild(currPartIndex[2]).GetComponent<SpriteRenderer>().sprite;
                down.sprite = temp;

                if (player == 1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)//小于3的直接对应
                            P1Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                        else if (i >= 3 && currPartIndex[1] <= 5)
                        {
                            P1Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                            P1Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                        }
                        else if (i > 5)
                        {
                            P1Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)//小于3的直接对应
                            P2Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                        else if (i >= 3 && currPartIndex[1] <= 5)
                        {
                            P2Sprite[i].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                            P2Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                        }
                        else if (i > 5)
                        {
                            P2Sprite[i + 3].spriteMesh = Resources.Load<SpriteMesh>("Model/" + modelName[2] + "/SpriteMesh/" + message[i]);
                        }
                    }
                }
                //回到最开始，ifChange改回0
                ifChange = 0;
                break;
            case 3://自定义
                //TODO 维护一个自定义的index，在之后回到自定义时能显示之前自定义的样子
                //temp = transform.Find("select").GetChild(index[currPartIndex[0]]).GetChild(currPartIndex[0]).GetComponent<SpriteRenderer>().sprite;
                //up.sprite = temp;
                //temp = transform.Find("select").GetChild(index[currPartIndex[1]]).GetChild(currPartIndex[1]).GetComponent<SpriteRenderer>().sprite;
                //mid.sprite = temp;
                //temp = transform.Find("select").GetChild(index[currPartIndex[2]]).GetChild(currPartIndex[2]).GetComponent<SpriteRenderer>().sprite;
                //down.sprite = temp;
                break;
            default:break;
        }
    }
    public int ifAActor()
    {
        int temp = index[0];
        for (int i = 1; i < 8; i++)
        {
            if (index[i] != temp)
            {
                return -1;//表明没有
            }
        }
        return temp;//一致的序号
    }
}
