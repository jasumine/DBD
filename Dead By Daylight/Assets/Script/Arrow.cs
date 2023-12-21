using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public float speed;

    private bool isRotate;
    private Transform arrowTrans;

    public LineRenderer lineRenderer;


    void Start()
    {
        arrowTrans = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawRadiusLine();
        ArrowRotate();
    }


    void DrawRadiusLine()
    {
        // 중앙에서 시작하는 선을 추가
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, new Vector3(0, 1f, 0f));
    }

    void ArrowRotate()
    {
        //Quaternion rotation = arrowRect.transform.rotation;
        //rotation = Quaternion.Euler(0, 0, 360 * Time.deltaTime * speed);

        //arrowRect.transform.rotation = rotation;
        arrowTrans.Rotate(0, 0, -360 * Time.deltaTime * speed);
            }
}

