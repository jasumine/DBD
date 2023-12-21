using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCircle : MonoBehaviour
{
    public float targetAngle;
    public float succeessPercent;

    //3. ������ up* r�� �ϸ� ���� ������ �����.���⼭ �������� a�� ���ϸ� up* a* r�� ���� ���������� ����°��̴�.a�� ���̰�
    //4. ���⼭ �뼺�� ������ up * a* r  * b(�뼺������) �� ���ָ� �ȴ�.
    //5.  4���� 5���� Ȯ���ϱ� ���ؼ��� lineWire�� �Ἥ Ȯ���Ѵ�.

    public float radius = 50f;
    public int segments = 100;
    public LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer �ʱ�ȭ
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false; // ���� ��ǥ ���

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
