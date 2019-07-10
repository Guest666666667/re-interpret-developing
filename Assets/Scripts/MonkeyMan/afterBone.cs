﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

//本代码为残影控制脚本

public class afterBone : MonoBehaviour
{
    /*private float timeLast = 10000f;//加速到完美位置需要的剩余时间
    private bool needChange = false;//是否需要改变姿势
    private float needCoverLast = 10000f;//距离需要复原剩余时间
    private float coverTimeLast = 10000f;//复原到正常位置需要的剩余时间*/
    public GameObject model;//对应残影部位模型对象
    public GameObject parent;
    private bool hasAcc = true;//是否已加速
    private float timeLast = 10000f;//加速到完美位置需要的剩余时间
    private bool showing = false;//是否正在展示残影
    private float showingLast = 10000f;//展示剩余时间
    public float existTime = 0.5f;
    private bool needSlow = false;
    private int countRun = 0;//跑步动作循环计数
    private int countThirsty = 0;//干渴动作循环计数
    private int countTired = 0;//累动作循环计数
    private int countExhaust = 0;//极累动作循环计数

    void Start()
    {
        countRun = 0;
        existTime = 0.5f;
        hasAcc = true;
        needSlow = false;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        /*timeLast -= Time.fixedDeltaTime;
        needCoverLast -= Time.fixedDeltaTime;
        coverTimeLast -= Time.fixedDeltaTime;
        if (needChange)
        {
            needChange = false;
            GetComponent<Animator>().speed = (0.5f / Time.fixedDeltaTime) + 1f;
            timeLast = Time.fixedDeltaTime - 0.001f;
        }
        if (timeLast <= 0f)
        {
            timeLast = 10000f;
            GetComponent<Animator>().speed = 1f;
            needCoverLast = 0.499f;
        }
        if (needCoverLast <= 0f)
        {
            needCoverLast = 10000f;
            GetComponent<Animator>().speed = ;
        }*/
        timeLast -= Time.fixedDeltaTime;
        showingLast -= Time.fixedDeltaTime;
        if (!hasAcc)
        {
            hasAcc = true;
            GetComponent<Animator>().speed = (existTime / Time.fixedDeltaTime / 3f) + 1f;
            timeLast = 3f * Time.fixedDeltaTime - 0.001f;//0.001用于补正细微误差
            //AnimatorStateInfo tmp = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            //GetComponent<Animator>().Play(tmp.fullPathHash, -1, (tmp.normalizedTime + existTime / tmp.length) % 1);
            
        }
        if (timeLast <= 0f)
        {
            timeLast = 10000f;
            //GetComponent<Animator>().speed = 1f;
            showing = true;
            showingLast = existTime;
            GetComponent<Animator>().speed = 0f;
            if (needSlow)
            {
                GameObject.Find("afterImage").GetComponent<timeScaleManagement>().addSlow();
            }
            //GameObject.Find("afterImage").GetComponent<timeScaleManagement>().addSlow();
        }
        /*if (showing)
        {
            
        }*/
        if (showingLast <= 0f)
        {
            if (needSlow)
            {
                GameObject.Find("afterImage").GetComponent<timeScaleManagement>().delSlow();
                needSlow = false;
            }
            //GameObject.Find("afterImage").GetComponent<timeScaleManagement>().delSlow();
            GetComponent<Animator>().speed = 1f;
            showing = false;
            showingLast = 10000f;
            Color temp = model.GetComponent<SpriteMeshInstance>().color;
            model.GetComponent<SpriteMeshInstance>().color = new Color(temp.r, temp.g, temp.b, 0f);
            foreach (SpriteMeshInstance tmp in model.GetComponentsInChildren<SpriteMeshInstance>())
            {
                tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);
            }
        }
    }

    public void Change(bool needSlow)//前景人改变姿势事件对应调用的残影脚本方法
    {
        //GetComponent<Animator>().Play(stateName, -1, 0.75f);
        /*showing = true;
        showingLast = 0.5f;
        GetComponent<Animator>().speed = 0f;*/
        hasAcc = false;
        Color temp = model.GetComponent<SpriteMeshInstance>().color;
        model.GetComponent<SpriteMeshInstance>().color = new Color(temp.r, temp.g, temp.b, 1f);
        foreach (SpriteMeshInstance tmp in model.GetComponentsInChildren<SpriteMeshInstance>())
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f);
        }
        this.needSlow = needSlow;
        if (needSlow)
        {
            existTime = 0.5f;
        }
        else
        {
            existTime = 1f;
        }
    }

    public void callChange(string stateName)//空方法，防止残影动画触发相同的事件
    {

    }

    public void begin()//同上
    {

    }

    public void enableSwing(bool isOn)
    {
        parent.GetComponent<parent>().enableSwing(isOn);
    }

    public void beginThirsty()
    {
        countRun++;
        if (countRun >= 5)
        {
            countRun = 0;
            GetComponent<Animator>().SetBool("thirsty", !GetComponent<Animator>().GetBool("thirsty"));
        }
    }

    public void beginDrink(int type)//0代表即将干渴 1代表即将极累 2代表即将累死
    {
        if (type == 0)
        {
            countThirsty++;
            GetComponent<Animator>().SetFloat("thirstySpeed", (5f / 6f) * GetComponent<Animator>().GetFloat("thirstySpeed"));
            if (countThirsty >= 3)
            {
                countThirsty = 0;
                GetComponent<Animator>().SetBool("drink", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("drink", false);
            }
        }
        else if (type == 1)
        {
            countTired++;
            GetComponent<Animator>().SetFloat("tiredSpeed", 0.9f * GetComponent<Animator>().GetFloat("tiredSpeed"));
            if (countTired >= 3)
            {
                countTired = 0;
                GetComponent<Animator>().SetBool("exhaust", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("exhaust", false);
            }
        }
        else if (type == 2)
        {
            countExhaust++;
            GetComponent<Animator>().SetFloat("exhaustSpeed", (5f / 6f) * GetComponent<Animator>().GetFloat("exhaustSpeed"));
            if (countExhaust >= 3)
            {
                countExhaust = 0;
                GetComponent<Animator>().SetBool("die", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("die", false);
            }
        }
    }
}
