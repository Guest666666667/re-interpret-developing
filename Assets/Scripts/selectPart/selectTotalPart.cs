using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectTotalPart : MonoBehaviour
{
    //控制UI动画
    private Animator anim_m;
    private Animator anim_left;
    private Animator anim_right;
    private bool ifFirstIn = true;
    private int LeftOrRight = 0;//用于标明上一个上下操作的是上还是下，无0，上1，下2
    private bool changeAlready = false;
    private selectUnitPart unitPartCrt;
    //private GameObject child;

    private void Awake()
    {
        anim_m = transform.Find("middleImage").GetComponent<Animator>();
        anim_left = transform.Find("leftImage").GetComponent<Animator>();
        anim_right = transform.Find("rightImage").GetComponent<Animator>();
        unitPartCrt = transform.Find(gameObject.name + "Child").GetComponent<selectUnitPart>();//动态找儿子

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
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
        if (LeftOrRight == 2)//如果上一次上下操作为下
        {
            anim_m.SetFloat("state", anim_m.GetFloat("state") - 5);
            anim_left.SetFloat("state", anim_left.GetFloat("state") - 5);
            anim_right.SetFloat("state", anim_right.GetFloat("state") - 5);
            changeAlready = true;
        }
        LeftOrRight = 1;//更新标记值


        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetFloat("state", 0.1f);
                anim_left.SetFloat("state", 1.1f);
                anim_right.SetFloat("state", 2.1f);
            }
            else
            {
                //middleeeeeeeeeeeeeeeeeeeee
                if (anim_m.GetFloat("state") < 2.0f)
                    anim_m.SetFloat("state", anim_m.GetFloat("state") + 1);
                else//状态机状态改变
                    anim_m.SetFloat("state", anim_m.GetFloat("state") - 2);

                //lefttttttttttttttttttttttt
                if (anim_left.GetFloat("state") < 2.0f)
                    anim_left.SetFloat("state", anim_left.GetFloat("state") + 1);
                else//状态机状态改变
                    anim_left.SetFloat("state", anim_left.GetFloat("state") - 2);

                //rightttttttttttttttttttttt
                if (anim_right.GetFloat("state") < 2.0f)
                    anim_right.SetFloat("state", anim_right.GetFloat("state") + 1);
                else//状态机状态改变
                    anim_right.SetFloat("state", anim_right.GetFloat("state") - 2);
            }
        }
        else changeAlready = false;

        //TODO chang child's showImage
    }
    public void OnEnterRight()
    {
        if (LeftOrRight == 1)//上一个操作为上
        {
            anim_m.SetFloat("state", anim_m.GetFloat("state") + 5);
            anim_left.SetFloat("state", anim_left.GetFloat("state") + 5);
            anim_right.SetFloat("state", anim_right.GetFloat("state") + 5);
            changeAlready = true;
        }
        LeftOrRight = 2;//标记值

        if (!changeAlready)
        {
            if (ifFirstIn)
            {
                ifFirstIn = false;
                anim_m.SetFloat("state", 7.1f);
                anim_left.SetFloat("state", 5.1f);
                anim_right.SetFloat("state", 6.1f);
            }
            else
            {
                //middleeeeeeeeeee
                if (anim_m.GetFloat("state") > 6.0f)
                    anim_m.SetFloat("state", anim_m.GetFloat("state") - 1);
                else//状态机状态改变
                    anim_m.SetFloat("state", anim_m.GetFloat("state") + 2);

                //lefttttttttttttt
                if (anim_left.GetFloat("state") > 6.0f)
                    anim_left.SetFloat("state", anim_left.GetFloat("state") - 1);
                else//状态机状态改变
                    anim_left.SetFloat("state", anim_left.GetFloat("state") + 2);

                //rightttttttttttt
                if (anim_right.GetFloat("state") > 6.0f)
                    anim_right.SetFloat("state", anim_right.GetFloat("state") - 1);
                else//状态机状态改变
                    anim_right.SetFloat("state", anim_right.GetFloat("state") + 2);
            }
        }
        else changeAlready = false;

        //TODO chang child's showImage
    }
}
