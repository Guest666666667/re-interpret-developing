using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    public help h1;
    public help_2 h2;
    public help h3;
    public help h4;
    public help_2 h5;
    public GameObject help_1;
    public GameObject help_2;
    public GameObject help_3;
    public GameObject help_4;
    public GameObject help_5;
    // Start is called before the first frame update
    void Start()
    {
        //h1 = GameObject.FindWithTag("help_1").GetComponent<help>();
        //h2 = GameObject.FindWithTag("help_2").GetComponent<help_2>();
        //h3 = GameObject.FindWithTag("help_3").GetComponent<help>();
        //h4 = GameObject.FindWithTag("help_4").GetComponent<help>();
        //h5 = GameObject.FindWithTag("help_5").GetComponent<help_2>();
        h1 = transform.Find("help_1").GetComponent<help>();
        h2 = transform.Find("help_2").GetComponent<help_2>();
        h3 = transform.Find("help_3").GetComponent<help>();
        h4 = transform.Find("help_4").GetComponent<help>();
        h5 = transform.Find("help_5").GetComponent<help_2>();
        help_1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //h1退出
        if(!h1.isActive)
        {
            h1.shouldActive = false;
            h2.shouldActive = true;
            help_1.SetActive(false);
            help_2.SetActive(true);
           
        }
        if (!h2.isActive)
        {
            h2.shouldActive = false;
            h3.shouldActive = true;
            help_2.SetActive(false);
            help_3.SetActive(true);
        }
        if (!h3.isActive)
        {
            h3.shouldActive = false;
            h4.shouldActive = true;
            help_3.SetActive(false);
            help_4.SetActive(true);
        }
        if (!h4.isActive)
        {
            h4.shouldActive = false;
            h5.shouldActive = true;
            help_4.SetActive(false);
            help_5.SetActive(true);
        }
        if (!h5.isActive)
        {
            h5.shouldActive = false;
            help_5.SetActive(false);
            //h2.shouldActive = true;

        }
    }
}
