using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectBgUnit : MonoBehaviour
{
    //控制UI动画
    private Animator anim_m;
    private Animator anim_left;
    private Animator anim_right;
    private bool ifFirstIn = true;
    private Image mid;
    private Image left;
    private Image right;
    private int LeftOrRight = 0;//用于标明上一个上下操作的是上还是下，无0，上1，下2
    private bool changeAlready = false;

    private int nearBgindex = 1;//默认中间是peach
    //private string[] nearBg = { "LocustTree", "Peach", "Stone", "Thorns" };
    private string[] nearBg = { "LocustTree", "Peach", "ST" };
    private GameObject BG;
    private GameObject nearBGtemp;
    //private GameObject child;

    private void Awake()
    {
        anim_m = transform.Find("middleImage").GetComponent<Animator>();
        anim_left = transform.Find("leftImage").GetComponent<Animator>();
        anim_right = transform.Find("rightImage").GetComponent<Animator>();

        mid= transform.Find("middleImage").GetComponent<Image>();
        left = transform.Find("leftImage").GetComponent<Image>();
        right = transform.Find("rightImage").GetComponent<Image>();

        BG = GameObject.Find("/BG");
        BG.transform.GetChild(0).gameObject.SetActive(true);//背景
        BG.transform.GetChild(1).gameObject.SetActive(true);//地面
        BG.transform.GetChild(2).gameObject.SetActive(true);//地面
        BG.transform.GetChild(3).gameObject.SetActive(true);//远景
        BG.transform.Find("Peach").gameObject.SetActive(true);//第一个桃林
        nearBGtemp = BG.transform.Find("Peach").gameObject;
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
                if (Input.GetKeyDown(KeyCode.A)) OnEnterLeft(true);
                else if (Input.GetKeyDown(KeyCode.D)) OnEnterRight(true);
            }
            else//nearBG复用代码
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) OnEnterLeft(false);
                else if (Input.GetKeyDown(KeyCode.RightArrow)) OnEnterRight(false);
            }
        }
    }
    public void OnEnterUp()
    {

    }
    public void OnEnterDown()
    {

    }
    public void OnEnterLeft(bool ifFar)//远景为true
    {
        //if (nearBGtemp != null)
        //    nearBGtemp.SetActive(false);

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

        //TODO change showImage
        if (ifFar)
        {

        }
        else
        {
            if (nearBGtemp != null) nearBGtemp.SetActive(false);
            ////TODO select下放四张图，以松树/桃林/石头/荆棘的顺序
            //int changeIndex = 0;//应该换成的图的下标
            //if (nearBgindex + 2 < 4) changeIndex = nearBgindex + 2;
            //else
            //{
            //    changeIndex = nearBgindex + 2 - 4;
            //}
            ////if (transform.Find("select").GetChild(changeIndex).GetComponent<Sprite>() != null) { Debug.Log("no wrong!!!"); }
            //if (anim_m.GetInteger("state") == 2 || anim_m.GetInteger("state") == 8)//此状态为右面位置
            //{ mid.sprite = transform.Find("select").GetChild(changeIndex).GetComponent<SpriteRenderer>().sprite; }
            //else if (anim_left.GetInteger("state") == 2 || anim_left.GetInteger("state") == 8)
            //{ left.sprite = transform.Find("select").GetChild(changeIndex).GetComponent<SpriteRenderer>().sprite; }
            //else if (anim_right.GetInteger("state") == 2 || anim_right.GetInteger("state") == 8)
            //{ right.sprite = transform.Find("select").GetChild(changeIndex).GetComponent<SpriteRenderer>().sprite; }
            ////更新index
            //nearBgindex = changeIndex - 1 >= 0 ? changeIndex - 1 : changeIndex - 1 + 4;

            ////更改背景
            //nearBGtemp = BG.transform.Find(nearBg[nearBgindex]).gameObject;
            //BG.transform.Find(nearBg[nearBgindex]).gameObject.SetActive(true);

            if (nearBgindex + 1 < 3)
            {
                nearBgindex += 1;
            }
            else
            {
                nearBgindex = 0;
            }
            nearBGtemp = BG.transform.Find(nearBg[nearBgindex]).gameObject;
            BG.transform.Find(nearBg[nearBgindex]).gameObject.SetActive(true);
        }
    }
    public void OnEnterRight(bool ifFar)
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

        //TODO change showImage
        if (ifFar)
        {

        }
        else
        {
            if (nearBGtemp != null) nearBGtemp.SetActive(false);
            //int changeIndex = 0;//应该换成的图的下标
            //if (nearBgindex - 2 >= 0) changeIndex = nearBgindex - 2;
            //else
            //{
            //    changeIndex = nearBgindex - 2 + 4;
            //}
            //if (anim_m.GetInteger("state") == 1 || anim_m.GetInteger("state") == 7)//此状态为zuo面位置
            //{ mid.sprite = transform.Find("select").GetChild(changeIndex).GetComponent<SpriteRenderer>().sprite; }
            //else if (anim_left.GetInteger("state") == 1 || anim_left.GetInteger("state") == 7)
            //{ left.sprite = transform.Find("select").GetChild(changeIndex).GetComponent<SpriteRenderer>().sprite; }
            //else if (anim_right.GetInteger("state") == 1 || anim_right.GetInteger("state") == 7)
            //{ right.sprite = transform.Find("select").GetChild(changeIndex).GetComponent<SpriteRenderer>().sprite; }
            ////更新index
            //nearBgindex = changeIndex + 1 < 4 ? changeIndex + 1 : changeIndex + 1 - 4;

            ////更改背景
            //nearBGtemp = BG.transform.Find(nearBg[nearBgindex]).gameObject;
            //BG.transform.Find(nearBg[nearBgindex]).gameObject.SetActive(true);

            if (nearBgindex - 1 >= 0) 
            {
                nearBgindex -= 1;
            }
            else
            {
                nearBgindex = 2;
            }
            nearBGtemp = BG.transform.Find(nearBg[nearBgindex]).gameObject;
            BG.transform.Find(nearBg[nearBgindex]).gameObject.SetActive(true);
        }
    }
}
