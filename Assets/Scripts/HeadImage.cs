using Anima2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadImage : MonoBehaviour
{
    public Image image1;
    public Image image2;
    private GameObject p1Head;
    private GameObject p2Head;

    // Start is called before the first frame update
    void Start()
    {
        p1Head = GameObject.Find("player1/model/头");
        p2Head = GameObject.Find("player2/model/头");
    }

    // Update is called once per frame
    void Update()
    {
        image1.sprite = p1Head.GetComponent<SpriteMeshInstance>().spriteMesh.sprite;
        image2.sprite = p2Head.GetComponent<SpriteMeshInstance>().spriteMesh.sprite;
    }
}
