using System.Collections;
using System.Collections.Generic;
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
    public GameObject SkillCheckUI;
    public GameObject CircleUI;
    public RectTransform ArrowUI;


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

        if (Input.GetKeyDown("Space"))
        {
            Debug.Log("space");
            isArrowRotate = false;
        }

        if (isArrowRotate)
        {
            if (ArrowUI.rotation.z >= -360)
            {
                ArrowUI.Rotate(0f, 0f, -4f * Time.deltaTime * 100);
            }
        }
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
        repairValue += 1f * Time.deltaTime;

        float randomValue = Random.Range(0f, 200f);
        if (randomValue <= 5f && isSkillCheck == false)
        {
            // 확률로 스킬체크 발생
            StartCoroutine(SkillCheck());
        }
    }

    IEnumerator SkillCheck()
    {
        Debug.Log("발전기 스킬체크 발생");
        isSkillCheck = true;
        // 오디오가 들리고
        yield return new WaitForSeconds(0.3f);

        // 스킬체크 화면 출력
        SkillCheckUI.SetActive(true);
        float zC = Random.Range(-116, -316);
        CircleUI.transform.rotation = Quaternion.Euler(0, 0, zC);

        // 화살표 회전
        ArrowUI.transform.Rotate(0, 0, 0);
        isArrowRotate = true;



        yield return new WaitForSeconds(4f);
        SkillCheckUI.SetActive(false);
        isSkillCheck = false;
        isArrowRotate = false;
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
