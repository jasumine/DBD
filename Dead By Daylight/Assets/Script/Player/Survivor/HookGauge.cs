using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGauge : MonoBehaviour
{
    public int hookChance = 0;
    public float hookCount = 100;

    public bool isHang = false;

    public float escapeValue = 0;
    private float escapeValueMax = 3f;

    public bool isTryEscape = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHang ==true)
        {
            ReduceValue();
        }

    }
    

    public void SetIsHang()
    {
        isHang = true;

        hookCount++;
        switch (hookChance)
        {
            case 1:
                // 처음 걸림
                hookCount = 100;
                break;
            case 2:
                // 두번째 걸림
                // 스킬체크 발생
                hookCount = 50;
                break;
            case 3:
                // 세번째 걸림
                // 죽음
                hookCount = 0;
                break;

        }
    }



    private void ReduceValue()
    {
        hookCount -=  Time.deltaTime;

        if (50 < hookCount && hookCount <= 100)
        {
            // 견자단을 할 수 있음.
            TryEscape();
        }
        else if(0 < hookCount && hookCount <=50)
        {
            hookChance = 2;
        }
        else if(hookCount<= 0)
        {
            // 죽음
            hookChance = 3;
        }
    }

    private void TryEscape()
    {
        if(Input.GetMouseButton(0) && isTryEscape == false)
        {
            escapeValue += 10f*Time.deltaTime;
            if(escapeValue >= escapeValueMax)
            {
                isTryEscape = true;
                if(isTryEscape == true)
                {
                    StartCoroutine(Try());
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            escapeValue = 0;
            isTryEscape = false;
        }

    }

    IEnumerator Try()
    {
        int lucky = Random.Range(0, 100);
        if (lucky <= 3f)
        {
            Debug.Log("탈출에 성공 했습니다.");
            escapeValue = 0;

            yield return new WaitForSeconds(3f);
            isTryEscape = false;
        }
        else
        {
            Debug.Log("탈출에 실패 했습니다.");
            hookCount -= 17f;
            escapeValue = 0;
            yield return new WaitForSeconds(3f);
            isTryEscape = false;
        }
    }


}
