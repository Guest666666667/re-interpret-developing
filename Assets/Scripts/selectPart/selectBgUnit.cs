﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectBgUnit : MonoBehaviour
{
    //控制UI动画
    private Animator anim_m;
    private Animator anim_left;
    private Animator anim_right;
    private bool ifFirstIn = true;
    private int LeftOrRight = 0;//用于标明上一个上下操作的是上还是下，无0，上1，下2
    private bool changeAlready = false;
    //private GameObject child;

    private void Awake()
    {
        anim_m = transform.Find("middleImage").GetComponent<Animator>();
        anim_left = transform.Find("leftImage").GetComponent<Animator>();
        anim_right = transform.Find("rightImage").GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //接收按键事件
        if (!anim_left.IsInTransition(0))
        {
            if (gameObject.name == "farBG")
            {
                if (Input.GetKeyDown(KeyCode.A)) OnEnterLeft();
                else if (Input.GetKeyDown(KeyCode.D)) OnEnterRight();
            }
            else//nearBG复用代码
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) OnEnterLeft();
                else if (Input.GetKeyDown(KeyCode.RightArrow)) OnEnterRight();
            }
        }
    }
    public void OnEnterUp()
    {

    }
    public void OnEnterDown()
    {

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

        //TODO chang showImage
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

        //TODO chang showImage
    }
}
