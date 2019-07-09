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
    private int tmp;//随机骨骼序号

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
            musicPoint.Add(double.Parse(sentences[i]));//鼓点微调
            musicJudge[i] = false;
            actionJudge[i] = false;
            boneSeq[i] = 0;
        }

        genRan();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0.001f)
        {
            //totalTime += Time.unscaledDeltaTime;
            totalTime = GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicPlayer.time;
        }

        
        //}
        for (int i = 0; i < musicPoint.Count; i++)
        {

            if (musicJudge[i] == false && ((!(tmp == 0 || tmp == 7 || tmp == 8) && totalTime >= (musicPoint[i] - 1.2f)) || ((tmp == 0 || tmp == 7 || tmp == 8) && totalTime >= (musicPoint[i] - 1.7f))))
            {
                
                Debug.Log(tmp);
                /*Debug.Log(musicPoint[i]);
                singal = true;
                Debug.Log(singal);
                singal = false;*/

                /*while(tmp < 1 || tmp == 7 || tmp == 8 || tmp == 3 || tmp == 6)//暂时设定只能选中运动幅度较大的四肢
                {
                    tmp = Mathf.FloorToInt(Random.value * boneList.Length);
                }*/

                int seq = 0, si = i;
                while (si - 1 >= 0 && ((musicPoint[si] - musicPoint[--si]) < 1.2f)) //根据1.2s内连续出现的按钮数量设定顺序号
                {
                    seq = (seq + 1) % 4;
                }
                seq++;
                if (i + 1 < musicPoint.Count && (seq == 1 && musicPoint[i + 1] - musicPoint[i] > 1.2f))
                {
                    seq = 0;
                }
                boneList[tmp].GetComponent<frontBone>().begin(tmp,seq);//随机骨骼判定完美动作
                //Debug.Log(seq);
                musicJudge[i] = true;
                boneSeq[i] = tmp;
                genRan();
            }
            if ((((boneSeq[i] == 0 || boneSeq[i] == 7 || boneSeq[i] == 8) && totalTime >= (musicPoint[i] - 0.5f / 0.5f)) || (!(boneSeq[i] == 0 || boneSeq[i] == 7 || boneSeq[i] == 8) && totalTime >= (musicPoint[i] - 1f))) && actionJudge[i] == false) //双键慢动作时间预算
            {
                boneList[boneSeq[i]].GetComponent<frontBone>().callChange((boneSeq[i] == 0 || boneSeq[i] == 7 || boneSeq[i] == 8) ? true : false);
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

    public void genRan()//生成随机骨骼序号
    {
        tmp = Mathf.FloorToInt(Random.value * boneList.Length);
        bool[] temp = new bool[4];
        //if (GameObject.Find("afterImage").GetComponent<timeScaleManagement>().getLevel() > 0)
        //{
        foreach (hint t in GameObject.Find("Canvas/runTimeUI").GetComponentsInChildren<hint>())//确定现在屏幕上存在着的按钮
        {
            if (t.Key == "q" && t.transform.Find("keyHint/evaluate").GetComponent<Image>().sprite == null) temp[0] = true;
            if (t.Key == "e" && t.transform.Find("keyHint/evaluate").GetComponent<Image>().sprite == null) temp[1] = true;
            if (t.Key == "a" && t.transform.Find("keyHint/evaluate").GetComponent<Image>().sprite == null) temp[2] = true;
            if (t.Key == "d" && t.transform.Find("keyHint/evaluate").GetComponent<Image>().sprite == null) temp[3] = true;
        }
        if (temp[0] && temp[1] && temp[2] && temp[3])
        {
            //break;
        }
        else
        {
            while ((temp[0] && (tmp == 0 || tmp == 7 || (tmp >= 4 && tmp <= 6))) || (temp[1] && (tmp == 0 || tmp == 7 || (tmp >= 1 && tmp <= 3))) || ((temp[2] && tmp >= 8 && tmp <= 9)) || (temp[3] && (tmp == 8 || tmp == 10)) || (totalTime > 43f && tmp == 0 || tmp == 7 || tmp == 8)) 
            {
                tmp = Mathf.FloorToInt(Random.value * boneList.Length);
            }
        }
    }
}
