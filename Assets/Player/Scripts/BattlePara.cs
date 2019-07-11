using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePara : MonoBehaviour
{
    public static float moveSpeed1;
    public static float jumpSpeed1;
    public static float chargeSpeed1;
    public static int bodyDamage1;
    public static int headDamage1;
    public static int guardDamage1;
    public static int armDamage1;
    public static float[] player1MotionCost = new float[3]; 

    public static float moveSpeed2;
    public static float jumpSpeed2;
    public static float chargeSpeed2;
    public static int bodyDamage2;
    public static int headDamage2;
    public static int guardDamage2;
    public static int armDamage2;
    public static float[] player2MotionCost = new float[3];

    private static GameObject attributeManager;
    /// <summary>
    ///      生命值，攻击力，头防，胸防，后臂防，跳跃高度，移速
    ///   头{ 1, 1, 1, 1, 1, 1, 1 }
    ///   胸{ 1, 1, 1, 1, 1, 1, 1 }
    ///   腹{ 1, 1, 1, 1, 1, 1, 1 }
    /// 前臂{ 1, 1, 1, 1, 1, 1, 1 }
    /// 后臂{ 1, 1, 1, 1, 1, 1, 1 }
    ///   手{ 1, 1, 1, 1, 1, 1, 1 }
    /// 左腿{ 1, 1, 1, 1, 1, 1, 1 }
    /// 右腿{ 1, 1, 1, 1, 1, 1, 1 }
    /// </summary>
    private static int[] P1DefaultValue = { 100, 15, 3, 5, 5, 10, 12 };
    private static int[] P2DefaultValue = { 100, 15, 3, 5, 5, 10, 12 };
    private static int[] SceneDefaultAttribute = { 0, 0, 0 };
    private static int[] P1ValueArray = { 100, 15, 3, 5, 5, 10, 12 };
    private static int[] P2ValueArray = { 100, 15, 3, 5, 5, 10, 12 };
    public static int[] SceneAttribute = { 0, 0, 0 };//第一个位置为远景，中间位置为天气，第三个为近景
                                                     //远景列表：
                                                     //天气列表：云，月，蚀，阳
                                                     //近景列表：高树，桃林，荆棘

    public enum Scene
    {
        远山,雪岭,险峰,
        高树,荆棘,桃林,
        艳阳,日蚀,新月,多云,
        无
    }

    public static Scene scene1 = Scene.远山; //远景
    public static Scene scene2 = Scene.高树; //近景
    public static Scene scene3 = Scene.艳阳; //天气

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed1 = 1; jumpSpeed1 = 1; chargeSpeed1 = 1; bodyDamage1 = 10; headDamage1 = 20; guardDamage1 = 10; armDamage1 = 10;
        moveSpeed2 = 1; jumpSpeed2 = 1; chargeSpeed2 = 1; bodyDamage2 = 10; headDamage2 = 20; guardDamage2 = 10; armDamage2 = 10;
        attributeManager = GameObject.Find("attributeManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Init()
    {
        Attribute a = attributeManager.GetComponent<Attribute>();
        P1ValueArray = a.getP1Attribute();
        P2ValueArray = a.getP2Attribute();
        SceneAttribute = a.getSceneAttribute();

        moveSpeed1 = 1 * (P1ValueArray[6]*1.0f)/(P1DefaultValue[6]*1.0f);
        jumpSpeed1 = 1 * (P1ValueArray[5] * 1.0f) / (P1DefaultValue[5] * 1.0f); ;
        chargeSpeed1 = 1;
        bodyDamage1 = P2ValueArray[1] - P1ValueArray[3];
        headDamage1 = P2ValueArray[1] - P1ValueArray[2];
        guardDamage1 = 10;
        armDamage1 = P2ValueArray[1] - P1ValueArray[4];

        moveSpeed2 = 1 * (P2ValueArray[6] * 1.0f) / (P2DefaultValue[6] * 1.0f);
        jumpSpeed2 = 1 * (P2ValueArray[5] * 1.0f) / (P2DefaultValue[5] * 1.0f); ;
        chargeSpeed2 = 1;
        bodyDamage2 = P1ValueArray[1] - P2ValueArray[3];
        headDamage2 = P1ValueArray[1] - P2ValueArray[2];
        guardDamage2 = 10;
        armDamage2 = P1ValueArray[1] - P2ValueArray[4];

        int[][] blueCost = a.getPoseOffset();
        for(int i=0;i<3;i++)
        {
            float temp1 = blueCost[0][i], temp2 = blueCost[1][i];
            player1MotionCost[i] = temp1 / 100.0f; player2MotionCost[i] = temp2 / 100.0f;
        }

        //第一个位置为远景，中间位置为天气，第三个为近景
        //远景列表：
        //天气列表：云，月，蚀，阳
        //近景列表：高树，桃林，荆棘

        for(int i=0;i<3;i++)
        {
            Debug.Log("Scene "+ (i+1) + ": " + SceneAttribute[i]);
        }

        switch (SceneAttribute[0])
        {
            case 0:
                scene1 = Scene.雪岭;
                break;
            case 1:
                scene1 = Scene.远山;
                break;
            case 2:
                scene1 = Scene.险峰;
                break;
        }
        switch (SceneAttribute[1])
        {
            case 0:
                scene3 = Scene.多云;
                break;
            case 1:
                scene3 = Scene.新月;
                break;
            case 2:
                scene3 = Scene.日蚀;
                break;
            case 3:
                scene3 = Scene.艳阳;
                break;
        }
        switch (SceneAttribute[2])
        {
            case 0:
                scene2 = Scene.高树;
                break;
            case 1:
                scene2 = Scene.桃林;
                break;
            case 2:
                scene2 = Scene.荆棘;
                break;
        }

        if(scene1.Equals(Scene.远山))
        {
            chargeSpeed1 *= 2.0f; chargeSpeed2 *= 2.0f;
        }
        if (scene1.Equals(Scene.雪岭))
        {
            moveSpeed1 *= 0.7f; moveSpeed2 *= 0.7f;
            jumpSpeed1 *= 0.7f; jumpSpeed2 *= 0.7f;
        }
        if (scene1.Equals(Scene.险峰))
        {
            moveSpeed1 *= 1.3f; moveSpeed2 *= 1.3f;
            bodyDamage1 += 1; headDamage1 += 1; armDamage1 += 1;
            bodyDamage2 += 1; headDamage2 += 1; armDamage2 += 1;
        }

        if(scene3.Equals(BattlePara.Scene.新月))
        {
            scene1 = Scene.无;
            scene2 = Scene.无;
        }
    }
}
