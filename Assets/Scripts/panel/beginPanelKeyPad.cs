using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class beginPanelKeyPad : MonoBehaviour
{
    private Image showImage;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        showImage = transform.parent.Find("Image").GetComponent<Image>();
        anim = transform.parent.Find("Image").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Vector3 temp = showImage.transform.localPosition;
            //temp.x = 10;
            //showImage.transform.localPosition = temp;
            //showImage.transform.DOMoveY(775 , 1);
            //showImage.transform.DOLocalMoveY(775, 1);
            anim.SetBool("nextIn", true);
            Debug.Log("shi zheli meicuo ");
            //TODO add delay
            Invoke("turnPage", 1);
            gameObject.SetActive(false);
        }
    }
    public void turnPage()
    {
        UIManager.Instance.PushPanel(UIPanelType.select);
    }
}
