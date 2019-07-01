using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.scene.name == "mainScene")
        {
            UIManager.Instance.PushPanel(UIPanelType.begin);
        }
        else if (gameObject.scene.name == "fightGame")
        {
            UIManager.Instance.PushPanel(UIPanelType.fightSelectPart);
        }
        else if (gameObject.scene.name == "storyGame")
        {
            UIManager.Instance.PushPanel(UIPanelType.storyMain);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
