using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class selectPart : MonoBehaviour
{
    private Button highLight;

    // Start is called before the first frame update
    void Start()
    {
        highLight = transform.GetComponent<Button>();
        highLight.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
