using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class beginPanelKeyPad : MonoBehaviour
{
    private Image showImage;
    // Start is called before the first frame update
    void Start()
    {
        showImage = transform.parent.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Vector3 temp = showImage.transform.localPosition;
            //temp.x = 10;
            //showImage.transform.localPosition = temp;
            showImage.transform.DOMoveY(775 , 1);
            Debug.Log("shi zheli meicuo ");
            //TODO add delay
            UIManager.Instance.PushPanel(UIPanelType.select);
            gameObject.SetActive(false);
        }
    }
}
