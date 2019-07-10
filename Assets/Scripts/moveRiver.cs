using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRiver : MonoBehaviour
{
    public float speed = 10.0f;//10
   
    public bool startMove = false;
    public bool moveDown = false;
    public bool stop = false;
    private changeSpeed cS;
  //  public bool moveAgain = false;
    // Start is called before the first frame update
    void Start()
    {
        cS = GameObject.FindWithTag("changeSpeed").GetComponent<changeSpeed>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 v = transform.localPosition;
        //40s后开始动
        if((startMove))
        {
            
            v.x -= speed * Time.deltaTime;//开始向左移动
        }
        if(v.x<=-57.6)
        {
            
            //moveDown = true;//水位下降
            if(cS.isStop)
            {
                stop = true;
            }
           
        }
     
        transform.localPosition = v;

    }
}
