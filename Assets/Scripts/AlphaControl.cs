using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControl : MonoBehaviour
{
    private float tempTime = 0;
    private int isOut;//0：in，1：out，2：还未调用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOut == 0)
        {
            //gameObject.GetComponent<MeshRenderer>().material.a
        }
        else if (isOut == 1)
        {

        }
    }
    public void fadeOut()
    {

    }
    public void fadeIn()
    {

    }
}
