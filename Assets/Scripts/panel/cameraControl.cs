using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 p = transform.localPosition;
        //p.x += 0.5f * Time.deltaTime;
        //transform.localPosition = p;
        int width = Screen.width;
        float scale = width / 1920;
        Event m_event = Event.current;
        Vector2 orien = m_event.mousePosition - new Vector2(1000, 500);
        
    }
}
