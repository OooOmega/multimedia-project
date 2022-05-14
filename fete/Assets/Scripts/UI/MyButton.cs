﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
/// <summary>
/// 我的自定义Button，继承 Button
/// </summary>
public class MyButton : Button
{
   // 构造函数
    protected MyButton()
    {
        my_onDoubleClick = new ButtonClickedEvent();
        my_onLongPress = new ButtonClickedEvent();
    }
 
    // 长按
    public ButtonClickedEvent my_onLongPress;
    public ButtonClickedEvent OnLongPress
    {
        get { return my_onLongPress; }
        set { my_onLongPress = value; }
    }
 
    // 双击
    public ButtonClickedEvent my_onDoubleClick;
    public ButtonClickedEvent OnDoubleClick
    {
        get { return my_onDoubleClick; }
        set { my_onDoubleClick = value; }
    }
 
    // 长按需要的变量参数
    private bool my_isStartPress = false;
    private float my_curPointDownTime = 0f;
    private float my_longPressTime = 0.6f;
    private bool my_longPressTrigger = false;
  
  private bool isStart = false;
 
    void Update()
    {
        if (isStart)
            my_onLongPress.Invoke();
    }
 
    #region 长按
 
    // /// <summary>
    // /// 处理长按
    // /// </summary>
    // void CheckIsLongPress() {
    //     if (my_isStartPress && !my_longPressTrigger)
    //     {
    //         if (Time.time > my_curPointDownTime + my_longPressTime)
    //         {
    //             my_longPressTrigger = true;
    //             my_isStartPress = false;
    //             if (my_onLongPress != null)
    //             {
    //                 my_onLongPress.Invoke();
    //             }
    //         }
    //     }
    // }
 
    public override void OnPointerDown(PointerEventData eventData)
    {
        // 按下刷新當前時間
        base.OnPointerDown(eventData);
        // my_curPointDownTime = Time.time;
        // my_isStartPress = true;
        // my_longPressTrigger = false;
        isStart = true;
    }
 
    public override void OnPointerUp(PointerEventData eventData)
    {
        // 指針擡起，結束開始長按
        base.OnPointerUp(eventData);
        // my_isStartPress = false;
        isStart = false;
    }
 
    public override void OnPointerExit(PointerEventData eventData)
    {
        // 指針移出，結束開始長按，計時長按標志
        base.OnPointerExit(eventData);
        // my_isStartPress = false;
        // my_onLongPress.Invoke();
       
    }
 
    #endregion
 
    #region 双击（单击）
 
    public override void OnPointerClick(PointerEventData eventData)
    {
        //(避免已經點擊進入長按后，擡起的情況)
        if (!my_longPressTrigger)
        {
            // 正常單擊 
            if (eventData.clickCount == 2 )
            {
              
                if (my_onDoubleClick != null)
                {
                    my_onDoubleClick.Invoke();
                }
                
            }// 雙擊
            else if (eventData.clickCount == 1)
            {
                onClick.Invoke();
            }
        }
    }
    #endregion
 
 
}