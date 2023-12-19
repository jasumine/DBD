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
            // 화살 회전
            if (ArrowUI.rotation.z >= -180)
            {
                ArrowUI.Rotate(0f, 0f, -4f * Time.deltaTime * 100);
            }

            // space를 이용한 화살 멈춤
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space");
                isArrowRotate = false;
                SkillSuccessRange();
            }
        }

        if (Input.GetMouseButtonUp(0) && isSkillCheck == true)
        {
            // 스킬체크가 떴는데, 마우스를 뗐다면 실패로 처리하기
            Debug.Log("마우스 떼지마!");

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

        if (target > 0) // 양수 일 때
        {
            bigSucessMin = target + 29;
            bigSucessMax = target + 19;
            successMax = target - 24;
        }

        // arrow 범위 체크
        if(bigSucessMax <= arrow && arrow <= bigSucessMin)
        {
            // 대성공
            Debug.Log("대성공");
            isSkillSuccess = true;
            repairValue += 5;
        }
        else if (successMax <= arrow && arrow <= bigSucessMax)
        {
            // 성공
            Debug.Log("성공");
            isSkillSuccess = true;
        }
        else
        {
            // 실패
            Debug.Log("실패");
            StartCoroutine(SkillFail());
        }

    }

    IEnumerator SkillFail()
    {
        // 살인마에게 알림이 가도록 추가하기


        // 수리되지 않도록 false 후 진행도 감소 -> 다시 수리
        Debug.Log("발전기 손상");
        isSkillSuccess = false;
        
        // 0이하가 될 경우 0으로 초기화
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
                // 생존자인 경우 발전기 수리
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
                // 살인마인 경우 발전기 손상
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
        // 발전기 스킬 체크 실패시 진행을 막기 위해 bool 추가
        if (isSkillSuccess == true)
        {
            repairValue += 0.5f * Time.deltaTime;
            float randomValue = Random.Range(0f, 100f);
            if (randomValue <= 5f && isSkillCheck == false)
            {
                StopCoroutine(SkillCheck());
                // 확률로 스킬체크 발생
                StartCoroutine(SkillCheck());
            }
        }
    }

    IEnumerator SkillCheck()
    {
        Debug.Log("발전기 스킬체크 발생");
        // 오디오가 들리고
        yield return new WaitForSeconds(0.3f);
        isSkillCheck = true;



        // 스킬체크 화면 출력
        SkillCheckUI.SetActive(true);
        float zC = Random.Range(-116, -316);
        CircleUI.rotation = Quaternion.Euler(0, 0, zC);

        // 화살표 회전
        ArrowUI.rotation = Quaternion.Euler(0, 0, 0);
        isArrowRotate = true;

        // 화살표 회전 멈춤
        yield return new WaitForSeconds(0.9f);
        // 회전이 멈출때까지(0.9초가 지날 때 까지) space를 안눌렀다면 fail실행 해준다.
        if(isArrowRotate)
        {
            isArrowRotate = false;

            StartCoroutine(SkillFail());
        }

        // 스킬체크 이벤트 종료
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
        // 발전기 수리가 완성되면 해야 하는 작업
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.white;
    }
}
