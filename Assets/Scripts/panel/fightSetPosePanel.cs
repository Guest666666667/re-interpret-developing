using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class fightSetPosePanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private const int paraCount = 6;

    private Text player1Button;
    private Text player2Button;
    private Text[] player1ParaText = new Text[paraCount];
    private Text[] player2ParaText = new Text[paraCount];

    public Color selectedColor;
    public Color nonSelectedColor;

    private GameObject[] player1 = new GameObject[paraCount];
    private GameObject[] player2 = new GameObject[paraCount];

    private int[][] player1Para = new int[2][];
    private int[][] player2Para = new int[2][];

    private int selectIndex1 = 1000000 * (paraCount + 1);
    private int selectIndex2 = 1000000 * (paraCount + 1);
    private string[] button1 = { "Attack:J", "Guard:K" };
    private int buttonIndex1 = 1000000 * 2;
    private string[] button2 = { "Attack:Num1", "Guard:Num2" };
    private int buttonIndex2 = 1000000 * 2;
    private bool player1OK = false;
    private bool player2OK = false;

    private GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();

        head = GameObject.Find("/player2/model/头");

        player1Para[0] = new int[paraCount];
        player1Para[1] = new int[paraCount];
        player2Para[0] = new int[paraCount];
        player2Para[1] = new int[paraCount];
        player1Para[0][0] = -152; player1Para[0][1] = -157; player1Para[0][2] = 96; player1Para[0][3] = -11; player1Para[0][4] = -270; player1Para[0][5] = -1;
        player1Para[1][0] = -152; player1Para[1][1] = -157; player1Para[1][2] = 89; player1Para[1][3] = -88; player1Para[1][4] = -270; player1Para[1][5] = -1;
        player2Para[0][0] = -152; player2Para[0][1] = -157; player2Para[0][2] = 96; player2Para[0][3] = -11; player2Para[0][4] = -270; player2Para[0][5] = -1;
        player2Para[1][0] = -152; player2Para[1][1] = -157; player2Para[1][2] = 89; player2Para[1][3] = -88; player2Para[1][4] = -270; player2Para[1][5] = -1;

        player1[0] = GameObject.Find("/player1/Skeleton/rootBone/leftArm"); player1[1] = GameObject.Find("/player1/Skeleton/rootBone/leftArm/leftArm2");
        player1[2] = GameObject.Find("/player1/Skeleton/rootBone/rightArm"); player1[3] = GameObject.Find("/player1/Skeleton/rootBone/rightArm/rightArm2");
        player1[4] = GameObject.Find("/player1/Skeleton/rootBone"); player1[5] = GameObject.Find("/player1/Skeleton/rootBone/head");

        player2[0] = GameObject.Find("/player2/Skeleton/rootBone/leftArm"); player2[1] = GameObject.Find("/player2/Skeleton/rootBone/leftArm/leftArm2");
        player2[2] = GameObject.Find("/player2/Skeleton/rootBone/rightArm"); player2[3] = GameObject.Find("/player2/Skeleton/rootBone/rightArm/rightArm2");
        player2[4] = GameObject.Find("/player2/Skeleton/rootBone"); player2[5] = GameObject.Find("/player2/Skeleton/rootBone/head");

        player1Button = transform.GetChild(0).Find("player1Button").GetComponent<Text>();
        player2Button = transform.GetChild(1).Find("player2Button").GetComponent<Text>();
        for (int i = 0; i < paraCount; i++)
        {
            player1ParaText[i] = transform.GetChild(0).Find("player1").GetChild(i).Find("angle").GetComponent<Text>();
            player2ParaText[i] = transform.GetChild(1).Find("player2").GetChild(i).Find("angle").GetComponent<Text>();
            //Debug.Log("设置进来了");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            player1OK = true;
            transform.Find("P1confirmMessage").gameObject.SetActive(true);
            transform.Find("P1").GetComponent<selectPose>().enabled = false;
            transform.Find("P1").GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            player2OK = true;
            transform.Find("P2confirmMessage").gameObject.SetActive(true);
            transform.Find("P2").GetComponent<selectPose>().enabled = false;
            transform.Find("P2").GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if (!player1OK)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                selectIndex1--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                selectIndex1++;
            }

            if (selectIndex1 % (paraCount + 1) == 0)
            {
                player1Button.color = selectedColor;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    buttonIndex1--;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    buttonIndex1++;
                }
                player1Button.text = button1[buttonIndex1 % 2];
            }
            else
            {
                player1Button.color = nonSelectedColor;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    int angle = player1Para[buttonIndex1 % 2][selectIndex1 % (paraCount + 1) - 1];
                    angle = Mathf.Max(-360, angle - 10);
                    player1Para[buttonIndex1 % 2][selectIndex1 % (paraCount + 1) - 1] = angle;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    int angle = player1Para[buttonIndex1 % 2][selectIndex1 % (paraCount + 1) - 1];
                    angle = Mathf.Min(360, angle + 10);
                    player1Para[buttonIndex1 % 2][selectIndex1 % (paraCount + 1) - 1] = angle;
                }
            }

        }

        if (!player2OK)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectIndex2--;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectIndex2++;
            }

            if (selectIndex2 % (paraCount + 1) == 0)
            {
                player2Button.color = selectedColor;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    buttonIndex2--;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    buttonIndex2++;
                }
                player2Button.text = button2[buttonIndex2 % 2];
            }
            else
            {
                player2Button.color = nonSelectedColor;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    int angle = player2Para[buttonIndex2 % 2][selectIndex2 % (paraCount + 1) - 1];
                    angle = Mathf.Max(-360, angle - 10);
                    player2Para[buttonIndex2 % 2][selectIndex2 % (paraCount + 1) - 1] = angle;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    int angle = player2Para[buttonIndex2 % 2][selectIndex2 % (paraCount + 1) - 1];
                    angle = Mathf.Min(360, angle + 10);
                    player2Para[buttonIndex2 % 2][selectIndex2 % (paraCount + 1) - 1] = angle;
                }
            }
        }

        for (int i = 0; i < paraCount; i++)
        {
            player1ParaText[i].text = player1Para[buttonIndex1 % 2][i] + "";
            player2ParaText[i].text = player2Para[buttonIndex2 % 2][i] + "";
            if (i == selectIndex1 % (paraCount + 1) - 1)
            {
                player1ParaText[i].color = selectedColor;
            }
            else
            {
                player1ParaText[i].color = nonSelectedColor;
            }
            if (i == selectIndex2 % (paraCount + 1) - 1)
            {
                player2ParaText[i].color = selectedColor;
            }
            else
            {
                player2ParaText[i].color = nonSelectedColor;
            }
        }

        SetPlayer(buttonIndex1 % 2, buttonIndex2 % 2);

        if (player1OK && player2OK)
        {
            SaveMotion();
            player1OK = false;
            player2OK = false;
            Invoke("OnMakeSure", 0.6f);
        }
    }
    public override void OnEnter()//不用enter，一开始就在   
    {
        base.OnEnter();
    }
    public override void OnPause()
    {
        gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板不再和鼠标交互
        base.OnPause();
    }
    public override void OnResume()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnMakeSure()
    {
        //TODO change the position of player
        uiMng.PushPanel(UIPanelType.fightSelectBg);
    }
    private void SetPlayer(int index1, int index2)
    {
        for (int i = 0; i < paraCount; i++)
        {
            player1[i].transform.localRotation = Quaternion.Euler(0, 0, player1Para[index1][i]);
            player2[i].transform.localRotation = Quaternion.Euler(0, 0, player2Para[index2][i]);
        }
    }

    private void SaveMotion()
    {
        JsonData jsonTemp1 = new JsonData();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < paraCount; j++)
            {
                MotionInfo motionInfo = new MotionInfo();
                motionInfo.motionIndex = i;
                motionInfo.boneIndex = j;
                motionInfo.value = player1Para[i][j];
                jsonTemp1.MotionList.Add(motionInfo);
            }
        }
        string result1 = JsonUtility.ToJson(jsonTemp1, true);
        StreamWriter sw1 = new StreamWriter(JsonPath(0), false);
        sw1.Write(result1);
        Debug.Log(result1);
        sw1.Flush();
        sw1.Close();

        JsonData jsonTemp2 = new JsonData();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < paraCount; j++)
            {
                MotionInfo motionInfo = new MotionInfo();
                motionInfo.motionIndex = i;
                motionInfo.boneIndex = j;
                motionInfo.value = player2Para[i][j];
                jsonTemp2.MotionList.Add(motionInfo);
            }
        }
        string result2 = JsonUtility.ToJson(jsonTemp2, true);
        StreamWriter sw2 = new StreamWriter(JsonPath(1), false);
        sw2.Write(result2);
        Debug.Log(result2);
        sw2.Flush();
        sw2.Close();
    }

    private string JsonPath(int index)
    {
        return Application.persistentDataPath + "/player" + (index + 1) + ".json";
    }
}
