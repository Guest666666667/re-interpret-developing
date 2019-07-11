using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    public float speed = 5f;
    public float totalTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime = GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicPlayer.time;
        Vector2 v = transform.localPosition;
        //if((totalTime>0f)&&(totalTime<6f))
        //{
        //    if(v.x<9.5f)
        //    {
        //        v.x += speed * Time.deltaTime;//向左移动
        //    }
           
        //}
        if((totalTime > 116f) && (totalTime < 126f))
        {
            v.x += 10 * Time.deltaTime*10;//向左移动
        }
      
        transform.localPosition = v;
    }
}
