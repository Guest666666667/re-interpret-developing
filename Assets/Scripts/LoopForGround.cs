using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopForGround : MonoBehaviour
{
    public float speed = 0f;//速度10
    public bool moveBack = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!moveBack)
        {
            Vector2 v = transform.localPosition;

            v.x -= speed * Time.deltaTime;//向左移动

            if (v.x < -19.2f)
            {
                v.x += 2 * 19.2f;
            }
            transform.localPosition = v;
        }
       if (moveBack)
        {
            Vector2 v = transform.localPosition;

            v.x += speed * Time.deltaTime;//向右移动

            if (v.x > 19.2f)
            {
                v.x -= 2 * 19.2f;
            }
            transform.localPosition = v;
        }
        
    }

}
