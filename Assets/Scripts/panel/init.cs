using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.fightSelectPart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
