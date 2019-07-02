using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class scoreManagement : MonoBehaviour
{
    private int perfectCount = 0;
    private Transform scoreText;
    private float tmp;

    public void addPerfect()
    {
        perfectCount++;
        scoreText.GetComponent<Text>().text = "善 X" + perfectCount;
        if (perfectCount % 3 == 0)
        {
            transform.parent.Find("health").GetComponent<healthManagement>().plus(1);
        }
        GetComponent<CanvasGroup>().DOFade(1f,0.1f);
        Sequence flash = DOTween.Sequence();
        if (perfectCount > 1)
        {
            flash.Append(transform.DOScale(2f, 0.1f))
            .Append(transform.DOScale(1f, 0.5f));
        }
        else
        {
            transform.position = new Vector3(tmp-GetComponent<RectTransform>().rect.size.x, transform.position.y, transform.position.z);
            flash.Append(transform.DOMoveX(tmp, 0.1f));
        }
    }

    public void fault(bool isKeyDown)
    {
        perfectCount = 0;
        scoreText.GetComponent<Text>().text = (isKeyDown ? "误" : "弃");
        transform.position = new Vector3(tmp - GetComponent<RectTransform>().rect.size.x, transform.position.y, transform.position.z);
        Sequence flash = DOTween.Sequence();
        flash.Append(transform.DOMoveX(tmp, 0.1f))
        .Append(GetComponent<CanvasGroup>().DOFade(0f, 0.5f));
    }

    // Start is called before the first frame update
    void Start()
    {
        tmp = transform.position.x;
        scoreText = transform.Find("scoreHint");
        scoreText.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
