using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControl;

public class GuardControl : MonoBehaviour
{
    public GameObject player = null;
    public GameObject other = null;
    private GameObject rightHand = null;
    private GameObject leftHand = null;

    // Start is called before the first frame update
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
        State playerState = 0, otherState = 0;

        if (player.name.Equals("player1"))
        {
            playerState = player.GetComponent<PlayerControl>().GetState();
            otherState = other.GetComponent<Player2Control>().GetState();
        }
        if (player.name.Equals("player2"))
        {
            playerState = player.GetComponent<Player2Control>().GetState();
            otherState = other.GetComponent<PlayerControl>().GetState();
        }

        if ((collision.collider.Equals(rightHand.GetComponent<Collider2D>())
            || collision.collider.Equals(leftHand.GetComponent<Collider2D>()))
            )
        {
            if (other.name.Equals("player1"))
            {
                other.GetComponent<PlayerControl>().HitFly(BattlePara.GetGuardDamage());

            }
            if (other.name.Equals("player2"))
            {
                other.GetComponent<Player2Control>().HitFly(BattlePara.GetGuardDamage());
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        State playerState = 0, otherState = 0;

        if (player.name.Equals("player1"))
        {
            playerState = player.GetComponent<PlayerControl>().GetState();
            otherState = other.GetComponent<Player2Control>().GetState();
        }
        if (player.name.Equals("player2"))
        {
            playerState = player.GetComponent<Player2Control>().GetState();
            otherState = other.GetComponent<PlayerControl>().GetState();
        }

        if ((collision.GetComponent<Collider2D>().Equals(rightHand.GetComponent<Collider2D>())
            || collision.GetComponent<Collider2D>().Equals(leftHand.GetComponent<Collider2D>()))
            && (playerState.Equals(State.guard))
            && (otherState.Equals(State.attack)))
        {
            if (other.name.Equals("player1"))
            {
                other.GetComponent<PlayerControl>().HitFly(BattlePara.GetGuardDamage());

            }
            if (other.name.Equals("player2"))
            {
                other.GetComponent<Player2Control>().HitFly(BattlePara.GetGuardDamage());
            }
        }
    }*/
}
