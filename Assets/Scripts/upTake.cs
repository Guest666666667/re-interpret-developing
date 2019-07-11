using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class upTake : MonoBehaviour
{
    private float totalTime;
    private bool hasUp;

    // Start is called before the first frame update
    void Start()
    {
        hasUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime = GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicPlayer.time;
        if (!hasUp && totalTime >= 50f)
        {
            hasUp = true;
            Sequence s = DOTween.Sequence();
            s.Append(transform.DOMoveY(-3.13f, 1.5f))
                .Append(DOTween.To(() => GetComponent<SpriteRenderer>().color.a, x => GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, x), 1f, 1f))
                .Append(DOTween.To(() => GetComponent<SpriteRenderer>().color.a, x => GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, x), 0f, 1f))
                .Append(DOTween.To(() => GetComponent<SpriteRenderer>().color.a, x => GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, x), 1f, 1f))
                .Append(DOTween.To(() => GetComponent<SpriteRenderer>().color.a, x => GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, x), 0f, 1f));
        }
    }
}
