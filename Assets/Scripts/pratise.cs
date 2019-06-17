using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pratise : MonoBehaviour {
    public float speed = 10f;//速度
    // Use this for initialization
    public GameObject[] images;
    public static int i=3;//数组的下标（这个不用改）
    public bool direction = false;//控制方向
    public static bool end = false;//判断背景图片是否加载播放完了
    public static bool moveFont = true;//判断背景是否可以继续往前移动
    public static bool moveBack = true; //判断背景是否可以继续往后移动
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 v = transform.localPosition;
        if(!direction)
        {
            if ((!end)||((v.x>=-38.4f)&&(moveFont)))//38.4=19.2*2
            {
                v.x -= speed * Time.deltaTime;//如果没有结束，则继续移动
                if((v.x<=-38.4)&&(i==3))//i是总图数-3
                {
                    moveFont = false;
                }
            }
            if (i > 2)//i=总图数-3
            {               
                end = true;
            }

            //判断是否出了屏幕
            if ((v.x < -38.4f)&&(!end))
            {
                v.x = 1 * 19.2f;
                //销毁
                foreach (Transform trans in transform)
                {
                    Destroy(trans.gameObject);
                }
                //创建新地形
                // Instantiate(images[Random.Range(0, images.Length)], transform);
                if (!end)
                {
                    Instantiate(images[i], transform);
                    i++;
                }
            }
        }
        else
        {
            if ((!end) || ((v.x <= 38.4f) && (moveBack)))
            { 
                v.x += speed * Time.deltaTime;//如果没有结束，则继续移动
                if ((v.x >= 38.4f) && (i == 3))
                 {             
                    moveBack = false;
                  }
            }
            if ((i - 4) < 0)
            {
                end = true;

            }
            //判断是否出了屏幕
            if ((v.x > 38.4f)&&(!end))
            {
                v.x = -1 * 19.2f;
                //销毁
                foreach (Transform trans in transform)
                {
                    Destroy(trans.gameObject);
                }
                //创建新地形
                Instantiate(images[i - 1], transform);
                i--;
               
            }
        }
       
        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("您按下了D键");
            direction = true;
            end = false;
            moveFont = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("您按下了A键");
            direction = false;
            end = false;
            // k = 0;
            moveBack = true;
        }
        if ((!end) || (v.x > -38.4f))
        {
            transform.localPosition = v;
         
        }
       
	}
}
