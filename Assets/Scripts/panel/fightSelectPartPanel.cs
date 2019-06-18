using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class fightSelectPartPanel : BasePanel
{
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        //checkButton = transform.Find("Button").GetComponent<Button>();
        //checkButton.onClick.AddListener(changLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changLayer()
    {
        SpriteMeshInstance order = head.GetComponent<SpriteMeshInstance>();
        order.sortingOrder = 3;
    }
}
