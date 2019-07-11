using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class selectBgUnit : MonoBehaviour
{
    //属性控制
    private Attribute attributeManager;
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

    private GameObject BG;
    //近景
    private int nearBgIndex = 1;//默认中间是peach
    private string[] nearBg = { "LocustTree", "Peach", "ST" };
    private GameObject nearBGtemp;
    //远景
    private int farBgIndex = 1;
    private string[] farBg = { "coldMount", "farMount", "dangerousMount" };
    private GameObject farBgtemp;

    private GameObject show;
    private RawImage titleImage;
    private RawImage bgMessageImage;//文字后面的高亮衬底
    private Text bgMessage;
    private Text bgTitle;
    private string[] farTitle = { "雪岭", "远山", "险峰" };
    private string[] farBgmessage = { "冰冻：移动速度和跳跃高度均下降30%", "凝神：能量值恢复速度加快1倍", "紧张:移动速度提高30%，防御力下降1点" };
    private string[] nearTitle = { "高树", "桃林", "荆棘" };
    private string[] nearBgmessage = { "遮掩：不能使用飞刀", "丰茂：每次受伤有20%几率恢复5点生命值", "针刺：每秒有40%几率失去2点生命值" };

    private void Awake()
    {
        anim_m = transform.Find("middleImage").GetComponent<Animator>();
        anim_left = transform.Find("leftImage").GetComponent<Animator>();
        anim_right = transform.Find("rightImage").GetComponent<Animator>();

        mid= transform.Find("middleImage").GetComponent<Image>();
        left = transform.Find("leftImage").GetComponent<Image>();
        right = transform.Find("rightImage").GetComponent<Image>();
        show = transform.Find("show").GetComponent<GameObject>();
        titleImage = transform.Find("show/titleImage").GetComponent<RawImage>();
        bgMessageImage = transform.Find("show/bgMessageImage").GetComponent<RawImage>();
        bgMessage = transform.Find("show/bgMessage").GetComponent<Text>();
        bgTitle = transform.Find("show/bgTitle").GetComponent<Text>();

        BG = GameObject.Find("/BG");
        //BG.transform.GetChild(0).gameObject.SetActive(true);//背景
        //BG.transform.GetChild(1).gameObject.SetActive(true);//地面
        //BG.transform.GetChild(2).gameObject.SetActive(true);//地面

        BG.transform.Find("farMount").gameObject.SetActive(true);//第一个远山
        BG.transform.Find("Peach").gameObject.SetActive(true);//第一个桃林
        nearBGtemp = BG.transform.Find("Peach").gameObject;
        farBgtemp = BG.transform.Find("farMount").gameObject;

        attributeManager = GameObject.Find("/attributeManager").GetComponent<Attribute>();
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
        AudioManager.Instance.PlaySound("Music/Sound/UI/tick");
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
            if (farBgtemp != null) farBgtemp.SetActive(false);
            if (farBgIndex + 1 < 3)
            {
                farBgIndex += 1;
            }
            else
            {
                farBgIndex = 0;
            }
            farBgtemp = BG.transform.Find(farBg[farBgIndex]).gameObject;

            Vector3 temp = farBgtemp.transform.localPosition;
            temp.x = 10;
            farBgtemp.transform.localPosition = temp;
            farBgtemp.SetActive(true);
            farBgtemp.transform.DOLocalMoveX(0, 0.6f);

            //更改属性表
            attributeManager.setSceneArray(0, farBgIndex);
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

            if (nearBgIndex + 1 < 3)
            {
                nearBgIndex += 1;
            }
            else
            {
                nearBgIndex = 0;
            }
            nearBGtemp = BG.transform.Find(nearBg[nearBgIndex]).gameObject;

            Vector3 temp = nearBGtemp.transform.localPosition;
            temp.x = 10;
            nearBGtemp.transform.localPosition = temp;
            nearBGtemp.SetActive(true);
            nearBGtemp.transform.DOLocalMoveX(0, 0.6f);

            //保存属性
            attributeManager.setSceneArray(2, nearBgIndex);
        }
        //延迟更改文字
        titleImage.gameObject.SetActive(false);
        bgMessageImage.gameObject.SetActive(false);
        bgTitle.text = "";
        bgMessage.text = "";
        Invoke("OnChangeMessage", 0.45f);
    }
    public void OnEnterRight(bool ifFar)
    {
        AudioManager.Instance.PlaySound("Music/Sound/UI/tick");
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
            if (farBgtemp != null) farBgtemp.SetActive(false);
            if (farBgIndex - 1 >= 0)
            {
                farBgIndex -= 1;
            }
            else
            {
                farBgIndex = 2;
            }
            farBgtemp = BG.transform.Find(farBg[farBgIndex]).gameObject;
            Vector3 temp = farBgtemp.transform.localPosition;
            temp.x = -10;
            farBgtemp.transform.localPosition = temp;
            farBgtemp.SetActive(true);
            farBgtemp.transform.DOLocalMoveX(0, 0.6f);

            //保存属性
            attributeManager.setSceneArray(0, farBgIndex);
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

            if (nearBgIndex - 1 >= 0) 
            {
                nearBgIndex -= 1;
            }
            else
            {
                nearBgIndex = 2;
            }
            nearBGtemp = BG.transform.Find(nearBg[nearBgIndex]).gameObject;
            Vector3 temp = nearBGtemp.transform.localPosition;
            temp.x = -10;
            nearBGtemp.transform.localPosition = temp;
            nearBGtemp.SetActive(true);
            nearBGtemp.transform.DOLocalMoveX(0, 0.6f);

            //保存属性
            attributeManager.setSceneArray(2, nearBgIndex);
        }
        //延迟更改文字
        titleImage.gameObject.SetActive(false);
        bgMessageImage.gameObject.SetActive(false);
        bgTitle.text = "";
        bgMessage.text = "";
        Invoke("OnChangeMessage", 0.45f);
    }
    public void OnChangeMessage()
    {
        titleImage.gameObject.SetActive(true);
        bgMessageImage.gameObject.SetActive(true);
        if (gameObject.name == "farBG")
        {
            bgTitle.text = farTitle[farBgIndex];
            bgMessage.text = farBgmessage[farBgIndex];
        }
        else
        {
            bgTitle.text = nearTitle[nearBgIndex];
            bgMessage.text = nearBgmessage[nearBgIndex];
        }
    }
}
