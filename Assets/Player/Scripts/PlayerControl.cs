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
        delay
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
    private State state = State.idle;

    //整合的
    public bool isMove = false;
    private Player2Control moveScript_2;//player_2
    private moveGrounds mG;
    public int direction = 0;
    private bool CanCollider = true;//用来判断自身是否碰到便边界

    private readonly KeyCode[] KeyCodeSet = new KeyCode[6];

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
        }
        if (player.name.Equals("player2"))
        {
            KeyCodeSet[0] = KeyCode.UpArrow;
            KeyCodeSet[1] = KeyCode.LeftArrow;
            KeyCodeSet[2] = KeyCode.DownArrow;
            KeyCodeSet[3] = KeyCode.RightArrow;
            KeyCodeSet[4] = KeyCode.Keypad1;
            KeyCodeSet[5] = KeyCode.Keypad2;
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleMotion"))
        {
            if(!state.Equals(State.idle))
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
            if (Input.GetKey(KeyCodeSet[1]))
            {
                if(player.name.Equals("player1")||(!isTouch))
                {
                    Vector3 vector3 = new Vector3(-2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                    player.transform.Translate(vector3, Space.World);
                    //整合的
                    direction = -1;
                }
            }
            else if (Input.GetKey(KeyCodeSet[3]))
            {
                if (player.name.Equals("player2") || (!isTouch))
                {
                    Vector3 vector3 = new Vector3(2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                    player.transform.Translate(vector3, Space.World);
                    //整合的
                    direction = 1;
                }
            }
            else
            {
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
                animator.SetBool("isAttack", true);
                state = State.attack;
            }

            if (Input.GetKeyDown(KeyCodeSet[5]))
            {
                animator.SetBool("isGuard", true);
                state = State.guard;
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
