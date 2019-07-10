using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheat : MonoBehaviour
{
    public float speed = 0f;//速度3
    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = transform.localPosition;
        v.x -= speed * Time.deltaTime;//向左移动
        if (v.x < -38.4f)
        {
            //如果超出了边界就让它停下来
            return;
        }
        transform.localPosition = v;
    }
}
