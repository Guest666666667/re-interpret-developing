using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMoveDown : MonoBehaviour
{
    public float speed = 0.125f;//0.5
    private moveRiver mR;
    // Start is called before the first frame update
    void Start()
    {
        mR = GameObject.FindWithTag("river").GetComponent<moveRiver>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = transform.localPosition;
        if(mR.moveDown)
        {
            v.y -= speed * Time.deltaTime;
        }
        if(v.y<-5)
        {
            return;
        }
        transform.localPosition = v;
    }
}
