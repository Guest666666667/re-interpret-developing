using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test
public class BasePanel : MonoBehaviour {

    protected UIManager uiMng;
    public UIManager UIMng
    {
        set
        {
            uiMng = value;
        }
    }

    /// 界面显示出来
    public virtual void OnEnter() { }

    /// 界面暂停(弹出了其他界面)
    public virtual void OnPause() { }

    /// 界面继续(其他界面移除，回复本来的界面交互)
    public virtual void OnResume() { }

    /// 界面不显示,退出这个界面，界面被关闭
    public virtual void OnExit() { }

}
