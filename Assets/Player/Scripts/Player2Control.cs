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
    public GameObject projectilePrefab;
    private GameObject throwArea = null;
    private BlueBar bluebar;
    private Gem[] gems = new Gem[3];
    private int gemCount = 0;

    private Animation animation;
    private AnimationClip ac1 = null;
    private AnimationClip ac2 = null;
    private AnimationClip ac3 = null;

    private bool isOnLand = false;
    private bool isHitted = false;
    private bool isDown = false;
    public bool isTurn = false;
    private bool isDash = false;
    private bool isCharge = false;
    private int throwCount = 1;
    private float poisonTimer = 0f;

    private float projectileBlueCost = 0.5f;

    private State state = State.idle;

    //整合的
    //private float transiation;
    public bool isMove = false;
    private PlayerControl moveScript;//player_1
    private moveGrounds mG;
    public int direction = 0;//移动的方向
    public bool CanCollider = true;//用来判断自身是否碰到便边界

    private readonly KeyCode[] KeyCodeSet = new KeyCode[10];

    public State GetState()
    {
        return state;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("2 Initing!!!!!!!!!!!!!!!!!!!!!!!!!!");

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
            KeyCodeSet[6] = KeyCode.U;
            KeyCodeSet[7] = KeyCode.I;
            KeyCodeSet[8] = KeyCode.O;
            KeyCodeSet[9] = KeyCode.J;
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
            KeyCodeSet[8] = KeyCode.Keypad6;
            KeyCodeSet[9] = KeyCode.Keypad3;
        }
        animator = GetComponent<Animator>();

        animation = GetComponent<Animation>();
        ac1 = GetComponent<AnimationCreator>().Create("attack"); ac2 = GetComponent<AnimationCreator>().Create("Guard"); ac3 = GetComponent<AnimationCreator>().Create("attack2");

        throwArea = GameObject.Find(name + "/Skeleton/rootBone/rightArm/rightArm2/rightHand/throwArea");
        bluebar = GameObject.FindWithTag("BlueBar_2").GetComponent<BlueBar>();
        gems[0] = GameObject.FindWithTag("Gem_2_1").GetComponent<Gem>(); gems[1] = GameObject.FindWithTag("Gem_2_2").GetComponent<Gem>(); gems[2] = GameObject.FindWithTag("Gem_2_3").GetComponent<Gem>();

        playerHealth.GetComponent<PlayerHealth>().init();
        BattlePara.Init();

        //整合的
        moveScript = GameObject.FindWithTag("player").GetComponent<PlayerControl>();//获得脚本的对象
        mG = GameObject.FindWithTag("grounds").GetComponent<moveGrounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BattlePara.scene3.Equals(BattlePara.Scene.日蚀))
        {
            if (name.Equals("player1"))
            {
                KeyCodeSet[1] = KeyCode.D;
                KeyCodeSet[3] = KeyCode.A;
            }
            if (name.Equals("player2"))
            {
                KeyCodeSet[1] = KeyCode.RightArrow;
                KeyCodeSet[3] = KeyCode.LeftArrow;
            }
        }

        if (BattlePara.scene3.Equals(BattlePara.Scene.多云))
        {
            if (!isCharge)
            {
                bluebar.chargeFull();
                gems[0].AddGem(); gems[1].AddGem(); gems[2].AddGem();
                gemCount = 3;
            }
            isCharge = true;
        }

        bool isTuring = true;
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("front") || animator.GetCurrentAnimatorStateInfo(2).IsName("back"))
        {
            isTuring = false;
            isDash = false;
        }

        if (isTuring && isDash)
        {
            if (isTurn)
            {
                Vector3 vector3 = new Vector3(-12 * BattlePara.moveSpeed2 * Time.deltaTime, 0, 0);
                //rigidbody.transform.Translate(vector3, Space.World);
                dashTranslate(vector3);
            }
            else
            {
                Vector3 vector3 = new Vector3(12 * BattlePara.moveSpeed2 * Time.deltaTime, 0, 0);
                //rigidbody.transform.Translate(vector3, Space.World);
                dashTranslate(vector3);
            }
        }

        /*if (animator.GetAnimatorTransitionInfo(0).IsName("attack -> idle") 
            || animator.GetAnimatorTransitionInfo(0).IsName("Guard -> idle")
            || animator.GetAnimatorTransitionInfo(0).IsName("attack2 -> idle")
            || animator.GetAnimatorTransitionInfo(0).IsName("throwComplete -> idle"))
        {
            if (!state.Equals(State.idle))
            {
                state = State.idle;
            }
        }*/

        if (animator.GetAnimatorTransitionInfo(0).IsName("throwComplete -> idle"))
        {
            if (state.Equals(State.throws))
            {
                state = State.idle;
            }
        }

        /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            animator.SetBool("isAttack", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
        {
            animator.SetBool("isGuard", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
        {
            animator.SetBool("isAttack2", false);
        }*/

        if (!animation.isPlaying && (state.Equals(State.attack) || state.Equals(State.guard) || state.Equals(State.attack2)))
        {
            state = State.idle;
            animator.enabled = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("throwComplete"))
        {
            animator.SetBool("isThrow", false);
        }
        if (animator.GetAnimatorTransitionInfo(0).IsName("throw -> throwComplete"))
        {
            if (throwCount == 1)
            {
                Vector2 d = new Vector2(0, 0);
                if (!isTurn)
                {
                    d.x = -1;
                }
                else
                {
                    d.x = 1;
                }
                HandThrow h = throwArea.GetComponent<HandThrow>();
                h.Throw(projectilePrefab, d);
                throwCount = 0;
            }
        }

        if (state.Equals(State.idle))
        {
            if (Input.GetKey(KeyCodeSet[1]) && !isTuring)
            {
                Vector3 vector3 = new Vector3(-2 * BattlePara.moveSpeed2 * Time.deltaTime, 0, 0);
                rigidbody.transform.Translate(vector3, Space.World);
                if(!isTurn)
                {
                    animator.SetBool("isFront", true);
                }
                else
                {
                    animator.SetBool("isBack", true);
                }
                //整合的
                direction = -1;

            }
            if (Input.GetKey(KeyCodeSet[3]) && !isTuring)
            {
                Vector3 vector3 = new Vector3(2 * BattlePara.moveSpeed2 * Time.deltaTime, 0, 0);
                rigidbody.transform.Translate(vector3, Space.World);
                if (!isTurn)
                {
                    animator.SetBool("isBack", true);
                }
                else
                {
                    animator.SetBool("isFront", true);
                }
                //整合的
                direction = 1;
            }
            //同时按下A,D
            if ((Input.GetKey(KeyCodeSet[3])) && (Input.GetKey(KeyCodeSet[1])))
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
            }
            //同时没有按A,D
            if ((!Input.GetKey(KeyCodeSet[3])) && (!Input.GetKey(KeyCodeSet[1])))
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                //整合的
                direction = 0;
            }

            if (Input.GetKeyDown(KeyCodeSet[0]) && isOnLand && !isDown && !isTuring)
            {
                rigidbody.velocity = new Vector2(0, 400 * BattlePara.jumpSpeed2 * Time.deltaTime);
                isOnLand = false;
                animator.SetTrigger("jumpUp");
            }

            if (Input.GetKeyDown(KeyCodeSet[4]) && !isTuring && bluebar.get() >= BattlePara.player2MotionCost[0])
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                bluebar.releaseSkill(BattlePara.player2MotionCost[0]);
                //animator.SetBool("isAttack", true);
                animator.enabled = false;
                animation.clip = ac1;
                animation.AddClip(ac1, ac1.name);
                animation.Play();
                state = State.attack;
            }

            if (Input.GetKeyDown(KeyCodeSet[5]) && !isTuring && bluebar.get() >= BattlePara.player2MotionCost[1])
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                bluebar.releaseSkill(BattlePara.player2MotionCost[1]);
                //animator.SetBool("isGuard", true);
                animator.enabled = false;
                animation.clip = ac2;
                animation.AddClip(ac2, ac2.name);
                animation.Play();
                state = State.guard;
            }

            if (Input.GetKeyDown(KeyCodeSet[9]) && !isTuring && bluebar.get() >= BattlePara.player2MotionCost[2])
            {
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                bluebar.releaseSkill(BattlePara.player2MotionCost[2]);
                //animator.SetBool("isAttack2", true);
                animator.enabled = false;
                animation.clip = ac3;
                animation.AddClip(ac3, ac3.name);
                animation.Play();
                state = State.attack2;
            }

            if (Input.GetKeyDown(KeyCodeSet[6]) && !isTuring && gemCount>0)
            {
                gemCount = Mathf.Max(0, gemCount - 1);
                gems[gemCount].DeleteGem();
                isDash = true;
                isTurn = !isTurn;
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isTurn", isTurn);
            }
            if (Input.GetKeyDown(KeyCodeSet[7]) && !isTuring)
            {
                isDash = false;
                isTurn = !isTurn;
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isTurn", isTurn);
            }
            if (Input.GetKeyDown(KeyCodeSet[8]) 
                && bluebar.get() >= projectileBlueCost 
                && !BattlePara.scene2.Equals(BattlePara.Scene.高树))
            {
                throwCount = 1;
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                animator.SetBool("isThrow", true);
                state = State.throws;
                bluebar.releaseSkill(projectileBlueCost);
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

        if (BattlePara.scene2.Equals(BattlePara.Scene.荆棘))
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer >= 1f)
            {
                float randomPoint = Random.value;
                if (randomPoint <= 0.4f)
                {
                    Poison(2);
                }
                poisonTimer = 0f;
            }
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
                        v.x -= BattlePara.moveSpeed2 * Time.deltaTime;
                    transform.localPosition = v;
                }
                //如果另一个人往左，那么自己往右
                if (moveScript.direction == -1)
                {
                    Vector2 v = transform.localPosition;
                    if (mG.canMove)
                        v.x += BattlePara.moveSpeed2 * Time.deltaTime;
                    transform.localPosition = v;
                }
            }

        }
        //如果对手没有碰到边界并且还是靠近自己，那么自己要往左移动
        if ((!moveScript.isMove) && (moveScript.direction == 1))
        {
            Vector2 v = transform.localPosition;
            if (mG.canMove)
                v.x -= BattlePara.moveSpeed2 * Time.deltaTime * 0.5f;
            transform.localPosition = v;
        }
        if ((!moveScript.isMove) && (moveScript.direction == -1))
        {
            Vector2 v = transform.localPosition;
            if (mG.canMove)
                v.x += BattlePara.moveSpeed2 * Time.deltaTime * 0.5f;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Grass"))
        {
            isOnLand = true;
            isHitted = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Grass"))
        {
            isOnLand = false;
        }
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
            gemCount = Mathf.Min(3, gemCount + 1);
            gems[gemCount - 1].AddGem();
            float x1 = transform.position.x, x2 = other.transform.position.x;
            if (x1 <= x2)
            {
                player.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-5, 5), ForceMode2D.Impulse);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(5, 5), ForceMode2D.Impulse);
            }

            if (BattlePara.scene2.Equals(BattlePara.Scene.桃林))
            {
                float randomPoint = Random.value;
                if (randomPoint <= 0.2f)
                {
                    damage -= 2;
                }
            }

            playerHealth.GetComponent<PlayerHealth>().damage(player.name, damage);
            isHitted = true;
        }
    }
    public void Hit(int damage)
    {
        gemCount = Mathf.Min(3, gemCount + 1);
        gems[gemCount - 1].AddGem();

        if (BattlePara.scene2.Equals(BattlePara.Scene.桃林))
        {
            float randomPoint = Random.value;
            if (randomPoint <= 0.2f)
            {
                damage -= 2;
            }
        }

        playerHealth.GetComponent<PlayerHealth>().damage(player.name, damage);

    }
    public void Poison(int damage)
    {
        playerHealth.GetComponent<PlayerHealth>().damage(player.name, damage);
    }
    private void dashTranslate(Vector3 vector3)
    {
        float x = transform.position.x + vector3.x;
        if (x >= -8.2 && x <= 8.2)
        {
            rigidbody.transform.Translate(vector3, Space.World);
        }
    }
}
