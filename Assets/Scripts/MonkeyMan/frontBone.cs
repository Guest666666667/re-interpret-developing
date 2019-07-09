using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontBone : MonoBehaviour
{
    public string objectName;//残影对应部位骨骼对象的路径
    public float existTime = 1.2f;//光圈缩小时间
    public GameObject parent;
    private float transLast = 10000f;//按键失误时用来计算距离状态转换的剩余时间
    private float perfectLast = 10000f;//距离完美判定点剩余时间
    private bool hasAcc = true;//是否已加速
    private float timeLast = 10000f;//加速到完美位置需要的剩余时间
    private float accTime = 0f;//距离正常进度的时间差
    private int No = -1;//正在进行判定的骨骼编号
    private int perfectSum = 0;//完美按键总次数
    private string[] Keys = null;//所有需要按下的键值
    private bool hasDel = false;//是否已删除一层慢镜头
    private List<GameObject> buttons;
    private int countRun = 0;//跑步动作循环计数

    void Start()
    {
        countRun = 0;
        hasDel = false;
        buttons = new List<GameObject>();
        hasAcc = true;
        Keys = null;
    }

    void Update()
    {
        perfectLast -= Time.deltaTime;
        transLast -= Time.deltaTime;
        bool judge = true;//判定多个键是否同时按下
        if (Keys == null) 
        {
            judge = false;
        }
        else
        {
            foreach (string t in Keys)
            {
                if (!Input.GetButton(t))
                {
                    judge = false;
                    break;
                }
            }
        }
        if (judge) //假设左臂对应q键控制，按下q键时根据时间差得出判定结果
        {
            if (perfectLast > (-0.1f) && perfectLast < 0.1f)//0.1为最大完美判定允许误差，误差小于0.1则判定为完美
            {
                perfectLast = 10000f;
                buttons.ForEach(t =>
                {
                    t.GetComponent<hint>().complete(true, true);
                });
                buttons.Clear();
                Debug.Log("perfect!");
            }
            else if (perfectLast >= 0.1f && perfectLast < existTime)//完美范围外则判定为miss，动画停顿并计时
            {
                transLast = perfectLast;
                accTime = perfectLast;
                perfectLast = 10000f;
                buttons.ForEach(t =>
                {
                    t.GetComponent<hint>().complete(true, false);
                });
                buttons.Clear();
                GetComponent<Animator>().speed = 0f;
                Debug.Log("miss");
            }
            
        }
        //if (!hasDel && perfectLast <= 0f)
        //{
        //    GameObject.Find("afterImage").GetComponent<timeScaleManagement>().delSlow();
        //    hasDel = true;
        //}
        if (perfectLast <= (-0.1f))//超时自动结束判定
        {
            perfectLast = 10000f;
            buttons.ForEach(t =>
            {
                t.GetComponent<hint>().complete(false, false);
            });
            buttons.Clear();
            Debug.Log("miss");
        }
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
            GetComponent<Animator>().speed = (accTime / Time.fixedDeltaTime / 3f) + 1f;
            timeLast = 3f * Time.fixedDeltaTime - 0.001f;//0.001用于补正细微误差
        }
        if (timeLast <= 0f)
        {
            timeLast = 10000f;
            GetComponent<Animator>().speed = 1f;
        }
    }

    public void callChange(bool needSlow)//判定起始点触发的事件，用于改变残影姿势
    {
        GameObject.Find(objectName).GetComponent<afterBone>().Change(needSlow);
    }

    public void begin(int No, int seq)//判定起始点触发的事件，用于显示判定蓝圈并开始缩小
    {
        //hasDel = false;
        //GameObject.Find("afterImage").GetComponent<timeScaleManagement>().addSlow();
        //bool needSlow = false;
        this.No = No;
        GameObject tmp = Resources.Load("UIPanel/hint") as GameObject;
        GameObject gen = Instantiate(tmp, GameObject.Find("Canvas/runTimeUI").transform, false);
        buttons.Add(gen);
        if (No == 0 || No == 7)
        {
            GameObject tmp2 = Resources.Load("UIPanel/hint") as GameObject;
            GameObject gen2 = Instantiate(tmp2, GameObject.Find("Canvas/runTimeUI").transform, false);
            buttons.Add(gen2);
            gen.GetComponent<hint>().setUp("q",1,seq);
            gen2.GetComponent<hint>().setUp("e",1,seq);
            Keys = new string[2];
            Keys[0] = "q";
            Keys[1] = "e";
            //needSlow = true;
        }
        else if (No >= 1 && No <= 3)
        {
            gen.GetComponent<hint>().setUp("e",0,seq);
            Keys = new string[1];
            Keys[0] = "e";
        }
        else if (No >= 4 && No <= 6)
        {
            gen.GetComponent<hint>().setUp("q",0, seq);
            Keys = new string[1];
            Keys[0] = "q";
        }
        else if (No == 8)
        {
            GameObject tmp2 = Resources.Load("UIPanel/hint") as GameObject;
            GameObject gen2 = Instantiate(tmp2, GameObject.Find("Canvas/runTimeUI").transform, false);
            buttons.Add(gen2);
            gen.GetComponent<hint>().setUp("a",1, seq);
            gen2.GetComponent<hint>().setUp("d",1, seq);
            Keys = new string[2];
            Keys[0] = "a";
            Keys[1] = "d";
            //needSlow = true;
        }
        else if (No == 9)
        {
            gen.GetComponent<hint>().setUp("a",0, seq);
            Keys = new string[1];
            Keys[0] = "a";
        }
        else if (No == 10)
        {
            gen.GetComponent<hint>().setUp("d",0, seq);
            Keys = new string[1];
            Keys[0] = "d";
        }
        perfectLast = existTime;
        //callChange(needSlow);
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
            GetComponent<Animator>().SetBool("thirsty", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("thirsty", false);
        }
    }
}
