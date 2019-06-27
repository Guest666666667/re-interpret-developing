using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_2 : MonoBehaviour
{
    public float speed = 5.0f;//前进的速度
    //private float transiation;
    public bool isMove = false;
    private Move moveScript;//player_1
    private moveGrounds mG;
    public int direction=0;//移动的方向
    private bool CanCollider = true;//用来判断自身是否碰到便边界

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GameObject.FindWithTag("player").GetComponent<Move>();//获得脚本的对象
        mG = GameObject.FindWithTag("grounds").GetComponent<moveGrounds>();
    }

    // Update is called once per frame
    void Update()
    {
        //transiation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //transform.Translate(transiation, 0, 0);
        //往左
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 v = transform.localPosition;
            v.x -= speed * Time.deltaTime;
            transform.localPosition = v;
            direction = -1;
        }
        //往右
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 v = transform.localPosition;
            v.x += speed * Time.deltaTime;
            transform.localPosition = v;
            direction = 1;
        }
        //不动
        else
        {
            direction = 0;
        }
        
        //如果另一个人物在移动
        if (moveScript.isMove)
        {
            //Debug.Log(CanCollider);
            //如果自己还没有到达边界    那么就做相对运动
            if (CanCollider)
            {
                //如果另一个人往右，那么自己往左
                if (moveScript.direction == 1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x -= speed * Time.deltaTime;
                    transform.localPosition = v;
                }
                //如果另一个人往左，那么自己往右
                if (moveScript.direction == -1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x += speed * Time.deltaTime;
                    transform.localPosition = v;
                }
            }
           
        }
        //如果对手没有碰到边界并且还是靠近自己，那么自己要往左移动
        if((!moveScript.isMove)&&(moveScript.direction==1))
        {
            Vector2 v = transform.localPosition;
            if (mG.canMove)
                v.x -= speed * Time.deltaTime*0.5f;
            transform.localPosition = v;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //如果触碰了界面的碰撞
        if (collision.collider.tag == "backGround")
        {
            //Debug.Log("碰撞！！！");
            isMove = true;
            CanCollider = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "backGround")
        {
           // Debug.Log("离开碰撞！！！");
            isMove = false;
            CanCollider = true;
        }
    }
}
