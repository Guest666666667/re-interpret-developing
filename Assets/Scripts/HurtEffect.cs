using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HurtEffect : MonoBehaviour
{
    public GameObject hurtEffect;
    public GameObject hurtEffect_2;
    Material tmp ;
     Material tmp_2 ;
    private float time_1=0f;
    private float time_2 = 0f;
    public bool begin_1 = false;
    public bool begin_2 = false;

    // Start is called before the first frame update
    void Awake()
    {
       tmp = new Material(Resources.Load<Material>("original"));
       tmp_2 = new Material(Resources.Load<Material>("hurt"));
        hurtEffect.GetComponent<Image>().material = tmp;
        hurtEffect_2.GetComponent<Image>().material = tmp;

        // transform.Find("head").GetComponent<Image>().material = tmp;
    }
    void Start()
    {
       
       //hurtEffect.GetComponent<Image>().material = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if(begin_1)
        {
            time_1 += Time.deltaTime;
            if(time_1>=0.5f)
            {
                begin_1 = false;
                time_1 = 0.0f;
            }
        }
        if (begin_2)
        {
            time_2 += Time.deltaTime;
            if (time_2 >= 0.5f)
            {
                begin_2 = false;
                time_2 = 0.0f;
            }
        }
        Reset();

    }
    void Reset()
    {
      
        if (!begin_1)
        {
            hurtEffect.GetComponent<Image>().material = tmp;
            
        }
       
        if (!begin_2)
        {
            hurtEffect_2.GetComponent<Image>().material = tmp;

        }
    }

    public void player1Hurt()
    {
        hurtEffect.GetComponent<Image>().material = tmp_2;
        begin_1 = true;
    }
    public void player2Hurt()
    {
        hurtEffect_2.GetComponent<Image>().material = tmp_2;
        begin_2 = true;
    }
}
