using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForCloud : MonoBehaviour
{
    public float speed = 5f;
    //public float speed_1 = 5f;
    //public float speed_2 = 5f;
    public bool isMove = false;//player_1
    public bool isMove_2 = false;//player_2
    private PlayerControl moveScript;//player_1
    private Player2Control moveScript_2;//player_2
    private moveGrounds mG;//地面的脚本

    // Start is called before the first frame update
    void Start()
    {
        //获得两个player的脚本对象
        moveScript = GameObject.FindWithTag("player").GetComponent<PlayerControl>();
        moveScript_2 = GameObject.FindWithTag("player_2").GetComponent<Player2Control>();
        mG = GameObject.FindWithTag("grounds").GetComponent<moveGrounds>();
    }

    // Update is called once per frame
    void Update()
    {
        //speed_1 = BattlePara.moveSpeed1;
        //speed_2 = BattlePara.moveSpeed2;
        //云要自己动
        Vector2 v2 = transform.localPosition;
        v2.x -= speed * Time.deltaTime;
        if(v2.x<-57.6)
        {
            v2.x += 6 * 19.2f;
            //销毁
            //foreach (Transform trans in transform)
            //{
            //    Destroy(trans.gameObject);
            //}
            //创建新地形
            //Instantiate(images[Random.Range(0, images.Length)], transform);
        }
        transform.localPosition = v2;

        isMove = moveScript.isMove;
        isMove_2 = moveScript_2.isMove;
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
                {
                    //v.x -= speed * Time.deltaTime;
                }

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
                {
                  // v.x += speed * Time.deltaTime;
                }

            }

            transform.localPosition = v;
        }
    }
    public void MoveBackGround()
    {
        if (BattlePara.scene3.Equals(BattlePara.Scene.日蚀))
        {

            if (Input.GetAxis("Horizontal") < 0)
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
            if (Input.GetAxis("Horizontal") > 0)
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
        }
        else
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
}
