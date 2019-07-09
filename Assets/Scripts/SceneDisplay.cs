using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDisplay : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;
    private string[] t1 = {
        "远山：能量值恢复速度加快1倍",
        "雪岭：移动速度和跳跃高度下降30%",
        "险峰：移动速度提高30%，防御下降1"
    };
    private string[] t2 = {
        "高树：双方玩家不能使用飞刀",
        "荆棘：每秒有40%几率失去2点生命值",
        "桃林：每次受伤有20%几率恢复5点生命"
    };
    private string[] t3 = {
        "艳阳：飞刀的攻击力提升15点",
        "日蚀：双方玩家左右键交换",
        "新月：无特殊效果",
        "多云：双方玩家初始能量和特技值全满"
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(BattlePara.scene1)
        {
            case BattlePara.Scene.远山:
                text1.text = t1[0];
                break;
            case BattlePara.Scene.雪岭:
                text1.text = t1[1];
                break;
            case BattlePara.Scene.险峰:
                text1.text = t1[2];
                break;
        }
        switch (BattlePara.scene2)
        {
            case BattlePara.Scene.高树:
                text2.text = t2[0];
                break;
            case BattlePara.Scene.荆棘:
                text2.text = t2[1];
                break;
            case BattlePara.Scene.桃林:
                text2.text = t2[2];
                break;
        }
        switch (BattlePara.scene3)
        {
            case BattlePara.Scene.艳阳:
                text3.text = t3[0];
                break;
            case BattlePara.Scene.日蚀:
                text3.text = t3[1];
                break;
            case BattlePara.Scene.新月:
                text3.text = t3[2];
                break;
            case BattlePara.Scene.多云:
                text3.text = t3[3];
                break;
        }
    }
}
