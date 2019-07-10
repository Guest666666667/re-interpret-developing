using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackForLocustTree : MonoBehaviour
{
    public float speed = 10f;//速度
    public GameObject[] images;
    private double totalTime = 0.0;//110s音乐的时间
    private changeSpeed cS;
    // Start is called before the first frame update
    void Start()
    {
        cS = GameObject.FindWithTag("changeSpeed").GetComponent<changeSpeed>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (cS.LocustSparse_1)
            {

                Instantiate(images[Random.Range(0, images.Length - 8)], transform);
                // Instantiate(images[8], transform);
            }//稀疏
            if (cS.LocustThick)
            {

                Instantiate(images[Random.Range(9, images.Length)], transform);
                //Instantiate(images[5], transform);
            }

            if (cS.LocustSparse_2)
            {

                Instantiate(images[Random.Range(0, images.Length - 8)], transform);
                //Instantiate(images[5], transform);
            }


        }
        transform.localPosition = v;
    }
}
