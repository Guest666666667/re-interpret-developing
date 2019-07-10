using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUp : MonoBehaviour
{
    public float speed = 0f;
   // private bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = transform.localPosition;
        if(v.y<7)
        {
            v.y += speed * Time.unscaledDeltaTime;
        }
      
        transform.localPosition = v;
    }
}
