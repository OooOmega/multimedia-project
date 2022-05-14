using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragModel : MonoBehaviour
{
    /// <summary>
    /// 当前帧所在位置
    /// </summary>
    private Vector2 currentPos;
    /// <summary>
    /// 上一帧所在位置
    /// </summary>
    private Vector2 lastPos;
    /// <summary>
    /// 旋转速度
    /// </summary>
    [Range(5, 50)] public float rotateSpeed = 15;

    private float cd = 1.0f;

    private void Update()
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > 960)
                return;
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > 960)
                return;
            currentPos = Input.mousePosition;
            gameObject.transform.Rotate(Vector3.up, (lastPos.x - currentPos.x) * Time.deltaTime * rotateSpeed);
            gameObject.transform.Rotate(Vector3.right, (lastPos.y - currentPos.y) * Time.deltaTime * rotateSpeed);
            lastPos = Input.mousePosition;
        }
    }
}
