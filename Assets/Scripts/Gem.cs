using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    public Image content;
    public float fillAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Handle();
    }
    private void Handle()
    {
        content.fillAmount = fillAmount;
    }
    public void AddGem()
    {
        fillAmount = 1.0f;
    }
    public void DeleteGem()
    {
        fillAmount = 0.0f;
    }
}
