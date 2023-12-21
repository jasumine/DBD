using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCircle : MonoBehaviour
{
    public float targetAngle;
    public float succeessPercent;

    //3. 범위는 up* r을 하면 시작 지점이 생긴다.여기서 성공각도 a를 곱하면 up* a* r은 성공 성공범위가 생기는것이다.a는 사이각
    //4. 여기서 대성공 각도는 up * a* r  * b(대성공각도) 를 해주면 된다.
    //5.  4번과 5번을 확인하기 위해서는 lineWire를 써서 확인한다.

    public float radius = 50f;
    public int segments = 100;
    public LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer 초기화
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false; // 로컬 좌표 사용

        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            theta += deltaTheta;
        }
    }
}
