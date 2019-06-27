using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectTotalPart : MonoBehaviour
{
    //控制UI动画
    private Animator anim_m;
    private Animator anim_left;
    private Animator anim_right;
    private bool ifFirstIn = true;
    private int LeftOrRight = 0;//用于标明上一个上下操作的是上还是下，无0，上1，下2
    private bool changeAlready = false;
    private string[] actorName = {  "大乔","关羽", "孙悟空", "自定义" };
    private int index = 1;//中间的那个的下标
    private selectUnitPart unitPartCrt;
    //private GameObject child;

    //文字及缩略图改变
    private Text midMessage;
    private Text leftMessage;
    private Text rightMessage;
    private void Awake()
    {
        anim_m = transform.Find("middleImage").GetComponent<Animator>();
        anim_left = transform.Find("leftImage").GetComponent<Animator>();
        anim_right = transform.Find("rightImage").GetComponent<Animator>();
        midMessage = transform.Find("middleImage").Find("message").GetComponent<Text>();
        leftMessage = transform.Find("leftImage").Find("message").GetComponent<Text>();
        rightMessage = transform.Find("rightImage").Find("message").GetComponent<Text>();
        unitPartCrt = transform.Find(gameObject.name + "Child").GetComponent<selectUnitPart>();//动态找儿子

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //更改至自定义
        if (unitPartCrt.getIfChange() == 1)
        {
            if (ifFirstIn)
            {
                leftMessage.text = actorName[2];
                midMessage.text = actorName[3];
                rightMessage.text = actorName[0];
            }
            if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态为左面位置
            { midMessage.text = actorName[2]; }
            else if (anim_left.GetInteger("state") == 1 || anim_left.GetInteger("state") == 7)
            { leftMessage.text = actorName[2]; }
            else if (anim_right.GetInteger("state") == 1 || anim_right.GetInteger("state") == 7)
            { rightMessage.text = actorName[2]; }

            if (anim_m.GetInteger("state") == 3 || anim_m.GetInteger("state") == 6)//此状态为中间位置
            { midMessage.text = actorName[3]; }
            else if (anim_left.GetInteger("state") == 3 || anim_left.GetInteger("state") == 6)
            { leftMessage.text = actorName[3]; }
            else if (anim_right.GetInteger("state") == 3 || anim_right.GetInteger("state") == 6)
            { rightMessage.text = actorName[3]; }

            if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为右面位置
            { midMessage.text = actorName[0]; }
            else if (anim_left.GetInteger("state") == 2 || anim_left.GetInteger("state") == 8)
            { leftMessage.text = actorName[0]; }
            else if (anim_right.GetInteger("state") == 2 || anim_right.GetInteger("state") == 8)
            { rightMessage.text = actorName[0]; }

            index = 3;unitPartCrt.setIfChange(2);
        }
        //更改至指定角色
        if (unitPartCrt.ifAActor() != -1)
        {
            unitPartCrt.setIfChange(0);
            //012---102
            switch (unitPartCrt.ifAActor())
            {
                case 0:
                    if (ifFirstIn)
                    {
                        leftMessage.text = actorName[0];
                        midMessage.text = actorName[1];
                        rightMessage.text = actorName[2];
                    }
                    if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态为左面位置
                    { midMessage.text = actorName[0]; }
                    else if (anim_left.GetInteger("state") == 1 || anim_left.GetInteger("state") == 7)
                    { leftMessage.text = actorName[0]; }
                    else if (anim_right.GetInteger("state") == 1 || anim_right.GetInteger("state") == 7)
                    { rightMessage.text = actorName[0]; }

                    if (anim_m.GetInteger("state") == 3 || anim_m.GetInteger("state") == 6)//此状态为中间位置
                    { midMessage.text = actorName[1]; }
                    else if (anim_left.GetInteger("state") == 3 || anim_left.GetInteger("state") == 6)
                    { leftMessage.text = actorName[1]; }
                    else if (anim_right.GetInteger("state") == 3 || anim_right.GetInteger("state") == 6)
                    { rightMessage.text = actorName[1]; }

                    if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为右面位置
                    { midMessage.text = actorName[2]; }
                    else if (anim_left.GetInteger("state") == 2 || anim_left.GetInteger("state") == 8)
                    { leftMessage.text = actorName[2]; }
                    else if (anim_right.GetInteger("state") == 2 || anim_right.GetInteger("state") == 8)
                    { rightMessage.text = actorName[2]; }
                    index = 1;break;
                case 1:
                    if (ifFirstIn)
                    {
                        leftMessage.text = actorName[3];
                        midMessage.text = actorName[0];
                        rightMessage.text = actorName[1];
                    }
                    if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态为左面位置
                    { midMessage.text = actorName[3]; }
                    else if (anim_left.GetInteger("state") == 1 || anim_left.GetInteger("state") == 7)
                    { leftMessage.text = actorName[3]; }
                    else if (anim_right.GetInteger("state") == 1 || anim_right.GetInteger("state") == 7)
                    { rightMessage.text = actorName[3]; }

                    if (anim_m.GetInteger("state") == 3 || anim_m.GetInteger("state") == 6)//此状态为中间位置
                    { midMessage.text = actorName[0]; }
                    else if (anim_left.GetInteger("state") == 3 || anim_left.GetInteger("state") == 6)
                    { leftMessage.text = actorName[0]; }
                    else if (anim_right.GetInteger("state") == 3 || anim_right.GetInteger("state") == 6)
                    { rightMessage.text = actorName[0]; }

                    if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为右面位置
                    { midMessage.text = actorName[1]; }
                    else if (anim_left.GetInteger("state") == 2 || anim_left.GetInteger("state") == 8)
                    { leftMessage.text = actorName[1]; }
                    else if (anim_right.GetInteger("state") == 2 || anim_right.GetInteger("state") == 8)
                    { rightMessage.text = actorName[1]; }
                    index = 1; break;
                case 2:
                    if (ifFirstIn)
                    {
                        leftMessage.text = actorName[1];
                        midMessage.text = actorName[2];
                        rightMessage.text = actorName[3];
                    }
                    if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态为左面位置
                    { midMessage.text = actorName[1]; }
                    else if (anim_left.GetInteger("state") == 1 || anim_left.GetInteger("state") == 7)
                    { leftMessage.text = actorName[1]; }
                    else if (anim_right.GetInteger("state") == 1 || anim_right.GetInteger("state") == 7)
                    { rightMessage.text = actorName[1]; }

                    if (anim_m.GetInteger("state") == 3 || anim_m.GetInteger("state") == 6)//此状态为中间位置
                    { midMessage.text = actorName[2]; }
                    else if (anim_left.GetInteger("state") == 3 || anim_left.GetInteger("state") == 6)
                    { leftMessage.text = actorName[2]; }
                    else if (anim_right.GetInteger("state") == 3 || anim_right.GetInteger("state") == 6)
                    { rightMessage.text = actorName[2]; }

                    if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为右面位置
                    { midMessage.text = actorName[3]; }
                    else if (anim_left.GetInteger("state") == 2 || anim_left.GetInteger("state") == 8)
                    { leftMessage.text = actorName[3]; }
                    else if (anim_right.GetInteger("state") == 2 || anim_right.GetInteger("state") == 8)
                    { rightMessage.text = actorName[3]; }
                    index = 1; break;

                default:break;
            }
        }

        //接收按键事件
        if (!anim_left.IsInTransition(0))
        {
            if (gameObject.name == "P1")
            {
                if (Input.GetKeyDown(KeyCode.Q)) OnEnterLeft();
                else if (Input.GetKeyDown(KeyCode.E)) OnEnterRight();
            }
            else//P2复用代码
            {
                if (Input.GetKeyDown(KeyCode.Keypad2)) OnEnterLeft();
                else if (Input.GetKeyDown(KeyCode.Keypad3)) OnEnterRight();
            }
            //lockTime = 0.7f;
        }
    }
    public void OnEnterUp()
    {

        //TODO chang showImage/message/playerImage
        //确定选中的是哪一个并进行替换
    }
    public void OnEnterDown()
    {
        //TODO chang showImage/message/playerImage
        //确定选中的是哪一个并进行替换
    }
    public void OnEnterLeft()
    {
        if (LeftOrRight == 2)//如果上一次上下操作为右
        {
            anim_m.SetInteger("state", anim_m.GetInteger("state") - 5);
            anim_left.SetInteger("state", anim_left.GetInteger("state") - 5);
            anim_right.SetInteger("state", anim_right.GetInteger("state") - 5);
            changeAlready = true;
        }
        LeftOrRight = 1;//更新标记值


        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetInteger("state", 1);
                anim_left.SetInteger("state", 2);
                anim_right.SetInteger("state", 3);
            }
            else
            {
                //middleeeeeeeeeeeeeeeeeeeee
                if (anim_m.GetInteger("state") < 3)
                    anim_m.SetInteger("state", anim_m.GetInteger("state") + 1);
                else//状态机状态改变
                    anim_m.SetInteger("state", anim_m.GetInteger("state") - 2);

                //lefttttttttttttttttttttttt
                if (anim_left.GetInteger("state") < 3)
                    anim_left.SetInteger("state", anim_left.GetInteger("state") + 1);
                else//状态机状态改变
                    anim_left.SetInteger("state", anim_left.GetInteger("state") - 2);

                //rightttttttttttttttttttttt
                if (anim_right.GetInteger("state") < 3)
                    anim_right.SetInteger("state", anim_right.GetInteger("state") + 1);
                else//状态机状态改变
                    anim_right.SetInteger("state", anim_right.GetInteger("state") - 2);
            }
        }
        else changeAlready = false;

        //TODO chang child's showImage
        //更换字
        int changeIndex = 0;//应该换成的字的下标
        if (index + 2 < 4) changeIndex = index + 2;
        else
        {
            changeIndex = index + 2 - 4;
        }
        if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为右面位置
        { midMessage.text = actorName[changeIndex]; }
        else if (anim_left.GetInteger("state") == 2 || anim_left.GetInteger("state") == 8)
        { leftMessage.text = actorName[changeIndex]; }
        else if (anim_right.GetInteger("state") == 2 || anim_right.GetInteger("state") == 8)
        { rightMessage.text = actorName[changeIndex]; }
        //更新index
        index = changeIndex - 1 >= 0 ? changeIndex - 1 : changeIndex - 1 + 4;

        //改变图片
        unitPartCrt.changeTotally(index);
    }
    public void OnEnterRight()
    {
        if (LeftOrRight == 1)//上一个操作为左
        {
            anim_m.SetInteger("state", anim_m.GetInteger("state") + 5);
            anim_left.SetInteger("state", anim_left.GetInteger("state") + 5);
            anim_right.SetInteger("state", anim_right.GetInteger("state") + 5);
            changeAlready = true;
        }
        LeftOrRight = 2;//标记值

        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetInteger("state", 8);
                anim_left.SetInteger("state", 6);
                anim_right.SetInteger("state", 7);
            }
            else
            {
                //middleeeeeeeeeee
                if (anim_m.GetInteger("state") > 6)
                    anim_m.SetInteger("state", anim_m.GetInteger("state") - 1);
                else//状态机状态改变
                    anim_m.SetInteger("state", anim_m.GetInteger("state") + 2);

                //lefttttttttttttt
                if (anim_left.GetInteger("state") > 6)
                    anim_left.SetInteger("state", anim_left.GetInteger("state") - 1);
                else//状态机状态改变
                    anim_left.SetInteger("state", anim_left.GetInteger("state") + 2);

                //rightttttttttttt
                if (anim_right.GetInteger("state") > 6)
                    anim_right.SetInteger("state", anim_right.GetInteger("state") - 1);
                else//状态机状态改变
                    anim_right.SetInteger("state", anim_right.GetInteger("state") + 2);
            }
        }
        else changeAlready = false;

        //TODO chang child's showImage
        //更换字
        int changeIndex = 0;//应该换成的字的下标
        if (index - 2 >= 0) changeIndex = index - 2;
        else
        {
            changeIndex = index -2 + 4;
        }
        if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态为左面位置
        { midMessage.text = actorName[changeIndex]; }
        else if (anim_left.GetInteger("state") == 1 || anim_left.GetInteger("state") == 7)
        { leftMessage.text = actorName[changeIndex]; }
        else if (anim_right.GetInteger("state") == 1 || anim_right.GetInteger("state") == 7)
        { rightMessage.text = actorName[changeIndex]; }
        //更新index
        index = changeIndex + 1 < 4 ? changeIndex + 1 : changeIndex + 1 - 4;

        //改变图片
        unitPartCrt.changeTotally(index);
    }
}
