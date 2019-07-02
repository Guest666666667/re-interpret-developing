using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private float player1Energy;
    private float player2Energy;
    public float chargeSpeed = 1f;
    private int player1HitPoint;
    private int player2HitPoint;

    // Start is called before the first frame update
    void Start()
    {
        player1Energy = 0;
        player2Energy = 0;
        player1HitPoint = 0;
        player2HitPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Charge(Time.deltaTime*chargeSpeed);
    }

    private void Charge(float para)
    {
        player1Energy += Mathf.Min(100, player1Energy + para);
        player2Energy += Mathf.Min(100, player2Energy + para);
    }
    public void SetPlayer1HitPoint(int para)
    {
        int result = Mathf.Max(0, player1HitPoint + para);
        player1HitPoint = Mathf.Min(3, result);
    }
    public void SetPlayer2HitPoint(int para)
    {
        int result = Mathf.Max(0, player2HitPoint + para);
        player2HitPoint = Mathf.Min(3, result);
    }

    public int GetPlayer1HitPoint()
    {
        return player1HitPoint;
    }
    public int GetPlayer2HitPoint()
    {
        return player2HitPoint;
    }
}
