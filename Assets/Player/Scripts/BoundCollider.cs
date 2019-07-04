using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCollider : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.localRotation = player.transform.localRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //如果触碰了界面的碰撞
        if (collision.collider.tag == "backGround")
        {
            Debug.Log("eeee");
            Debug.Log("碰撞！！！");
            if(player.name.Equals("player1"))
            {
                player.GetComponent<PlayerControl>().isMove = true;
                player.GetComponent<PlayerControl>().CanCollider = false;
            }
            if (player.name.Equals("player2"))
            {
                player.GetComponent<Player2Control>().isMove = true;
                player.GetComponent<Player2Control>().CanCollider = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //离开碰撞
        Debug.Log("emmm");
        if (collision.collider.tag == "backGround")
        {
            Debug.Log("离开碰撞！！！");
            if(player.name.Equals("player1"))
            {
                player.GetComponent<PlayerControl>().isMove = false;
                player.GetComponent<PlayerControl>().CanCollider = true;
            }
            if (player.name.Equals("player2"))
            {
                player.GetComponent<Player2Control>().isMove = false;
                player.GetComponent<Player2Control>().CanCollider = true;
            }
        }
    }
}
