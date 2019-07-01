using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private float player1Energy;
    private float player2Energy;
    public float chargeSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player1Energy = 0;
        player2Energy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        charge(Time.deltaTime*chargeSpeed);
    }

    private void charge(float para)
    {
        player1Energy += Mathf.Min(100, player1Energy + para);
        player2Energy += Mathf.Min(100, player2Energy + para);
    }
}
