using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text text1;
    public Text text2;

    private int player1Health = 100;
    private int player2Health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = "Health:" + player1Health;
        text2.text = "Health:" + player2Health;
    }

    public void damage(string player, int damage)
    {
        if(player.Equals("player1"))
        {
            if(player1Health-damage<=0)
            {
                player1Health = 0;
            }
            else
            {
                player1Health -= damage;
            }
        }
        if (player.Equals("player2"))
        {
            if (player2Health - damage <= 0)
            {
                player2Health = 0;
            }
            else
            {
                player2Health -= damage;
            }
        }
    }
}
