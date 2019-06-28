using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static PlayerControl;

public class Player2Control : MonoBehaviour
{
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
        if (player.name.Equals("player1"))
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
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("delay"))
        {
            state = State.delay;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            if (state.Equals(State.delay))
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isGuard", false);
            }
            state = State.idle;
        }

        if (state.Equals(State.idle))
        {
            if (Input.GetKey(KeyCodeSet[1]))
            {
                if (player.name.Equals("player1") || (!isTouch))
                {
                    Vector3 vector3 = new Vector3(-2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                    player.transform.Translate(vector3, Space.World);
                }
            }

            if (Input.GetKey(KeyCodeSet[3]))
            {
                if (player.name.Equals("player2") || (!isTouch))
                {
                    Vector3 vector3 = new Vector3(2 * BattlePara.GetMoveSpeed() * Time.deltaTime, 0, 0);
                    player.transform.Translate(vector3, Space.World);
                }
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Grass"))
        {
            isOnLand = false;
        }
        if (collision.collider.Equals(other.GetComponent<Collider2D>()))
        {
            isTouch = false;
        }
    }

    public void HitFly(int damage)
    {
        if(!isHitted)
        {
            player.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(5, 5), ForceMode2D.Impulse);
            playerHealth.GetComponent<PlayerHealth>().damage(player.name, damage);
            isHitted = true;
        }
    }
}
