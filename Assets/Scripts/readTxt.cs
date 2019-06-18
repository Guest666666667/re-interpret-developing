using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readTxt : MonoBehaviour {
    public TextAsset m_TextAsset;
    public string text;//txt文本的内容
    public bool singal = false;//触发信号
    public List<string> sentences = new List<string>();//存放时间点的数组
    public GameObject[] boneList;//骨骼列表
    private double totalTime = 110.00;//110s音乐的时间
    private List<double> musicPoint = new List<double>();//存放时间点的数组
    private bool[] musicJudge;//音乐判定点

    // Use this for initialization
    void Start()
    {

        text = Resources.Load<TextAsset>("data").text;
        // Debug.Log(text);

        int num = 0;

        for (int i = 0; i < text.Length; i++)
        {
            string c = text.Substring(i, 1);     //提取字符串中第i个字符，
            if (c == "\n")                    //如果遇到换行符，跳过，
            {
                num += 1;
                continue;
            }
            if (sentences.Count <= num)         //创建新句子
            {
                sentences.Add(c);
            }
            else
            {
                sentences[sentences.Count - 1] += c;
            }
        }

        musicJudge = new bool[sentences.Count];

        for (int i = 0; i < sentences.Count; i++)
        {
            //musicPoint[i] = double.Parse(sentences[i]);
            musicPoint.Add(double.Parse(sentences[i]));
            musicJudge[i] = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.unscaledDeltaTime;

        for (int i = 0; i < musicPoint.Count; i++)
        {
            if (totalTime <= (musicPoint[i] + 5f/3f) && musicJudge[i] == false)
            {
                /*Debug.Log(musicPoint[i]);
                singal = true;
                Debug.Log(singal);
                singal = false;*/
                boneList[Mathf.FloorToInt(Random.value * boneList.Length)].GetComponent<frontBone>().begin();//随机骨骼判定完美动作
                musicJudge[i] = true;
            }

        }
        if (AudioManager.Instance != null)
        {
            //Debug.Log("willianFenghe");
            //  AudioManager.Instance.PlayMusic("test");
            AudioManager.Instance.changeMusicVolume(1f);//改变音量
        }
    }
}
