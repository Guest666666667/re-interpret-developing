using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5.0f;
   // private float transiation;
    public bool isMove = false;
    // Start is called before the first frame update
    private Move_2 moveScript_2;//player_2
    private moveGrounds mG;
    public int direction=0;
    private bool CanCollider = true;//用来判断自身是否碰到便边界

    //private Rigidbody2D riBody;
    void Start()
    {
        
        moveScript_2 = GameObject.FindWithTag("player_2").GetComponent<Move_2>();
        //riBody = GetComponent<Rigidbody2D>();//用来处理抖动的问题
        mG = GameObject.FindWithTag("grounds").GetComponent<moveGrounds>();
    }

    // Update is called once per frame
    void Update()   
    {
        //transiation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
       // transform.Translate(transiation, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 v = transform.localPosition;
            v.x -= speed * Time.deltaTime;
            transform.localPosition = v;
           // riBody.velocity = new Vector2(-speed, 0);
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector2 v = transform.localPosition;
            v.x += speed * Time.deltaTime;
            transform.localPosition = v;

            direction = 1;
        }
        else
        {
            direction = 0;
        }


        if (moveScript_2.isMove)
        {
            //Debug.Log(CanCollider);
            if (CanCollider)
            {
                if (moveScript_2.direction == 1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x -= speed * Time.deltaTime;
                    transform.localPosition = v;
                }
                if (moveScript_2.direction == -1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x += speed * Time.deltaTime;
                    transform.localPosition = v;
                }
            }

        }

        if ((!moveScript_2.isMove) && (moveScript_2.direction == -1))
        {
            Vector2 v = transform.localPosition;
            if (mG.canMove)
                v.x += speed * Time.deltaTime * 0.5f;
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
        if(collision.collider.tag == "backGround")
        {
           // Debug.Log("离开碰撞！！！");
            isMove = false;
            CanCollider = true;
        }
    }
    
    
}
