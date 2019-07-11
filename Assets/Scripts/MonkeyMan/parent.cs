using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent : MonoBehaviour
{
    private Vector3 originPosition;
    private bool swing;
    private float wav = 0f;
    private GameObject both;
    // Start is called before the first frame update
    void Start()
    {
        wav = 0f;
        originPosition = transform.position;
        both = GameObject.Find("parent");
    }

    // Update is called once per frame
    void Update()
    {
        if (swing)
        {
            wav += Time.deltaTime;
            wav = both.GetComponent<parent>().getWav();
            transform.position = originPosition + new Vector3(0f, Mathf.Sin(wav * 3) * 0.2f, 0f);
        }
        else
        {
            wav = 0f;
            transform.position = originPosition;
        }
    }

    public void enableSwing(bool isOn)
    {
        swing = isOn;
    }

    public float getWav()
    {
        return wav;
    }
}
