using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help_2 : MonoBehaviour
{
    public GameObject component_1;
    public GameObject component_2;
    public GameObject component_3;
    public GameObject component_4;
    public GameObject component_5;
    private int number = 4;
    public bool isActive = true;
    public bool shouldActive = false;
    // Start is called before the first frame update
    void Start()
    {
        component_1.SetActive(true);
        component_2.SetActive(false);
        component_3.SetActive(false);
        component_4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldActive)
        {
            component_5.SetActive(true);
        }
        OnClick();
        SetActive();
       
    }
    void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //component_1.SetActive(false);
            number--;
            AudioManager.Instance.PlaySound("Music/Sound/UI/turn");
        }
    }
    void SetActive()
    {
        if (number == 4)
        {
            component_1.SetActive(true);
            component_2.SetActive(false);
            component_3.SetActive(false);
            component_4.SetActive(false);
        }
        if (number == 3)
        {
            component_1.SetActive(false);
            component_2.SetActive(true);
        }
        if (number == 2)
        {
            component_1.SetActive(false);
            component_2.SetActive(false);
            component_3.SetActive(true);
        }
        if (number == 1)
        {
            component_1.SetActive(false);
            component_2.SetActive(false);
            component_3.SetActive(false);
            component_4.SetActive(true);
        }
        if (number == 0)
        {    
            component_4.SetActive(false);
            //component_5.SetActive(false);
            isActive = false;
        }
    }
}
