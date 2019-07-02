using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum State{
        idle,
        attack,
        guard,
        turn
    }

    public GameObject player;
    public GameObject other;
    private GameObject playerHealth = null;
    private Rigidbody2D rigidbody = null;
    private Animator animator = null;

    private bool isTouch = false;
    private bool isOnLand = false;
    private bool isHitted = false;
    private bool isDown = false;
    private bool isTurn = false;

    private State state = State.idle;

    //整合的
    public bool isMove = false;
    private Player2Control moveScript_2;//player_2
    private moveGrounds mG;
    public int direction = 0;
    private bool CanCollider = true;//用来判断自身是否碰到便边界

    private readonly KeyCode[] KeyCodeSet = new KeyCode[7];

    public State GetState()
    {
        return state;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("PlayerHealth");
        rigidbody = GetComponent<Rigidbody2D>();
        if(player.name.Equals("player1"))
        {
            KeyCodeSet[0] = KeyCode.W;
            KeyCodeSet[1] = KeyCode.A;
            KeyCodeSet[2] = KeyCode.S;
            KeyCodeSet[3] = KeyCode.D;
            KeyCodeSet[4] = KeyCode.J;
            KeyCodeSet[5] = KeyCode.K;
            KeyCodeSet[6] = KeyCode.U;
        }
        if (player.name.Equals("player2"))
        {
            KeyCodeSet[0] = KeyCode.UpArrow;
            KeyCodeSet[1] = KeyCode.LeftArrow;
            KeyCodeSet[2] = KeyCode.DownArrow;
            KeyCodeSet[3] = KeyCode.RightArrow;
            KeyCodeSet[4] = KeyCode.Keypad1;
            KeyCodeSet[5] = KeyCode.Keypad2;
            KeyCodeSet[6] = KeyCode.Keypad4;
        }
        animator = GetComponent<Animator>();

        //整合的(设置Tag)
        moveScript_2 = GameObject.FindWithTag("player_2").GetComponent<Player2Control>();
        //riBody = GetComponent<Rigidbody2D>();//用来处理抖动的问题
        mG = GameObject.FindWithTag("grounds").GetComponent<moveGrounds>();
    }

    // Update is called once per frame
    void Update()
    {

        bool isTuring = true;
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("front") || animator.GetCurrentAnimatorStateInfo(2).IsName("back"))
        {
            isTuring = false;
        }

        if(isTuring)
        {
            if(isTurn)
            {
                Vector3 vector3 = new Vector3(12 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                player.transform.Translate(vector3, Space.World);
            }
            else
            {
                Vector3 vector3 = new Vector3(-12 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                player.transform.Translate(vector3, Space.World);
            }
        }

        if (animator.GetAnimatorTransitionInfo(0).IsName("attack -> idle") || animator.GetAnimatorTransitionInfo(0).IsName("Guard -> idle"))
        {
            if (!state.Equals(State.idle))
            {
                state = State.idle;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            animator.SetBool("isAttack",false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            animator.SetBool("isGuard", false);
        }

        if (state.Equals(State.idle))
        {
            if (Input.GetKey(KeyCodeSet[1]) && !isTuring)
            {
                /*if(player.name.Equals("player1") || (!isTouch))
                {
                    Vector3 vector3 = new Vector3(-2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                    player.transform.Translate(vector3, Space.World);
                    animator.SetBool("isBack",true);
                    //整合的
                    direction = -1;
                }*/
                Vector3 vector3 = new Vector3(-2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                rigidbody.transform.Translate(vector3, Space.World);
                animator.SetBool("isBack", true);
                //整合的
                direction = -1;
            }
            if (Input.GetKey(KeyCodeSet[3]) && !isTuring)
            {
                /*if (player.name.Equals("player2") || (!isTouch))
                {
                    Vector3 vector3 = new Vector3(2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                    player.transform.Translate(vector3, Space.World);
                    animator.SetBool("isFront", true);
                    //整合的
                    direction = 1;
                }*/
                Vector3 vector3 = new Vector3(2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                rigidbody.transform.Translate(vector3, Space.World);
                animator.SetBool("isFront", true);
                //整合的
                direction = 1;
            }
            //同时按下A,D
            if((Input.GetKey(KeyCodeSet[3])) && (Input.GetKey(KeyCodeSet[1])))
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
            }
            //同时没有按A,D
            if ((!Input.GetKey(KeyCodeSet[3]))&&(!Input.GetKey(KeyCodeSet[1])))
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                //整合的
                direction = 0;
            }

            if (Input.GetKeyDown(KeyCodeSet[0]) && isOnLand && !isDown)
            {
                rigidbody.velocity = new Vector2(0, 400 * BattlePara.GetJumpSpeed() * Time.deltaTime);
                animator.SetTrigger("jumpUp");
            }

            if (Input.GetKeyDown(KeyCodeSet[4]))
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isAttack", true);
                state = State.attack;
            }

            if (Input.GetKeyDown(KeyCodeSet[5]))
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isGuard", true);
                state = State.guard;
            }

            //有Bug待修
            if (Input.GetKeyDown(KeyCodeSet[6]) && !isTuring)
            {
                isTurn = !isTurn;
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isTurn", isTurn);
            }
        }

        if (Input.GetKey(KeyCodeSet[2]) && isOnLand && !isDown)
        {
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCodeSet[2]) && isDown)
        {
            isDown = false;
        }

        animator.SetBool("isDown", isDown);
        animator.SetBool("isOnLand", isOnLand);

        //整合的
        if (moveScript_2.isMove)
        {
            //Debug.Log(CanCollider);
            if (CanCollider)
            {
                if (moveScript_2.direction == 1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x -= BattlePara.GetMoveSpeed() * Time.deltaTime;
                    transform.localPosition = v;
                }
                if (moveScript_2.direction == -1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x += BattlePara.GetMoveSpeed() * Time.deltaTime;
                    transform.localPosition = v;
                }
            }

        }

        if ((!moveScript_2.isMove) && (moveScript_2.direction == -1))
        {
            Vector2 v = transform.localPosition;
            if (mG.canMove)
                v.x += BattlePara.GetMoveSpeed() * Time.deltaTime * 0.5f;
            transform.localPosition = v;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Grass"))
        {
            isOnLand = true;
            isHitted = false;
        }
        if (collision.collider.Equals(other.GetComponent<Collider2D>()))
        {
            isTouch = true;
        }

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
        if(collision.collider.name.Equals("Grass"))
        {
            isOnLand = false;
        }
        if (collision.collider.Equals(other.GetComponent<Collider2D>()))
        {
            isTouch = false;
        }

        //离开碰撞
        if (collision.collider.tag == "backGround")
        {
            // Debug.Log("离开碰撞！！！");
            isMove = false;
            CanCollider = true;
        }
    }

    public void HitFly(int damage)
    {
        if(!isHitted)
        {
            player.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-5, 5), ForceMode2D.Impulse);
            playerHealth.GetComponent<PlayerHealth>().damage(player.name, damage);
            isHitted = true;
        }
    }
}
