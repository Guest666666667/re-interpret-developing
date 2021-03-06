﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text timeT;

    private int player1Health = 100;
    private int max1 = 100;
    private int player2Health = 100;
    private int max2 = 100;
    private BloodBar bloodBar1;
    private BloodBar bloodBar2;

    private bool isPlaying = false;
    private float time = 0f;
    private float maxTime = 100f;

    private HurtEffect hurtEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        bloodBar1= GameObject.FindWithTag("BloodBar_1").GetComponent<BloodBar>();
        bloodBar2 = GameObject.FindWithTag("BloodBar_2").GetComponent<BloodBar>();
        hurtEffect = GameObject.FindWithTag("HurtEffect").GetComponent<HurtEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = "Health:" + player1Health;
        text2.text = "Health:" + player2Health;

        timeT.text = "" + (int)(maxTime - time);

        if (isPlaying)
        {
            time += Time.deltaTime;
        }

        if(time >= maxTime)
        {
            Attribute a = GameObject.Find("/attributeManager").GetComponent<Attribute>();

            a.setTime((int)time);

            if (player1Health >= player2Health)
            {
                a.setWhoWin(true);
            }
            else
            {
                a.setWhoWin(false);
            }

            player1Health = max1; player2Health = max2;

            Time.timeScale = 0;

            time = 0;

            UIManager.Instance.PushPanel(UIPanelType.fightFinal);
        }

        if(player1Health == 0 || player2Health == 0)
        {
            Attribute a = GameObject.Find("/attributeManager").GetComponent<Attribute>();

            a.setTime((int)time);

            if (player1Health == 0)
            {
                a.setWhoWin(false);
            }
            else
            {
                a.setWhoWin(true);
            }

            player1Health = max1; player2Health = max2;
            Time.timeScale = 0;
            UIManager.Instance.PushPanel(UIPanelType.fightFinal);

        }
    }

    public void init()
    {
        GameObject attributeManager = GameObject.Find("attributeManager");
        Attribute a = attributeManager.GetComponent<Attribute>();
        int[] P1ValueArray = a.getP1Attribute();
        int[] P2ValueArray = a.getP2Attribute();
        player1Health = P1ValueArray[0];
        player2Health = P2ValueArray[0];
        max1 = P1ValueArray[0];
        max2 = P2ValueArray[0];
        isPlaying = true;
    }

    public void damage(string player, int damage)
    {
        if(player.Equals("player1"))
        {
            if(player1Health-damage<=0)
            {
                bloodBar1.changeBlood(1.0f * (player1Health-0) / max1);
                hurtEffect.player1Hurt();
                player1Health = 0;
            }
            else
            {
                player1Health -= damage;
                bloodBar1.changeBlood(1.0f* damage / max1);
                hurtEffect.player1Hurt();
            }
        }
        if (player.Equals("player2"))
        {
            if (player2Health - damage <= 0)
            {
                bloodBar2.changeBlood(1.0f * (player2Health - 0) / max2);
                hurtEffect.player2Hurt();
                player2Health = 0;
            }
            else
            {
                player2Health -= damage;
                bloodBar2.changeBlood(1.0f * damage / max2);
                hurtEffect.player2Hurt();
            }
        }
    }
}
