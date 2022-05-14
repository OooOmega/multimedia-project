using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMainCamera : MonoBehaviour
{
    public MyButton leftBtn;
    public MyButton rightBtn;

    public Vector3 center;
    public Camera mainCamera;

    public float leftSpeed;
    public float rightSpeed;

    private void Start()
    {
        mainCamera = Camera.main;
        leftBtn.OnLongPress.AddListener(LeftAction);
        rightBtn.OnLongPress.AddListener(RightAction);
    }

    public void LeftAction()
    {
        mainCamera.transform.RotateAround(center, Vector3.up, Time.deltaTime * leftSpeed);
    }

    public void RightAction()
    {
        mainCamera.transform.RotateAround(center, Vector3.down, Time.deltaTime * rightSpeed);
    }
}
