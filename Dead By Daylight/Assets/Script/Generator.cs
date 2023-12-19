using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public float repairValue;
    public float damageValue;

    public bool isComplete = false;
    public bool isDamaged = false;

    private bool isSkillCheck = false;
    private bool isArrowRotate = false;
    private bool isSkillSuccess = true;
    public GameObject SkillCheckUI;
    public RectTransform CircleUI;
    public RectTransform ArrowUI;

    float bigSucessMin, bigSucessMax, successMax;

    private void Start()
    {
        repairValue = 0;
        damageValue = 0;
    }

    private void Update()
    {
        if(isDamaged)
        {
            ReduceRepairValue();
        }

        if (isArrowRotate)
        {
            // ȭ�� ȸ��
            if (ArrowUI.rotation.z >= -180)
            {
                ArrowUI.Rotate(0f, 0f, -4f * Time.deltaTime * 100);
            }

            // space�� �̿��� ȭ�� ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space");
                isArrowRotate = false;
                SkillSuccessRange();
            }
        }

        if (Input.GetMouseButtonUp(0) && isSkillCheck == true)
        {
            // ��ųüũ�� ���µ�, ���콺�� �ôٸ� ���з� ó���ϱ�
            Debug.Log("���콺 ������!");

            StartCoroutine(SkillFail());

            isArrowRotate = false;
            SkillCheckUI.SetActive(false);
            isSkillCheck = false;

        }

    }

    private void SkillSuccessRange()
    {
        Quaternion circleRotation = CircleUI.rotation;
        Vector3 circleEulerRotation = circleRotation.eulerAngles;

        float target = circleEulerRotation.z;

        Quaternion arrowRotation = ArrowUI.rotation;
        Vector3 arrowEulerRotation = arrowRotation.eulerAngles;

        float arrow = arrowEulerRotation.z;

        if (target > 0) // ��� �� ��
        {
            bigSucessMin = target + 29;
            bigSucessMax = target + 19;
            successMax = target - 24;
        }

        // arrow ���� üũ
        if(bigSucessMax <= arrow && arrow <= bigSucessMin)
        {
            // �뼺��
            Debug.Log("�뼺��");
            isSkillSuccess = true;
            repairValue += 5;
        }
        else if (successMax <= arrow && arrow <= bigSucessMax)
        {
            // ����
            Debug.Log("����");
            isSkillSuccess = true;
        }
        else
        {
            // ����
            Debug.Log("����");
            StartCoroutine(SkillFail());
        }

    }

    IEnumerator SkillFail()
    {
        // ���θ����� �˸��� ������ �߰��ϱ�


        // �������� �ʵ��� false �� ���൵ ���� -> �ٽ� ����
        Debug.Log("������ �ջ�");
        isSkillSuccess = false;
        
        // 0���ϰ� �� ��� 0���� �ʱ�ȭ
        repairValue -= 5;
        if(repairValue <=0) repairValue = 0;

        yield return new WaitForSeconds(2f);
        isSkillSuccess = true;
    }


    private void OnCollisionStay(Collision collision)
    {
        if (isComplete == false)
        {
            if (collision.gameObject.tag == "Survivor")
            {
                // �������� ��� ������ ����
                if (Input.GetMouseButton(0))
                {
                    isDamaged = false;
                    Rapairing();
                    if (repairValue >= 100)
                    {
                        CompleteGenerator();
                    }
                }
            }

            if (collision.gameObject.tag == "Killer")
            {
                // ���θ��� ��� ������ �ջ�
                if (Input.GetKey(KeyCode.Space) && !isDamaged)
                {
                    damageValue += Time.deltaTime;
                    if (damageValue >= 2)
                    {
                        isDamaged = true;
                    }
                }
                else
                {
                    damageValue = 0;
                }
            }
        }
    }

 
    private void Rapairing()
    {
        // ������ ��ų üũ ���н� ������ ���� ���� bool �߰�
        if (isSkillSuccess == true)
        {
            repairValue += 0.5f * Time.deltaTime;
            float randomValue = Random.Range(0f, 100f);
            if (randomValue <= 5f && isSkillCheck == false)
            {
                StopCoroutine(SkillCheck());
                // Ȯ���� ��ųüũ �߻�
                StartCoroutine(SkillCheck());
            }
        }
    }

    IEnumerator SkillCheck()
    {
        Debug.Log("������ ��ųüũ �߻�");
        // ������� �鸮��
        yield return new WaitForSeconds(0.3f);
        isSkillCheck = true;



        // ��ųüũ ȭ�� ���
        SkillCheckUI.SetActive(true);
        float zC = Random.Range(-116, -316);
        CircleUI.rotation = Quaternion.Euler(0, 0, zC);

        // ȭ��ǥ ȸ��
        ArrowUI.rotation = Quaternion.Euler(0, 0, 0);
        isArrowRotate = true;

        // ȭ��ǥ ȸ�� ����
        yield return new WaitForSeconds(0.9f);
        // ȸ���� ���⶧����(0.9�ʰ� ���� �� ����) space�� �ȴ����ٸ� fail���� ���ش�.
        if(isArrowRotate)
        {
            isArrowRotate = false;

            StartCoroutine(SkillFail());
        }

        // ��ųüũ �̺�Ʈ ����
        yield return new WaitForSeconds(1f);
        SkillCheckUI.SetActive(false);
        isSkillCheck = false;
    }


    private void ReduceRepairValue()
    {
        repairValue -= 0.1f * Time.deltaTime;
    }


    private void CompleteGenerator()
    {
        isComplete = true;
        // ������ ������ �ϼ��Ǹ� �ؾ� �ϴ� �۾�
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.white;
    }
}
