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

    private bool isOnLand = false;
    private bool isHitted = false;
    private bool isDown = false;
    private bool isTurn = false;
    private bool isDash = false;

    private State state = State.idle;

    //整合的
    public bool isMove = false;
    private Player2Control moveScript_2;//player_2
    private moveGrounds mG;
    public int direction = 0;
    private bool CanCollider = true;//用来判断自身是否碰到便边界

    private readonly KeyCode[] KeyCodeSet = new KeyCode[8];

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
            KeyCodeSet[7] = KeyCode.I;
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
            KeyCodeSet[7] = KeyCode.Keypad5;
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
            isDash = false;
        }

        if(isTuring && isDash)
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
                Vector3 vector3 = new Vector3(-2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                rigidbody.transform.Translate(vector3, Space.World);
                if(!isTurn)
                {
                    animator.SetBool("isBack", true);
                }
                else
                {
                    animator.SetBool("isFront", true);
                }
                //整合的
                direction = -1;
            }
            if (Input.GetKey(KeyCodeSet[3]) && !isTuring)
            {
                Vector3 vector3 = new Vector3(2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                rigidbody.transform.Translate(vector3, Space.World);
                if (!isTurn)
                {
                    animator.SetBool("isFront", true);
                }
                else
                {
                    animator.SetBool("isBack", true);
                }
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
                isDash = true;
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isTurn", isTurn);
            }
            if (Input.GetKeyDown(KeyCodeSet[7]) && !isTuring)
            {
                isTurn = !isTurn;
                isDash = false;
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
        if ((!moveScript_2.isMove) && (moveScript_2.direction == 1))
        {
            Vector2 v = transform.localPosition;
            if (mG.canMove)
                v.x -= BattlePara.GetMoveSpeed() * Time.deltaTime * 0.5f;
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
