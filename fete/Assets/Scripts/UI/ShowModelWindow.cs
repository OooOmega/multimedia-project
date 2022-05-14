using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowModelWindow : MonoBehaviour
{
    public GameObject cameraControl;
    public Camera mainCamera;
    public GameObject target;
    public Transform modelNode;

    public Text text;
    public Button returnBtn;

    private Vector3 cameraPos;

    private void Start() 
    {
        returnBtn.onClick.AddListener(CloseModel);
    }

    public void ShowModel(string name)
    {
        //调整摄像机
        cameraPos = mainCamera.gameObject.transform.position;
        mainCamera.gameObject.transform.position = new Vector3(10000, 0, -10);

        //关闭移动UI
        cameraControl.gameObject.SetActive(false);

        //加载对应的prefab
        string path = "Prefab/" + name;
        GameObject obj = Resources.Load<GameObject>(path);
        target = GameObject.Instantiate(obj);
        Debug.Log(target.transform.localEulerAngles);
        target.transform.eulerAngles = Vector3.zero;
        Debug.Log(target.transform.localEulerAngles);

        target.transform.parent = modelNode;
        Vector3 pos = target.transform.localPosition;
        pos.x += 9998;
        target.transform.localPosition = pos;
        Debug.Log(target.transform.localEulerAngles);
        //添加控制组件
        target.AddComponent<DragModel>();

        //读取文字并显示
        text.text = GetResult.getTexts(name);
        //播放音频
        SoundManager.instance.PlayerSoundEffect(name);
    }

    public void CloseModel()
    {
        Destroy(target);
        //调整摄像机
        mainCamera.gameObject.transform.position = cameraPos;
        //打开移动UI
        cameraControl.gameObject.SetActive(true);
        //关闭当前UI
        gameObject.SetActive(false);
        //关闭音频
        SoundManager.instance.CloseEffectSound();
    }
}