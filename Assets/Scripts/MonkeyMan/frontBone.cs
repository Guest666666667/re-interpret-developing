using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontBone : MonoBehaviour
{
    public string objectName;//残影对应部位骨骼对象的路径
    private float transLast = 10000f;//按键失误时用来计算距离状态转换的剩余时间
    private float perfectLast = 10000f;//距离完美判定点剩余时间
    private bool hasAcc = true;//是否已加速
    private float timeLast = 10000f;//加速到完美位置需要的剩余时间
    private float accTime = 0f;//距离正常进度的时间差

    void Start()
    {
        hasAcc = true;
    }

    void Update()
    {
        perfectLast -= Time.deltaTime;
        if (Input.GetButtonDown("q"))//假设左臂对应q键控制，按下q键时根据时间差得出判定结果
        {
            if (perfectLast > (-0.1f) && perfectLast < 0.1f)//0.1为最大完美判定允许误差，误差小于0.1则判定为完美
            {
                perfectLast = 10000f;
                Debug.Log("perfect!");
            }
            else if (perfectLast >= 0.1f && perfectLast < 0.5f)//完美范围外则判定为miss，动画停顿并计时
            {
                transLast = perfectLast;
                accTime = perfectLast;
                perfectLast = 10000f;
                GetComponent<Animator>().speed = 0f;
                Debug.Log("miss");
            }
            else if (perfectLast <= (-0.1f))
            {
                perfectLast = 10000f;
                Debug.Log("miss");
            }
        }
        transLast -= Time.deltaTime;
        if (transLast < 0f)//停顿时间结束之后开始加速到正常进度
        {
            hasAcc = false;
            //GetComponent<Animator>().Play(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, -1, (0f - transLast) + 0.65f);
            GetComponent<Animator>().speed = 1f;
            transLast = 10000f;
        }

        
    }

    void FixedUpdate()
    {
        timeLast -= Time.fixedDeltaTime;
        if (!hasAcc)
        {
            hasAcc = true;
            GetComponent<Animator>().speed = (accTime / Time.fixedDeltaTime / 2f) + 1f;
            timeLast = 2f * Time.fixedDeltaTime - 0.001f;
        }
        if (timeLast <= 0f)
        {
            timeLast = 10000f;
            GetComponent<Animator>().speed = 1f;
        }
    }

    public void callChange()//判定起始点触发的事件，用于改变残影姿势
    {
        GameObject.Find(objectName).GetComponent<afterBone>().Change();
    }

    public void begin()//判定起始点触发的事件，用于显示判定蓝圈并开始缩小
    {
        GameObject.Find("runTimeUI/hint").GetComponent<hint>().begin();
        perfectLast = 0.5f;
        callChange();
    }
}
