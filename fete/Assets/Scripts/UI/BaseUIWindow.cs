using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//当前ui的显示状态
public enum UIViewState
{
    Nonvisible, //不可见的
    Visible,    //可见的
}

public class BaseUIWindow : MonoBehaviour
{

    public UIViewState ViewState = UIViewState.Visible;
    //用于点击关闭的背景
    private GameObject bgObj;

    //初始化
    public virtual void initial()
    {
        OnHide();
    }
    //被显示时
    public virtual void OnShow()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (ViewState != UIViewState.Visible)
        {
            //将z坐标归0
            Vector3 pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;

            ViewState = UIViewState.Visible;
            //InitBG();
        }

        UpdateView();
    }

    public virtual void OnShowWithBG()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (ViewState != UIViewState.Visible)
        {
            //将z坐标归0
            Vector3 pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;

            ViewState = UIViewState.Visible;
            //InitBG();
        }

        UpdateView();
        InitBG();
    }

    //更新
    public virtual void UpdateView()
    {
    }

    //被隐藏
    public virtual void OnHide()
    {
        //if (ViewState == UIViewState.Visible)
        //{
        //从相机的视域体内推出
        Vector3 pos = transform.localPosition;
        pos.z = -99999;
        transform.localPosition = pos;

        ViewState = UIViewState.Nonvisible;
        this.gameObject.SetActive(false);
        //}
        //去掉背景
        //Destroy(bgObj);
    }

    public virtual void OnHideWithBG()
    {
        Vector3 pos = transform.localPosition;
        pos.z = -99999;
        transform.localPosition = pos;

        ViewState = UIViewState.Nonvisible;
        this.gameObject.SetActive(false);
        Destroy(bgObj);
    }

    //初始化背景(点击背景自动关闭界面)
    //未设置状态转移机，也就是倒回上一步
    protected void InitBG()
    {
        Transform bgTran = transform.Find("BG");
        //假定每次右键都会清除BG
        //if (bgTran == null)
        //{
        bgObj = new GameObject("BG", typeof(RectTransform));
        bgTran = bgObj.transform;
        bgTran.SetParent(transform);
        bgTran.SetAsFirstSibling();
        RectTransform rt = bgObj.GetComponent<RectTransform>();
        //rt.Normalize();
        //}
        //查看是否有图片
        Image img = bgTran.GetComponent<Image>();
        if (img == null)
        {
            img = bgTran.gameObject.AddComponent<Image>();
            img.color = new Color(0, 0, 0, 0f);
            CanvasRenderer cr = bgTran.GetComponent<CanvasRenderer>();
            cr.cullTransparentMesh = true;
        }
        img.raycastTarget = true;
        //是否有事件点击
        EventTrigger eventTrigger = bgTran.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = bgTran.gameObject.AddComponent<EventTrigger>();
        }
        //监听点击背景的事件
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        //此处新创建的BG没有关闭，可能出现bug，先做个记号
        entry.callback.AddListener(e => clossePanel());
        eventTrigger.triggers.Add(entry);

    }

    protected void canntClickBG()
    {
        bgObj = new GameObject("BG", typeof(RectTransform));
        Transform bgTran = bgObj.transform;
        bgTran.SetParent(transform);
        bgTran.SetAsFirstSibling();
        RectTransform rt = bgObj.GetComponent<RectTransform>();
    }

    protected void closeCanntClickBG()
    {
        Destroy(bgObj);
    }

    protected virtual void clossePanel()
    {
        OnHideWithBG();
        Destroy(bgObj);
    }

    public virtual void setArgument(params object[] args)
    {
        //TODO
    }
}

