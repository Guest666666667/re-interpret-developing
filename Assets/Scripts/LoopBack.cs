using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBack : MonoBehaviour {
    public float speed = 10f;//速度
    public GameObject[] images;
  
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 v = transform.localPosition;
      
        v.x -= speed * Time.deltaTime;//向左移动

        if (v.x < -38.4f)
        {
            v.x = 1 * 19.2f;
            //销毁
            foreach (Transform trans in transform)
            {
                Destroy(trans.gameObject);
            }
            //创建新地形
            Instantiate(images[Random.Range(0, images.Length)], transform);
        }
        transform.localPosition = v;
    }
    
}
