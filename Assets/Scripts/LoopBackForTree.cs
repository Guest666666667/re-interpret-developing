 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackForTree : MonoBehaviour {
    public float speed = 10f;//速度
    public GameObject[] images;
    private float totalTime = 0.0f;//总时间125
    private changeSpeed cS;
    void Start () {
        cS = GameObject.FindWithTag("changeSpeed").GetComponent<changeSpeed>();
    }
	
	// Update is called once per frame
	void Update () {
        
        totalTime = totalTime + Time.deltaTime;
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
            //创建新地形(可以根据时间来改变初始化的地形)
            //if ((totalTime <= 26)&& (totalTime >= 15))
            if(cS.sparse_1)
            {
                
                Instantiate(images[Random.Range(8, images.Length)], transform);
               // Instantiate(images[8], transform);
            }//稀疏
           
            //if ((totalTime <= 42) && (totalTime > 26))
            if(cS.thick)
            {
               
                Instantiate(images[Random.Range(0, images.Length-3)], transform);
                //Instantiate(images[5], transform);
            }//茂密

            //if ((totalTime <= 50) && (totalTime > 42))
            if(cS.sparse_2)
            {
               
                Instantiate(images[Random.Range(7, images.Length)], transform);
                //Instantiate(images[5], transform);
            }


        }
        transform.localPosition = v;
    }
    
}
