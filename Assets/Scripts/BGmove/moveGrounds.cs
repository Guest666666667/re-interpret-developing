using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGrounds : MonoBehaviour
{
    //这个脚本的目的是维护一个bool值让远景能不再动
    public bool canMove = true;//如果地面到头了这个值就是false,只有地面能动才是true

    public float speed = 5f;
    public bool isMove = false;//player_1
    public bool isMove_2 = false;//player_2
    private PlayerControl moveScript;//player_1
    private Player2Control moveScript_2;//player_2
    // Start is called before the first frame update
    void Start()
    {
        //获得两个player的脚本对象
        moveScript = GameObject.FindWithTag("player").GetComponent<PlayerControl>();
        moveScript_2 = GameObject.FindWithTag("player_2").GetComponent<Player2Control>();
    }

    // Update is called once per frame
    void Update()
    {
        canMove = false;
        isMove = moveScript.isMove;
        isMove_2 = moveScript_2.isMove;
        //isMove = transform.Find("player").gameObject.GetComponent<Move>().isMove;
        if (isMove ^ isMove_2)
        {
            MoveBackGround();//如果两个人有一个移动，背景就移动
        }
        //如果player_1没有碰到墙壁并且往右移动，背景往左移动
        if ((!moveScript.isMove) && (moveScript.direction == 1))
        {

            Vector2 v = transform.localPosition;
            //如果没有出边界
            if (v.x > -19.2)
            {
                canMove = true;
                v.x -= speed * Time.deltaTime * 0.5f;
            }

            transform.localPosition = v;
        }
        //如果player_2没有碰到墙壁并且往左移动，背景往右移动
        if ((!moveScript_2.isMove) && (moveScript_2.direction == -1))
        {
            Vector2 v = transform.localPosition;
            //如果没有出边界
            if (v.x < 19.2)
            {
                canMove = true;
                v.x += speed * Time.deltaTime * 0.5f;
            }

            transform.localPosition = v;
        }
    }
    public void MoveBackGround()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            Vector2 v = transform.localPosition;
            //如果没有出边界
            if (v.x < 19.2)
            {
                canMove = true;
                v.x += speed * Time.deltaTime;
            }

            transform.localPosition = v;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            Vector2 v = transform.localPosition;
            //如果没有出边界
            if (v.x > -19.2)
            {
                canMove = true;
                v.x -= speed * Time.deltaTime;
            }

            transform.localPosition = v;
        }

    }
}
