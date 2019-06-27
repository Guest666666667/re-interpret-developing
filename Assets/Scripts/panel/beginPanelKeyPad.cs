using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginPanelKeyPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("shi zheli meicuo ");
            UIManager.Instance.PushPanel(UIPanelType.select);
            gameObject.SetActive(false);
        }
    }
}
