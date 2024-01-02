using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGauge : MonoBehaviour
{
    public Hook myHook;
    private SurvivorStat mySurvStat;

    // Start is called before the first frame update
    void Start()
    {
        mySurvStat = GetComponent<SurvivorStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mySurvStat.isHang == true)
        {
            ReduceValue();
        }
    }
    

    public void SetIsHang()
    {
        mySurvStat.isHang = true;

        mySurvStat.hookChance++;
        switch (mySurvStat.hookChance)
        {
            case 1:
                // 처음 걸림
                mySurvStat.currentHookValue = 100;
                break;
            case 2:
                // 두번째 걸림
                // 스킬체크 발생
                mySurvStat.currentHookValue = 50;
                break;
            case 3:
                // 세번째 걸림
                // 죽음
                mySurvStat.currentHookValue = 0;
                break;

        }
    }



    private void ReduceValue()
    {
        mySurvStat.currentHookValue -=  Time.deltaTime;

        if (50 < mySurvStat.currentHookValue && mySurvStat.currentHookValue <= 100)
        {
            // 견자단을 할 수 있음.
            TryEscape();
        }
        else if(0 < mySurvStat.currentHookValue && mySurvStat.currentHookValue <= 50)
        {
            mySurvStat.hookChance = 2;
        }
        else if(mySurvStat.currentHookValue <= 0)
        {
            // 죽음
            mySurvStat.hookChance = 3;
        }
    }

    private void TryEscape()
    {
        if(Input.GetMouseButton(0) && mySurvStat.isTryEscape == false)
        {
            mySurvStat.currentEscape += 10f*Time.deltaTime;
            if(mySurvStat.currentEscape >= mySurvStat.maxEscape)
            {
                mySurvStat.isTryEscape = true;
                if(mySurvStat.isTryEscape == true)
                {
                    StartCoroutine(Try());
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            mySurvStat.currentEscape = 0;
            mySurvStat.isTryEscape = false;
        }

    }

    IEnumerator Try()
    {
        int lucky = Random.Range(0, 100);
        if (lucky <= 3f)
        {
            Debug.Log("탈출에 성공 했습니다.");
            mySurvStat.currentEscape = 0;
            EscapeHook();

            yield return new WaitForSeconds(3f);
            mySurvStat.isTryEscape = false;
        }
        else
        {
            Debug.Log("탈출에 실패 했습니다.");
            mySurvStat.currentHookValue -= 17f;
            mySurvStat.currentEscape = 0;
            yield return new WaitForSeconds(3f);
            mySurvStat.isTryEscape = false;
        }
    }

    public void EscapeHook()
    {
        myHook.isHanging = false;
        mySurvStat.isHang = false;
        myHook = null;
        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.11f, gameObject.transform.position.z);
        mySurvStat.health = 1;
    }
}
