using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontBone : MonoBehaviour
{
    public string objectName;//残影对应部位骨骼对象的路径
    private float timeLast = 10000f;//按键失误时用来计算距离状态转换的剩余时间

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown("q"))//假设左臂对应q键控制，按下q键时根据时间差得出判定结果
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.65f && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 0.85f)//假设标准化时间点0.75为完美时间点，0.1为最大完美判定允许误差，误差小于0.1则判定为完美
            {
                Debug.Log("perfect!");
            }
            else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 0.65f)//完美范围外则判定为miss，动画停顿并计时
            {
                timeLast = (0.65f - GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime) * GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
                GetComponent<Animator>().speed = 0f;
                Debug.Log("miss");
            }
        }
        timeLast -= Time.deltaTime;
        if (timeLast < 0f)//在标准化时间点0.65处（即剩余时间小于0时）将动画时间同步跳转到标准化时间点0.65+剩余时间与0之间的误差
        {
            GetComponent<Animator>().Play(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, -1, (0f - timeLast) + 0.65f);
            GetComponent<Animator>().speed = 1f;
            timeLast = 10000f;
        }
    }

    public void callChange()//判定起始点触发的事件，用于改变残影姿势
    {
        GameObject.Find(objectName).GetComponent<afterBone>().Change();
    }

    public void begin()//判定起始点触发的事件，用于显示判定蓝圈并开始缩小
    {
        GameObject.Find("runTimeUI/hint").GetComponent<hint>().begin();
        callChange();
    }
}
