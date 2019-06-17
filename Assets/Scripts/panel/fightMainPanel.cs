using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class fightMainPanel : BasePanel
{
    public GameObject head;
    private Button checkButton;
    // Start is called before the first frame update
    void Start()
    {
        checkButton = transform.Find("Button").GetComponent<Button>();
        checkButton.onClick.AddListener(changLayer);
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
