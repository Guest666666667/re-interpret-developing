using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent : MonoBehaviour
{
    private Vector3 originPosition;
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originPosition + new Vector3(0f, Mathf.Sin(Time.time*3) * 0.2f, 0f);
    }
}
