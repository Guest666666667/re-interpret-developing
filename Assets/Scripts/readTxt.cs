using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class readTxt : MonoBehaviour {
    public TextAsset m_TextAsset;
    public string text;//txt文本的内容
    public bool singal = false;//触发信号
    public List<string> sentences = new List<string>();//存放时间点的数组
    public GameObject[] boneList;//骨骼列表
    private double totalTime = 0.00;
    private List<double> musicPoint = new List<double>();//存放时间点的数组
    private bool[] musicJudge;//音乐判定点
    private bool[] actionJudge;//动作判定点
    private int[] boneSeq;//生成的骨骼编号序列

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
        actionJudge = new bool[sentences.Count];
        boneSeq = new int[sentences.Count];

        for (int i = 0; i < sentences.Count; i++)
        {
            //musicPoint[i] = double.Parse(sentences[i]);
            musicPoint.Add(double.Parse(sentences[i]));
            musicJudge[i] = false;
            actionJudge[i] = false;
            boneSeq[i] = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0.001f)
        {
            //totalTime += Time.unscaledDeltaTime;
            totalTime = GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicPlayer.time;
        }
        

        for (int i = 0; i < musicPoint.Count; i++)
        {
            if (totalTime >= (musicPoint[i] - 1.2f) && musicJudge[i] == false)//
            {
                /*Debug.Log(musicPoint[i]);
                singal = true;
                Debug.Log(singal);
                singal = false;*/
                int tmp = Mathf.FloorToInt(Random.value * boneList.Length);
                /*while(tmp < 1 || tmp == 7 || tmp == 8 || tmp == 3 || tmp == 6)//暂时设定只能选中运动幅度较大的四肢
                {
                    tmp = Mathf.FloorToInt(Random.value * boneList.Length);
                }*/
                bool[] temp = new bool[4];
                //if (GameObject.Find("afterImage").GetComponent<timeScaleManagement>().getLevel() > 0)
                //{
                    foreach (Text t in GameObject.Find("Canvas/runTimeUI").GetComponentsInChildren<Text>())//确定现在屏幕上存在着的按钮
                    {
                        if (t.text == "Q") temp[0] = true;
                        if (t.text == "E") temp[1] = true;
                        if (t.text == "A") temp[2] = true;
                        if (t.text == "D") temp[3] = true;
                    }
                    if (temp[0] && temp[1] && temp[2] && temp[3])
                    {
                        break;
                    }
                    else
                    {
                        while ((temp[0] && (tmp == 0 || tmp == 7 || (tmp >= 4 && tmp <= 6))) || (temp[1] && (tmp == 0 || tmp == 7 || (tmp >= 1 && tmp <= 3))) || ((temp[2] && tmp >= 8 && tmp <= 9)) || (temp[3] && (tmp == 8 || tmp == 10)))
                        {
                            tmp = Mathf.FloorToInt(Random.value * boneList.Length);
                        }
                    }
                //}
                boneList[tmp].GetComponent<frontBone>().begin(tmp);//随机骨骼判定完美动作
                musicJudge[i] = true;
                boneSeq[i] = tmp;
            }
            if (totalTime >= (musicPoint[i] - 0.5f) && actionJudge[i] == false)
            {
                boneList[boneSeq[i]].GetComponent<frontBone>().callChange();
                actionJudge[i] = true;
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
