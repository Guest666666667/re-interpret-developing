using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSpeed : MonoBehaviour
{
    //用来控制深林的创建
    public bool sparse_1 = false;
    public bool thick = false;
    public bool sparse_2 = false;

    //用来控制高树的创建
    public bool LocustSparse_1 = false;
    public bool LocustThick = false;
    public bool LocustSparse_2 = false;

    private double totalTime = 0.0;//音乐的时间120s
    public bool isStop = false;//为了解决黄河停住的问题
    private bool review = false;
    /// <summary>
    /// 获得背景板的脚本对象
    /// </summary>
    private LoopBack LB_11;
    private LoopBack LB_12;
    private LoopBack LB_13;
    private LoopBack LB_21;
    private LoopBack LB_22;
    private LoopBack LB_23;
    private LoopBack LB_31;
    private LoopBack LB_32;
    private LoopBack LB_33;
    private LoopBack LB_41;
    private LoopBack LB_42;
    private LoopBack LB_43;
    private LoopBack LB_51;
    private LoopBack LB_52;
    private LoopBack LB_53;
    private LoopBack LB_61;
    private LoopBack LB_62;
    private LoopBack LB_63;
    /// <summary>
    /// 获得深林的对象
    /// </summary>
    private LoopBackForTree LBFT_11;
    private LoopBackForTree LBFT_12;
    private LoopBackForTree LBFT_13;
    private LoopBackForTree LBFT_21;
    private LoopBackForTree LBFT_22;
    private LoopBackForTree LBFT_23;
    /// <summary>
    /// 获得地面的对象
    /// </summary>
    private LoopForGround LFG_11;
    private LoopForGround LFG_12;
    private LoopForGround LFG_21;
    private LoopForGround LFG_22;
    //小麦的对象
    private wheat wh;
    //河流
    private moveRiver mR;
    private RiverMoveDown RMD;
    //石头
    private stone sT;
    private stone sT_2;
    //荆棘
    private thorns th_1;
    private thorns th_2;
    //桃林
    private moveUp mU;
    // Start is called before the first frame update
    void Start()
    {
        //初始化对象
        //山和云的对象
        LB_11 = GameObject.FindWithTag("BackGround_11").GetComponent<LoopBack>();
        LB_12 = GameObject.FindWithTag("BackGround_12").GetComponent<LoopBack>();
        LB_13 = GameObject.FindWithTag("BackGround_13").GetComponent<LoopBack>();
        LB_21 = GameObject.FindWithTag("BackGround_21").GetComponent<LoopBack>();
        LB_22 = GameObject.FindWithTag("BackGround_22").GetComponent<LoopBack>();
        LB_23 = GameObject.FindWithTag("BackGround_23").GetComponent<LoopBack>();
        LB_31 = GameObject.FindWithTag("BackGround_31").GetComponent<LoopBack>();
        LB_32 = GameObject.FindWithTag("BackGround_32").GetComponent<LoopBack>();
        LB_33 = GameObject.FindWithTag("BackGround_33").GetComponent<LoopBack>();
        LB_41 = GameObject.FindWithTag("BackGround_41").GetComponent<LoopBack>();
        LB_42 = GameObject.FindWithTag("BackGround_42").GetComponent<LoopBack>();
        LB_43 = GameObject.FindWithTag("BackGround_43").GetComponent<LoopBack>();
        LB_51 = GameObject.FindWithTag("BackGround_51").GetComponent<LoopBack>();
        LB_52 = GameObject.FindWithTag("BackGround_52").GetComponent<LoopBack>();
        LB_53 = GameObject.FindWithTag("BackGround_53").GetComponent<LoopBack>();
        LB_61 = GameObject.FindWithTag("BackGround_61").GetComponent<LoopBack>();
        LB_62 = GameObject.FindWithTag("BackGround_62").GetComponent<LoopBack>();
        LB_63 = GameObject.FindWithTag("BackGround_63").GetComponent<LoopBack>();
        //树林的对象
        LBFT_11 = GameObject.FindWithTag("small_1").GetComponent<LoopBackForTree>();
        LBFT_12 = GameObject.FindWithTag("middle_1").GetComponent<LoopBackForTree>();
        LBFT_13 = GameObject.FindWithTag("big_1").GetComponent<LoopBackForTree>();
        LBFT_21 = GameObject.FindWithTag("small_2").GetComponent<LoopBackForTree>();
        LBFT_22 = GameObject.FindWithTag("middle_2").GetComponent<LoopBackForTree>();
        LBFT_23 = GameObject.FindWithTag("big_2").GetComponent<LoopBackForTree>();
        //地面的对象
        LFG_11 = GameObject.FindWithTag("ground_1").GetComponent<LoopForGround>();
        LFG_12 = GameObject.FindWithTag("ground_11").GetComponent<LoopForGround>();
        LFG_21 = GameObject.FindWithTag("ground_2").GetComponent<LoopForGround>();
        LFG_22 = GameObject.FindWithTag("ground_22").GetComponent<LoopForGround>();
        //小麦的对象
        wh = GameObject.FindWithTag("wheat").GetComponent<wheat>();
        //黄河的对象
        mR= GameObject.FindWithTag("river").GetComponent<moveRiver>();
        //河里的水的对象
        RMD = GameObject.FindWithTag("water").GetComponent<RiverMoveDown>();
        //
        sT= GameObject.FindWithTag("stone").GetComponent<stone>();
        sT_2 = GameObject.FindWithTag("stone_2").GetComponent<stone>();
        //
        th_1= GameObject.FindWithTag("thorns_1").GetComponent<thorns>();
        th_2 = GameObject.FindWithTag("thorns_2").GetComponent<thorns>();
        //
        mU = GameObject.FindWithTag("peach").GetComponent<moveUp>();
    }

    // Update is called once per frame
    void Update()
    {
        //totalTime = totalTime + Time.deltaTime;
        totalTime = GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicPlayer.time;
        //0-5s		只有云在慢慢地移动
        if (totalTime<=5.0)
        {
            //山
            LB_11.speed =0.0f;
            LB_12.speed =0.0f;
            LB_13.speed =0.0f;
            LB_21.speed =0.0f;
            LB_22.speed =0.0f;
            LB_23.speed =0.0f;
            LB_31.speed =0.0f;
            LB_32.speed =0.0f;
            LB_33.speed =0.0f;
            //云
            LB_41.speed =0.2f;
            LB_42.speed =0.2f;
            LB_43.speed =0.2f;
            LB_51.speed =0.15f;
            LB_52.speed =0.15f;
            LB_53.speed =0.15f;
            LB_61.speed =0.1f;
            LB_62.speed =0.1f;
            LB_63.speed =0.1f;
            //地面
            LFG_11.speed=0.0f;
            LFG_12.speed=0.0f;
            LFG_21.speed=0.0f;
            LFG_22.speed = 0.0f;
            //小麦
            wh.speed = 0.0f;
        }
        //5-10s	所有的景物开始缓慢递增的移动,速度增加到最大
        if ((totalTime <= 10.0)&&(totalTime>5.0))
        {
            //山加到0.4    0.5     0.6
            LB_11.speed += 0.6f / (5f/Time.deltaTime);
            LB_12.speed += 0.6f / (5f / Time.deltaTime);
            LB_13.speed += 0.6f / (5f / Time.deltaTime);
            LB_21.speed += 0.5f / (5f / Time.deltaTime);
            LB_22.speed += 0.5f / (5f / Time.deltaTime);
            LB_23.speed += 0.5f / (5f / Time.deltaTime);
            LB_31.speed += 0.4f / (5f / Time.deltaTime);
            LB_32.speed += 0.4f / (5f / Time.deltaTime);
            LB_33.speed += 0.4f / (5f / Time.deltaTime);
            //云加到0.45    0.55       0.65
            LB_41.speed += 0.45f / (5f / Time.deltaTime);
            LB_42.speed += 0.45f / (5f / Time.deltaTime);
            LB_43.speed += 0.45f / (5f / Time.deltaTime);
            LB_51.speed += 0.4f / (5f / Time.deltaTime);
            LB_52.speed += 0.4f / (5f / Time.deltaTime);
            LB_53.speed += 0.4f / (5f / Time.deltaTime);
            LB_61.speed += 0.35f / (5f / Time.deltaTime);
            LB_62.speed += 0.35f / (5f / Time.deltaTime);
            LB_63.speed += 0.35f / (5f / Time.deltaTime);
            //地面加到10    5
            LFG_11.speed += 10f / (5f / Time.deltaTime);
            LFG_12.speed += 10f / (5f / Time.deltaTime);
            LFG_21.speed += 5f / (5f / Time.deltaTime);
            LFG_22.speed += 5f / (5f / Time.deltaTime);
            //小麦加到10
            wh.speed += 10f / (5f / Time.deltaTime);
        }
        //13-40S	匀速奔跑   这段时间是深林时间
        //稀疏
        if ((totalTime <= 20) && (totalTime > 13))
        {
            sparse_1 = true;
        }
        //		茂密
        if ((totalTime <= 30) && (totalTime > 20))
        {
            sparse_1 = false;
            thick = true;
        }

        //动作变慢   速度变为原来的0.5
        //if ((totalTime <= 29) && (totalTime > 27))
        //{
        //    if(!review)
        //    {
        //        //山
        //        LB_11.speed = LB_11.speed * 0.5f;
        //        LB_12.speed = LB_12.speed * 0.5f;
        //        LB_13.speed = LB_13.speed * 0.5f;
        //        LB_21.speed = LB_21.speed * 0.5f;
        //        LB_22.speed = LB_22.speed * 0.5f;
        //        LB_23.speed = LB_23.speed * 0.5f;
        //        LB_31.speed = LB_31.speed * 0.5f;
        //        LB_32.speed = LB_32.speed * 0.5f;
        //        LB_33.speed = LB_33.speed * 0.5f;
        //        //云
        //        LB_41.speed = LB_41.speed * 0.5f;
        //        LB_42.speed = LB_42.speed * 0.5f;
        //        LB_43.speed = LB_43.speed * 0.5f;
        //        LB_51.speed = LB_51.speed * 0.5f;
        //        LB_52.speed = LB_52.speed * 0.5f;
        //        LB_53.speed = LB_53.speed * 0.5f;
        //        LB_61.speed = LB_61.speed * 0.5f;
        //        LB_62.speed = LB_62.speed * 0.5f;
        //        LB_63.speed = LB_63.speed * 0.5f;
        //        //地
        //        LFG_11.speed = LFG_11.speed * 0.5f;
        //        LFG_12.speed = LFG_12.speed * 0.5f;
        //        LFG_21.speed = LFG_21.speed * 0.5f;
        //        LFG_22.speed = LFG_22.speed * 0.5f;
        //        //深林
        //        LBFT_11.speed = LBFT_11.speed * 0.5f;
        //        LBFT_12.speed = LBFT_12.speed * 0.5f;
        //        LBFT_13.speed = LBFT_13.speed * 0.5f;
        //        LBFT_21.speed = LBFT_21.speed * 0.5f;
        //        LBFT_22.speed = LBFT_22.speed * 0.5f;
        //        LBFT_23.speed = LBFT_23.speed * 0.5f;
        //        review = true;
        //    }
        //}
        ////让速度恢复
        //if ((totalTime <= 30) && (totalTime > 29))
        //{
        //    if(review)
        //    {
        //        //山
        //        LB_11.speed = LB_11.speed * 2f;
        //        LB_12.speed = LB_12.speed * 2f;
        //        LB_13.speed = LB_13.speed * 2f;
        //        LB_21.speed = LB_21.speed * 2f;
        //        LB_22.speed = LB_22.speed * 2f;
        //        LB_23.speed = LB_23.speed * 2f;
        //        LB_31.speed = LB_31.speed * 2f;
        //        LB_32.speed = LB_32.speed * 2f;
        //        LB_33.speed = LB_33.speed * 2f;
        //        //云
        //        LB_41.speed = LB_41.speed * 2f;
        //        LB_42.speed = LB_42.speed * 2f;
        //        LB_43.speed = LB_43.speed * 2f;
        //        LB_51.speed = LB_51.speed * 2f;
        //        LB_52.speed = LB_52.speed * 2f;
        //        LB_53.speed = LB_53.speed * 2f;
        //        LB_61.speed = LB_61.speed * 2f;
        //        LB_62.speed = LB_62.speed * 2f;
        //        LB_63.speed = LB_63.speed * 2f;
        //        //地
        //        LFG_11.speed = LFG_11.speed * 2f;
        //        LFG_12.speed = LFG_12.speed * 2f;
        //        LFG_21.speed = LFG_21.speed * 2f;
        //        LFG_22.speed = LFG_22.speed * 2f;
        //        //深林
        //        LBFT_11.speed = LBFT_11.speed * 2f;
        //        LBFT_12.speed = LBFT_12.speed * 2f;
        //        LBFT_13.speed = LBFT_13.speed * 2f;
        //        LBFT_21.speed = LBFT_21.speed * 2f;
        //        LBFT_22.speed = LBFT_22.speed * 2f;
        //        LBFT_23.speed = LBFT_23.speed * 2f;
        //        review = false;
        //    }         
        //}

        //稀疏
        if ((totalTime <= 40) && (totalTime > 30))
        {
            sparse_1 = false;
            thick = false;
            sparse_2 = true;
        }
        if ((totalTime <= 41) && (totalTime > 40))
        {
            sparse_2 = false;
        }
            //动作变慢
        // if ((totalTime <= 44) && (totalTime > 42))
        //{
        //    if(!review)
        //    {
        //        //山
        //        LB_11.speed = LB_11.speed * 0.5f;
        //        LB_12.speed = LB_12.speed * 0.5f;
        //        LB_13.speed = LB_13.speed * 0.5f;
        //        LB_21.speed = LB_21.speed * 0.5f;
        //        LB_22.speed = LB_22.speed * 0.5f;
        //        LB_23.speed = LB_23.speed * 0.5f;
        //        LB_31.speed = LB_31.speed * 0.5f;
        //        LB_32.speed = LB_32.speed * 0.5f;
        //        LB_33.speed = LB_33.speed * 0.5f;
        //        //云
        //        LB_41.speed = LB_41.speed * 0.5f;
        //        LB_42.speed = LB_42.speed * 0.5f;
        //        LB_43.speed = LB_43.speed * 0.5f;
        //        LB_51.speed = LB_51.speed * 0.5f;
        //        LB_52.speed = LB_52.speed * 0.5f;
        //        LB_53.speed = LB_53.speed * 0.5f;
        //        LB_61.speed = LB_61.speed * 0.5f;
        //        LB_62.speed = LB_62.speed * 0.5f;
        //        LB_63.speed = LB_63.speed * 0.5f;
        //        //地
        //        LFG_11.speed = LFG_11.speed * 0.5f;
        //        LFG_12.speed = LFG_12.speed * 0.5f;
        //        LFG_21.speed = LFG_21.speed * 0.5f;
        //        LFG_22.speed = LFG_22.speed * 0.5f;
               

        //        review = true;
        //    }
        //}
        //if ((totalTime <= 45) && (totalTime > 44))
        //{
        //    if (review)
        //    {
        //        //山
        //        LB_11.speed = LB_11.speed * 2f;
        //        LB_12.speed = LB_12.speed * 2f;
        //        LB_13.speed = LB_13.speed * 2f;
        //        LB_21.speed = LB_21.speed * 2f;
        //        LB_22.speed = LB_22.speed * 2f;
        //        LB_23.speed = LB_23.speed * 2f;
        //        LB_31.speed = LB_31.speed * 2f;
        //        LB_32.speed = LB_32.speed * 2f;
        //        LB_33.speed = LB_33.speed * 2f;
        //        //云
        //        LB_41.speed = LB_41.speed * 2f;
        //        LB_42.speed = LB_42.speed * 2f;
        //        LB_43.speed = LB_43.speed * 2f;
        //        LB_51.speed = LB_51.speed * 2f;
        //        LB_52.speed = LB_52.speed * 2f;
        //        LB_53.speed = LB_53.speed * 2f;
        //        LB_61.speed = LB_61.speed * 2f;
        //        LB_62.speed = LB_62.speed * 2f;
        //        LB_63.speed = LB_63.speed * 2f;
        //        //地
        //        LFG_11.speed = LFG_11.speed * 2f;
        //        LFG_12.speed = LFG_12.speed * 2f;
        //        LFG_21.speed = LFG_21.speed * 2f;
        //        LFG_22.speed = LFG_22.speed * 2f;
              
        //        review = false;
        //    }
        //}


            //黄河水过来
         if ((totalTime <= 49) && (totalTime > 42))
        {
           
            mR.startMove = true;
            //减速的过程
            LB_11.speed -= 0.3f / (5f / Time.deltaTime);
            LB_12.speed -= 0.3f / (5f / Time.deltaTime);
            LB_13.speed -= 0.3f / (5f / Time.deltaTime);
            LB_21.speed -= 0.2f / (5f / Time.deltaTime);
            LB_22.speed -= 0.2f / (5f / Time.deltaTime);
            LB_23.speed -= 0.2f / (5f / Time.deltaTime);
            LB_31.speed -= 0.1f / (5f / Time.deltaTime);
            LB_32.speed -= 0.1f / (5f / Time.deltaTime);
            LB_33.speed -= 0.1f / (5f / Time.deltaTime);
            //
            LB_41.speed -= 0.15f / (5f / Time.deltaTime);
            LB_42.speed -= 0.15f / (5f / Time.deltaTime);
            LB_43.speed -= 0.15f / (5f / Time.deltaTime);
            LB_51.speed -= 0.1f / (5f / Time.deltaTime);
            LB_52.speed -= 0.1f / (5f / Time.deltaTime);
            LB_53.speed -= 0.1f / (5f / Time.deltaTime);
            LB_61.speed -= 0.05f / (5f / Time.deltaTime);
            LB_62.speed -= 0.05f / (5f / Time.deltaTime);
            LB_63.speed -= 0.05f / (5f / Time.deltaTime);
            //地面的速度
            //LFG_11.speed -= 0.008f;
            //LFG_12.speed -= 0.008f;
            LFG_21.speed -= 0.004f;
            LFG_22.speed -= 0.004f;
            LFG_11.speed = mR.speed;
            LFG_12.speed = mR.speed;
            mR.speed -= 0.0012f;
        }
        //49-57黄河使背景停住
        if ((totalTime <= 57) && (totalTime > 40))
        {
            isStop = true;
        }

        //49-57黄河使背景停住
        if (mR.stop)
        {
            mR.speed = 0;
            LB_11.speed = 0.0f;
            LB_12.speed = 0.0f;
            LB_13.speed = 0.0f;
            LB_21.speed = 0.0f;
            LB_22.speed = 0.0f;
            LB_23.speed = 0.0f;
            LB_31.speed = 0.0f;
            LB_32.speed = 0.0f;
            LB_33.speed = 0.0f;
            //云
            LB_41.speed = 0.3f;
            LB_42.speed = 0.3f;
            LB_43.speed = 0.3f;
            LB_51.speed = 0.2f;
            LB_52.speed = 0.2f;
            LB_53.speed = 0.2f;
            LB_61.speed = 0.1f;
            LB_62.speed = 0.1f;
            LB_63.speed = 0.1f;
            //地面
            LFG_11.speed = 0.0f;
            LFG_12.speed = 0.0f;
            LFG_21.speed = 0.0f;
            LFG_22.speed = 0.0f;

        }

        if ((totalTime <= 57.0) && (totalTime > 52))
        {
            mR.moveDown = true;//水位下降
        }
        //55-60加速阶段改为喝水阶段jiushi静止不动
        if ((totalTime <= 64.0) && (totalTime > 59.0))
        {
            isStop = false;
            mR.stop = false;
           
            //山加到0.4    0.8     1.2
            LB_11.speed += 0.6f / (5f / Time.deltaTime);
            LB_12.speed += 0.6f / (5f / Time.deltaTime);
            LB_13.speed += 0.6f / (5f / Time.deltaTime);
            LB_21.speed += 0.5f / (5f / Time.deltaTime);
            LB_22.speed += 0.5f / (5f / Time.deltaTime);
            LB_23.speed += 0.5f / (5f / Time.deltaTime);
            LB_31.speed += 0.4f / (5f / Time.deltaTime);
            LB_32.speed += 0.4f / (5f / Time.deltaTime);
            LB_33.speed += 0.4f / (5f / Time.deltaTime);
            //云加到0.6    1       1.4
            LB_41.speed += 0.45f / (5f / Time.deltaTime);
            LB_42.speed += 0.45f / (5f / Time.deltaTime);
            LB_43.speed += 0.45f / (5f / Time.deltaTime);
            LB_51.speed += 0.4f / (5f / Time.deltaTime);
            LB_52.speed += 0.4f / (5f / Time.deltaTime);
            LB_53.speed += 0.4f / (5f / Time.deltaTime);
            LB_61.speed += 0.35f / (5f / Time.deltaTime);
            LB_62.speed += 0.35f / (5f / Time.deltaTime);
            LB_63.speed += 0.35f / (5f / Time.deltaTime);
            //地面加到10    5
            LFG_11.speed += 10f / (5f / Time.deltaTime);
            LFG_12.speed += 10f / (5f / Time.deltaTime);
            LFG_21.speed += 5f / (5f / Time.deltaTime);
            LFG_22.speed += 5f / (5f / Time.deltaTime);
            mR.speed += 10f / (5f / Time.deltaTime);
        }
      


            //高树开始出现:从稀疏到茂密到稀疏
            if ((totalTime <= 70.0) && (totalTime > 65))
        {
            LocustSparse_1 = true;
        }
        if ((totalTime <= 78) && (totalTime > 70))
        {
            LocustSparse_1 = false;
            LocustThick = true;
        }
        if ((totalTime <= 83) && (totalTime > 78))
        {
            LocustThick = false;
            LocustSparse_2 = true;
        }

        //沙地时间
        if ((totalTime <= 88) && (totalTime > 83))
        {
            LocustSparse_2 = false;
        }

        //石头速度8
        if ((totalTime <= 120) && (totalTime > 88))
        {
            sT.move = true;
            sT_2.move = true;
        }
        //荆棘速度12
        if ((totalTime <= 120) && (totalTime > 93))
        {
            th_1.move = true;
            th_2.move = true;
        }
        if ((totalTime <= 114) && (totalTime > 109))
        {
            //减速的过程
            LB_11.speed -= 0.6f / (5f / Time.deltaTime);
            LB_12.speed -= 0.6f / (5f / Time.deltaTime);
            LB_13.speed -= 0.6f / (5f / Time.deltaTime);
            LB_21.speed -= 0.5f / (5f / Time.deltaTime);
            LB_22.speed -= 0.5f / (5f / Time.deltaTime);
            LB_23.speed -= 0.5f / (5f / Time.deltaTime);
            LB_31.speed -= 0.4f / (5f / Time.deltaTime);
            LB_32.speed -= 0.4f / (5f / Time.deltaTime);
            LB_33.speed -= 0.4f / (5f / Time.deltaTime);
            //
            LB_41.speed -= 0.45f / (5f / Time.deltaTime);
            LB_42.speed -= 0.45f / (5f / Time.deltaTime);
            LB_43.speed -= 0.45f / (5f / Time.deltaTime);
            LB_51.speed -= 0.4f / (5f / Time.deltaTime);
            LB_52.speed -= 0.4f / (5f / Time.deltaTime);
            LB_53.speed -= 0.4f / (5f / Time.deltaTime);
            LB_61.speed -= 0.35f / (5f / Time.deltaTime);
            LB_62.speed -= 0.35f / (5f / Time.deltaTime);
            LB_63.speed -= 0.35f / (5f / Time.deltaTime);
            //
            LFG_11.speed -= 10f / (5f / Time.deltaTime);
            LFG_12.speed -= 10f / (5f / Time.deltaTime);
            LFG_21.speed -= 5f / (5f / Time.deltaTime);
            LFG_22.speed -= 5f / (5f / Time.deltaTime);

            //mU.speed = 2;
        }
        if ((totalTime <= 115) && (totalTime > 114))
        {
            if(!review)
            {
                LB_11.speed = 0.0f;
                LB_12.speed = 0.0f;
                LB_13.speed = 0.0f;
                LB_21.speed = 0.0f;
                LB_22.speed = 0.0f;
                LB_23.speed = 0.0f;
                LB_31.speed = 0.0f;
                LB_32.speed = 0.0f;
                LB_33.speed = 0.0f;
                //云
                LB_41.speed = 0.3f;
                LB_42.speed = 0.3f;
                LB_43.speed = 0.3f;
                LB_51.speed = 0.2f;
                LB_52.speed = 0.2f;
                LB_53.speed = 0.2f;
                LB_61.speed = 0.1f;
                LB_62.speed = 0.1f;
                LB_63.speed = 0.1f;
                //地面
                LFG_11.speed = 0.0f;
                LFG_12.speed = 0.0f;
                LFG_21.speed = 0.0f;
                LFG_22.speed = 0.0f;
               
                review = true;
            }

        }
       
            //让视野往左移
            if ((totalTime <= 119) && (totalTime > 117))
        {
            
            LB_11.moveBack=true;
            LB_12.moveBack=true;
            LB_13.moveBack=true;
            LB_21.moveBack=true;
            LB_22.moveBack=true;
            LB_23.moveBack=true;
            LB_31.moveBack=true;
            LB_32.moveBack=true;
            LB_33.moveBack = true;

            LFG_11.moveBack=true;
            LFG_12.moveBack=true;
            LFG_21.moveBack=true;
            LFG_22.moveBack = true;


        }
        if ((totalTime > 117))
        {
            if (review)
            {
                LB_11.speed = 0.6f;
                LB_12.speed = 0.6f;
                LB_13.speed = 0.6f;
                LB_21.speed = 0.5f;
                LB_22.speed = 0.5f;
                LB_23.speed = 0.5f;
                LB_31.speed = 0.4f;
                LB_32.speed = 0.4f;
                LB_33.speed = 0.4f;

                //地面
                LFG_11.speed = 10.0f;
                LFG_12.speed = 10.0f;
                LFG_21.speed = 5.0f;
                LFG_22.speed = 5.0f;

                review = false;
            }
        }
        //桃林时间
        // 
        if ((totalTime <= 121) && (totalTime > 119))
        {
            LB_11.speed = 0.0f;
            LB_12.speed = 0.0f;
            LB_13.speed = 0.0f;
            LB_21.speed = 0.0f;
            LB_22.speed = 0.0f;
            LB_23.speed = 0.0f;
            LB_31.speed = 0.0f;
            LB_32.speed = 0.0f;
            LB_33.speed = 0.0f;
            //云
            LB_41.speed = 0.3f;
            LB_42.speed = 0.3f;
            LB_43.speed = 0.3f;
            LB_51.speed = 0.2f;
            LB_52.speed = 0.2f;
            LB_53.speed = 0.2f;
            LB_61.speed = 0.1f;
            LB_62.speed = 0.1f;
            LB_63.speed = 0.1f;
            //地面
            LFG_11.speed = 0.0f;
            LFG_12.speed = 0.0f;
            LFG_21.speed = 0.0f;
            LFG_22.speed = 0.0f;
            mU.speed = 2;
        }

        if (totalTime >119)
        {
            UIManager.Instance.PushPanel(UIPanelType.vectory);
            Time.timeScale = 0;
            AudioManager.Instance.Stop();
        }

    }
}
