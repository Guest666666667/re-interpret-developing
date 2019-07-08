using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControl;

public class HitControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player = null;
    public GameObject other = null;
    private GameObject rightHand = null;
    private GameObject leftHand = null;

    void Start()
    {
        leftHand = GameObject.Find(other.name + "/Skeleton/rootBone/leftArm/leftArm2/leftHand");
        rightHand = GameObject.Find(other.name + "/Skeleton/rootBone/rightArm/rightArm2/rightHand");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int damage1 = 0, damage2 = 0;
        if (name.Equals("rootBone"))
        {
            damage1 = BattlePara.bodyDamage1;
            damage2 = BattlePara.bodyDamage2;
        }
        if (name.Equals("head"))
        {
            damage1 = BattlePara.headDamage1;
            damage2 = BattlePara.headDamage2;
        }
        if (name.Equals("leftArm")|| name.Equals("rightArm"))
        {
            damage1 = BattlePara.armDamage1;
            damage2 = BattlePara.armDamage2;
        }

        State otherState = 0, playerState = 0;

        if (other.name.Equals("player1"))
        {
            otherState = other.GetComponent<PlayerControl>().GetState();
            playerState = player.GetComponent<Player2Control>().GetState();
        }
        if (other.name.Equals("player2"))
        {
            otherState = other.GetComponent<Player2Control>().GetState();
            playerState = player.GetComponent<PlayerControl>().GetState();
        }

        Debug.Log(collision.collider);

        if ((collision.collider.Equals(rightHand.GetComponent<Collider2D>())
            || collision.collider.Equals(leftHand.GetComponent<Collider2D>()))
            )
        {
            if (player.name.Equals("player1"))
            {
                Debug.Log("player1");
                player.GetComponent<PlayerControl>().HitFly(damage1);
            }
            if (player.name.Equals("player2"))
            {
                Debug.Log("player2");
                player.GetComponent<Player2Control>().HitFly(damage2);
            }
        }
    }

    public void hitted()
    {
        int damage1 = 0, damage2 = 0;
        if (name.Equals("rootBone"))
        {
            damage1 = BattlePara.bodyDamage1;
            damage2 = BattlePara.bodyDamage2;
        }
        if (name.Equals("head"))
        {
            damage1 = BattlePara.headDamage1;
            damage2 = BattlePara.headDamage2;
        }
        if (name.Equals("leftArm") || name.Equals("rightArm"))
        {
            damage1 = BattlePara.armDamage1;
            damage2 = BattlePara.armDamage2;
        }
        if (player.name.Equals("player1"))
        {
            if(BattlePara.scene3.Equals(BattlePara.Scene.艳阳))
            {
                damage1 += 15;
            }
            player.GetComponent<PlayerControl>().Hit(damage1);
        }
        if (player.name.Equals("player2"))
        {
            if (BattlePara.scene3.Equals(BattlePara.Scene.艳阳))
            {
                damage2 += 15;
            }
            player.GetComponent<Player2Control>().Hit(damage2);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {

        int damage = 0;
        if(name.Equals("rootBone"))
        {
            damage = BattlePara.GetBodyDamage();
        }
        if (name.Equals("head"))
        {
            damage = BattlePara.GetHeadDamage();
        }

        State otherState = 0,playerState = 0;

        if (other.name.Equals("player1"))
        {
            otherState = other.GetComponent<PlayerControl>().GetState();
            playerState = player.GetComponent<Player2Control>().GetState();
        }
        if (other.name.Equals("player2"))
        {
            otherState = other.GetComponent<Player2Control>().GetState();
            playerState = player.GetComponent<PlayerControl>().GetState();
        }

        if ((collision.GetComponent<Collider2D>().Equals(rightHand.GetComponent<Collider2D>())
            || collision.GetComponent<Collider2D>().Equals(leftHand.GetComponent<Collider2D>()))
            && (otherState.Equals(State.attack))
            && (!playerState.Equals(State.guard)))
        {
            if (player.name.Equals("player1"))
            {
                player.GetComponent<PlayerControl>().HitFly(damage);
            }
            if (player.name.Equals("player2"))
            {
                player.GetComponent<Player2Control>().HitFly(damage);
            }
        }
    }*/
}
