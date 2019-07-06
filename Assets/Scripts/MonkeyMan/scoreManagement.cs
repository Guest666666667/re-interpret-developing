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
        //scoreText.GetComponent<Text>().text = "善 X";
        scoreText.Find("evaluate").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI source/perfect");
        scoreText.Find("evaluate").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        int dig, tenDig, hunDig;
        dig = perfectCount % 10;
        tenDig = (perfectCount / 10) % 10;
        hunDig = perfectCount / 100;
        foreach (writeShow d in scoreValue.GetComponentsInChildren<writeShow>())
        {
            Destroy(d.gameObject);
        }
        if (tenDig == 0)
        {
            //单个数字 
            GameObject tmp = Instantiate(Resources.Load<GameObject>("UIPanel/" + dig), scoreValue, false);
            tmp.GetComponent<writeShow>().wShow(null, dig);
        }
        else if (perfectCount == 10)
        {
            //十
            GameObject tmp = Instantiate(Resources.Load<GameObject>("UIPanel/10"), scoreValue, false);
            tmp.GetComponent<writeShow>().wShow(null, 10);
        }
        else if (dig == 0)
        {
            //Tendig十
            GameObject tmp = Instantiate(Resources.Load<GameObject>("UIPanel/"+tenDig), scoreValue, false);
            Sequence t = tmp.GetComponent<writeShow>().wShow(null, tenDig);
            tmp = Instantiate(Resources.Load<GameObject>("UIPanel/10"), scoreValue, false);
            tmp.GetComponent<writeShow>().wShow(t, 10);
        }
        else if (tenDig == 1)
        {
            //十dig
            GameObject tmp = Instantiate(Resources.Load<GameObject>("UIPanel/10"), scoreValue, false);
            Sequence t = tmp.GetComponent<writeShow>().wShow(null, 10);
            tmp = Instantiate(Resources.Load<GameObject>("UIPanel/"+dig), scoreValue, false);
            tmp.GetComponent<writeShow>().wShow(t, dig);
        }
        else
        {
            //tenDig十dig
            GameObject tmp = Instantiate(Resources.Load<GameObject>("UIPanel/"+tenDig), scoreValue, false);
            Sequence t = tmp.GetComponent<writeShow>().wShow(null, tenDig);
            tmp = Instantiate(Resources.Load<GameObject>("UIPanel/10"), scoreValue, false);
            t = tmp.GetComponent<writeShow>().wShow(t, 10);
            tmp = Instantiate(Resources.Load<GameObject>("UIPanel/" + dig), scoreValue, false);
            tmp.GetComponent<writeShow>().wShow(t, dig);
        }

        //根据连击数改变颜色
        float h = (120 - 6 * perfectCount) > 0 ? 120 - 6 * perfectCount : 0;
        float s = 5 * perfectCount;
        float v = 50f;
        //scoreText.GetComponent<Text>().color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);

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
        foreach (writeShow d in scoreValue.GetComponentsInChildren<writeShow>())
        {
            Destroy(d.gameObject);
        }
        //根据连击数改变颜色
        float h = (120 - 6 * perfectCount) > 0 ? 120 - 6 * perfectCount : 0;
        float s = 5 * perfectCount;
        float v = 50f;
        //scoreText.GetComponent<Text>().color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);

        //scoreText.GetComponent<Text>().text = (isKeyDown ? "误" : "弃");
        scoreText.Find("evaluate").GetComponent<Image>().sprite = Resources.Load<Sprite>(isKeyDown ? "UI source/premature" : "UI source/miss");
        scoreText.Find("evaluate").GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
        //scoreText.GetComponent<Text>().text = "";
        scoreText.Find("evaluate").GetComponent<Image>().sprite = null;
        scoreText.Find("evaluate").GetComponent<Image>().color = new Color(1,1,1,0);
        scoreValue = transform.Find("value");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
