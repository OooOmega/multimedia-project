using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public ShowModelWindow window;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //从摄像机发出射线的点
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                string name = hit.collider.gameObject.name;
                if (name.Contains("("))
                {
                    name = name.Remove(name.Length-4, 4);
                }
                //Debug.LogFormat("鼠标点击了屏幕!点击对象：{0}", name);
                window.gameObject.SetActive(true);
                window.ShowModel(name);
            }
            else
            {
                //Debug.Log("没点中！");
            }
        }
    }
}
