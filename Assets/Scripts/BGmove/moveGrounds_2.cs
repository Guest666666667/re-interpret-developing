using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGrounds_2 : MonoBehaviour
{
    public float speed = 2.5f;
    public bool isMove = false;//player_1
    public bool isMove_2 = false;//player_2
    private Move moveScript;//player_1
    private Move_2 moveScript_2;//player_2
    private moveGrounds mG;//地面的脚本

    // Start is called before the first frame update
    void Start()
    {
        //获得两个player的脚本对象
        moveScript = GameObject.FindWithTag("player").GetComponent<Move>();
        moveScript_2 = GameObject.FindWithTag("player_2").GetComponent<Move_2>();
        mG = GameObject.FindWithTag("grounds").GetComponent<moveGrounds>();
    }

    // Update is called once per frame
    void Update()
    {
        isMove = moveScript.isMove;
        isMove_2 = moveScript_2.isMove;
        //isMove = transform.Find("player").gameObject.GetComponent<Move>().isMove;
        if (isMove ^ isMove_2)
        {
            if (mG.canMove)
            {
                MoveBackGround();//如果两个人有一个移动，背景就移动
            }

        }
        //如果player_1没有碰到墙壁并且往右移动，背景往左移动
        if ((!moveScript.isMove) && (moveScript.direction == 1))
        {

            Vector2 v = transform.localPosition;
            //如果没有出边界
            if (v.x > -19.2)
            {
                if (mG.canMove)
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
                if (mG.canMove)
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
                if (mG.canMove)
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
                if (mG.canMove)
                    v.x -= speed * Time.deltaTime;
            }

            transform.localPosition = v;
        }

    }

}
