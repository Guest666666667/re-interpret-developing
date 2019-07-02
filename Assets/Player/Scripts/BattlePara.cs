using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePara : MonoBehaviour
{
    private static float moveSpeed;
    private static float jumpSpeed;
    private static float chargeSpeed;
    private static int bodyDamage;
    private static int headDamage;
    private static int guardDamage;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1; jumpSpeed = 1; bodyDamage = 10; headDamage = 20; guardDamage = 10; chargeSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public static float GetJumpSpeed()
    {
        return jumpSpeed;
    }
    public static float GetChargeSpeed()
    {
        return chargeSpeed;
    }
    public static int GetBodyDamage()
    {
        return bodyDamage;
    }
    public static int GetHeadDamage()
    {
        return headDamage;
    }
    public static int GetGuardDamage()
    {
        return guardDamage;
    }

    public static void SetMoveSpeed(float para)
    {
        float result = Mathf.Max(0.5f, para);
        moveSpeed = Mathf.Min(2.0f, result);
    }
    public static void SetJumpSpeed(float para)
    {
        float result = Mathf.Max(0.5f, para);
        jumpSpeed = Mathf.Min(2.0f, result);
    }
    public static void SetChargeSpeed(float para)
    {
        float result = Mathf.Max(0.5f, para);
        chargeSpeed = Mathf.Min(2.0f, result);
    }
    public static void SetBodyDamage(int para)
    {
        int result = Mathf.Max(5, para);
        bodyDamage = Mathf.Min(30, result);
    }
    public static void SetHeadDamage(int para)
    {
        int result = Mathf.Max(10, para);
        headDamage = Mathf.Min(50, result);
    }
    public static void SetGuardDamage(int para)
    {
        int result = Mathf.Max(5, para);
        guardDamage = Mathf.Min(20, result);
    }
}
