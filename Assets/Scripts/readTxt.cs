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
        if (Time.timeScale > 0.001f)
        {
            totalTime -= Time.unscaledDeltaTime;
        }
        

        for (int i = 0; i < musicPoint.Count; i++)
        {
            if (totalTime <= (musicPoint[i] + 0.5f/0.3f) && musicJudge[i] == false)//0.5代表慢镜头放慢前计划持续时间，0.3代表放慢倍率
            {
                /*Debug.Log(musicPoint[i]);
                singal = true;
                Debug.Log(singal);
                singal = false;*/
                int tmp = Mathf.FloorToInt(Random.value * boneList.Length);
                while(tmp < 1 || tmp == 7 || tmp == 8 || tmp == 3 || tmp == 6)//暂时设定只能选中运动幅度较大的四肢
                {
                    tmp = Mathf.FloorToInt(Random.value * boneList.Length);
                }
                boneList[tmp].GetComponent<frontBone>().begin(tmp);//随机骨骼判定完美动作
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
