using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class scoreManagement : MonoBehaviour
{
    private int perfectCount = 0;
    private Transform scoreText;
    private Transform scoreValue;
    private float tmp;

    public void addPerfect()
    {
        perfectCount++;
        scoreText.GetComponent<Text>().text = "善 X" + perfectCount;
        int dig, tenDig, hunDig;
        dig = perfectCount % 10;
        tenDig = (perfectCount / 10) % 10;
        hunDig = perfectCount / 100;
        if (tenDig == 0)
        {
            //单个数字 
        }
        else if (perfectCount == 10)
        {
            //十
        }
        else if (dig == 0)
        {
            //Tendig十
        }
        else if (tenDig == 1)
        {
            //十dig
        }
        else
        {
            //tenDig十dig
        }

        //根据连击数改变颜色
        float h = (120 - 6 * perfectCount) > 0 ? 120 - 6 * perfectCount : 0;
        float s = 5 * perfectCount;
        float v = 50f;
        scoreText.GetComponent<Text>().color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);

        if (perfectCount % 3 == 0)
        {
            transform.parent.Find("health").GetComponent<healthManagement>().plus(1);
        }
        GetComponent<CanvasGroup>().DOFade(1f,0.1f);
        Sequence flash = DOTween.Sequence();
        if (perfectCount > 1)
        {
            flash.Append(transform.DOScale((1f + 0.05f * perfectCount) > 2f ? 2f : (1f + 0.05f * perfectCount), 0.01f))
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
        //根据连击数改变颜色
        float h = (120 - 6 * perfectCount) > 0 ? 120 - 6 * perfectCount : 0;
        float s = 5 * perfectCount;
        float v = 50f;
        scoreText.GetComponent<Text>().color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);

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
        scoreValue = transform.Find("value");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
