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

    public static float moveSpeed2;
    public static float jumpSpeed2;
    public static float chargeSpeed2;
    public static int bodyDamage2;
    public static int headDamage2;
    public static int guardDamage2;
    public static int armDamage2;

    private GameObject attributeManager;
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
    private int[] P1DefaultValue = { 100, 15, 3, 5, 5, 10, 12 };
    private int[] P2DefaultValue = { 100, 15, 3, 5, 5, 10, 12 };
    private int[] SceneDefaultAttribute = { 0, 0, 0 };
    private int[] P1ValueArray = { 100, 15, 3, 5, 5, 10, 12 };
    private int[] P2ValueArray = { 100, 15, 3, 5, 5, 10, 12 };
    public int[] SceneAttribute = { 0, 0, 0 };//第一个位置为远景，中间位置为天气，第三个为近景
                                               //远景列表：
                                               //天气列表：云，月，蚀，阳
                                               //近景列表：高树，桃林，荆棘

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

    public void init()
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
    }
}
