using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone : MonoBehaviour
{
    public float speed = 8f;//速度
    public GameObject[] images;
    private changeSpeed cS;
    public bool move = false;
    public static int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        cS = GameObject.FindWithTag("changeSpeed").GetComponent<changeSpeed>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = transform.localPosition;

        v.x -= speed * Time.deltaTime;//向左移动

        if (v.x < -19.2f)
        {
            v.x += 2 * 19.2f;
            //销毁
            foreach (Transform trans in transform)
            {
                Destroy(trans.gameObject);

            }
            //创建
            if(move)
            {
                if(index<7)
                {
                    Instantiate(images[index], transform);
                    index++;
                }
                
            }
        }
        transform.localPosition = v;
    }
}
