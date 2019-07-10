﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
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
    private static int[,] guanyuAttribute = { { 15, 0, 3, 0, 0, 0, 0 },
                                              { 35, 0, 0, 5, 0, 0, 0 },
                                              { 20, 0, 0, 0, 0, 4, 2 },
                                              { 10, 5, 0, 0, 0, 0, 0 },
                                              { 15, 0, 0, 0, 5, 0, 0 },
                                              { 5, 10, 0, 0, 0, 0, 0 },
                                              { 0, 0, 0, 0, 0, 3, 5 },
                                              { 0, 0, 0, 0, 0, 3, 5 } };//关羽

    private static int[,] daqiaoAttribute = { { 18, 0, 2, 0, 0, 0, 0 },
                                              { 38, 0, 0, 4, 0, 0, 0 },
                                              { 10, 0, 0, 0, 0, 4, 2 },
                                              { 0, 7, 0, 0, 0, 0, 0 },
                                              { 18, 0, 0, 0, 4, 0, 0 },
                                              { 0, 13, 0, 0, 0, 0, 0 },
                                              { 0, 0, 0, 0, 0, 2, 6 },
                                              { 0, 0, 0, 0, 0, 2, 6 } };//大乔

    private static int[,] wukongAttribute = { { 10, 0, 5, 0, 0, 0, 0 },
                                              { 30, 0, 0, 7, 0, 0, 0 },
                                              { 20, 0, 0, 0, 0, 4, 2 },
                                              { 15, 4, 0, 0, 0, 0, 0 },
                                              { 10, 0, 0, 0, 7, 0, 0 },
                                              { 20, 8, 0, 0, 0, 0, 0 },
                                              { 0, 0, 0, 0, 0, 4, 4 },
                                              { 0, 0, 0, 0, 0, 4, 4 } };//悟空

    private static int[][,] totalAttribute = new int[3][,];
    private int[] P1ValueArray = { 100, 15, 3, 5, 5, 10, 12 };
    private int[] P2ValueArray = { 100, 15, 3, 5, 5, 10, 12 };
    private int[] SceneAttribute = { 1, 0, 1 };//第一个位置为远景，中间位置为天气，第三个为近景
                                               //远景列表：雪岭，远山，险峰
                                               //天气列表：云，月，蚀，阳
                                               //近景列表：高树，桃林，荆棘
    private int[][] poseOffset = new int[2][];//0为P1,1为P2；姿势偏差蓝量，用于蓝控，顺序：JKL
    private int[] compareArray = { -152, -157, 167, 145, -270, 0 };//中间23位不管，只管左手
    private bool isPlayer1;//true为1,false为二
    private int timeCount;
    // Start is called before the first frame update
    void Start()
    {
        totalAttribute[0] = guanyuAttribute;
        totalAttribute[1] = daqiaoAttribute;
        totalAttribute[2] = wukongAttribute;
        poseOffset[0] = new int[3];//player1
        poseOffset[1] = new int[3];//player2
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 3; j++)
                poseOffset[i][j] = 0;
        //totalAttribute[index[i]][i,j],index[i]为部件指定的角色序号，i为部件,j为属性序号
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getNewValue(int[] index,int attributeIndex,int player)
    {
        int value = 0;
        for (int i = 0; i < 8; i++)
        {
            value += totalAttribute[index[i]][i, attributeIndex];
        }
        //储存数值
        if (player == 1)
        {
            P1ValueArray[attributeIndex] = value;
        }
        else
        {
            P2ValueArray[attributeIndex] = value;
        }
        return value;
    }
    public int[] setTotal(int[] index, int player)
    {
        if (player == 1)
        {
            for (int i = 0; i < 7; i++)
            {
                int value = 0;
                for (int j = 0; j < 8; j++)
                {
                    value += totalAttribute[index[j]][j, i];
                }
                P1ValueArray[i] = value;
            }
            return P1ValueArray;
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                int value = 0;
                for (int j = 0; j < 8; j++)
                {
                    value += totalAttribute[index[j]][j, i];
                }
                P2ValueArray[i] = value;
            }
            return P2ValueArray;
        }
    }
    public void setSceneArray(int changePartIndex,int value)
    {
        SceneAttribute[changePartIndex] = value;
    }
    public void setPoseArray(int[][] value,int player)
    {
        if (player == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (k > 1)
                    {
                        poseOffset[0][i] += Mathf.Abs(value[i][k] - compareArray[k]);
                    }
                }
                if (poseOffset[0][i] / 10 < 10)
                    poseOffset[0][i] = poseOffset[0][i] / 10;
                else
                {
                    poseOffset[0][i] = 10;
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (k > 1)
                    {
                        poseOffset[1][i] += Mathf.Abs(value[i][k] - compareArray[k]);
                    }
                }
                if (poseOffset[1][i] / 10 < 10)
                    poseOffset[1][i] = poseOffset[1][i] / 10;
                else
                {
                    poseOffset[1][i] = 10;
                }
            }
        }

        Debug.Log("OFFSET: " + poseOffset[0][0]);
        Debug.Log("OFFSET: " + poseOffset[0][1]);

    }
    public int[] getP1Attribute()
    {
        return P1ValueArray;
    }
    public int[] getP2Attribute()
    {
        return P2ValueArray;
    }
    public int[] getSceneAttribute()
    {
        return SceneAttribute;
    }
    public int[][] getPoseOffset()
    {
        return poseOffset;
    }
    public void setWhoWin(bool a)
    {
        isPlayer1 = a;
    }
    public bool getWhoWin()
    {
        return isPlayer1;
    }
    public void setTime(int t)
    {
        timeCount = t;
    }
    public int getTime()
    {
        return timeCount;
    }
}
