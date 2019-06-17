using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using LitJson;

/*
 UI框架使用方式：
 新建页面后将页面做成预制体，预制体摆放在UIPlanel文件夹下
 更改UIPanelType和UIPanelType.json，使管理器能够找到panel
 每个页面都有自己的脚本进行管理，各种按钮跳转函数都写在该脚本下
 页面跳转通过栈push、pop实现，切换场景前要清空栈、清空页面字典。
*/
public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// 
    /// 单例模式的核心
    /// 1，定义一个静态的对象 在外界访问 在内部构造
    /// 2，构造方法私有化

    private static UIManager _instance;//场景一的静态实例
    private static UIManager _instance2;//场景二的静态实例

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    public static UIManager Instance2
    {
        get
        {
            if (_instance2 == null)
            {
                _instance2 = new UIManager();
            }
            return _instance2;
        }
    }

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private static Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径
    private static Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件
    private static Stack<BasePanel> panelStack;//栈,要和UImanager一样static，important！！！
    private beginPanel beginpanel;

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        Debug.Log("getPanel in!");
        BasePanel panel = GetPanel(panelType);
        Debug.Log("push right!");
        panel.OnEnter();
        panelStack.Push(panel);//入栈
    }

    /// 出栈 ，把页面从界面上移除
    public void PopPanel()
    {
        //Debug.Log("pop in");
        if (panelStack == null)
        { panelStack = new Stack<BasePanel>(); Debug.Log("it is non!!!"); }//判断栈顶是否为空，为空创建

        //如果栈顶元素数量为0，则直接返回结束方法
        if (panelStack.Count <= 0) { Debug.Log("count = 0"); return; }

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        Debug.Log("exit already");
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();//得到当前的栈顶元素
        topPanel2.OnResume();//执行当前栈顶UI面板的继续方法
    }

    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDict.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            instPanel.GetComponent<BasePanel>().UIMng = this;
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }

    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");//得到我们的UPanelType.json文件

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);//将json数据转换成类中的数据,并返回一个list
        //Debug.Log(ta.text);
        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            //if(info==null) Debug.Log("info nothing!!!!!!!!!!!!");
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);//保存路径
        }
    }
    public int getStackCount()
    {
        return panelStack.Count;
    }
    public void clearDict()
    {
        panelDict.Clear();
    }
    /// <summary>
    /// just for test
    /// </summary>
    //public void Test()
    //{
    //    string path;
    //    panelPathDict.TryGetValue(UIPanelType.begin, out path);
    //    Debug.Log(path);
    //}
}
