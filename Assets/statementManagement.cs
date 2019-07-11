using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class statementManagement : MonoBehaviour
{
    private bool[] hasShow;
    public float[] showTime;
    // Start is called before the first frame update
    void Start()
    {
        hasShow = new bool[4];
        showTime = new float[4];
        showTime[0] = 1f;//显示第一段字的时间点
        showTime[1] = 50f;
        showTime[2] = 114f;
        showTime[3] = 118f;
    }

    // Update is called once per frame
    void Update()
    {
        float tmp = GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicPlayer.time;
        for (int i = 0; i < 4; i++)
        {
            if (tmp>=showTime[i]&& !hasShow[i])
            {
                hasShow[i] = true;
                Sequence t = DOTween.Sequence();
                t.Append(GetComponentsInChildren<CanvasGroup>()[i].DOFade(1f, 1f))
                    .Append(GetComponentsInChildren<CanvasGroup>()[i].DOFade(1f, 1f))
                    .Append(GetComponentsInChildren<CanvasGroup>()[i].DOFade(0f, 1f));
            }
        }
    }
}
