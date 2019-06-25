using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testPanel : MonoBehaviour
{
    private Button b1;
    private Button b2;
    // Start is called before the first frame update
    void Start()
    {
        b1 = transform.Find("Button").GetComponent<Button>();
        b2 = transform.Find("Button2").GetComponent<Button>();
        b1.Select();
        b1.onClick.AddListener(OnClick1);
        b2.onClick.AddListener(OnClick2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            
        }
    }
    public void OnClick1()
    {
        Debug.Log("Button 1 pressed");
    }
    public void OnClick2()
    {
        Debug.Log("Button 2 pressed");
    }
}
