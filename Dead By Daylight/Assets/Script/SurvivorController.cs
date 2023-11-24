using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    Run,
    Sit
}

public class SurvivorController : MonoBehaviour
{
    public float moveSpeed;
    private float health = 2;

    private bool isSurvMove = true;

    private bool isSurvWalk;
    private bool isSurvRun;
    private bool isSurvSit;
    private bool isSurvSitWalk;

    Rigidbody surRigid;
    Vector3 surPos;

    PlayerStates playerState;

    private void Start()
    {
        surRigid = GetComponent<Rigidbody>();
        playerState = PlayerStates.Idle;
    }

    private void Update()
    {
        SurvivorMove();
        CheckState();
        CheckHealth();
    }


    private void SurvivorMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        switch (playerState)
        {
            case PlayerStates.Idle:
                surPos = new Vector3(x, 0f, z).normalized * moveSpeed * Time.deltaTime;
                surRigid.MovePosition(transform.position + surPos);
                break;
            case PlayerStates.Run:
                surPos = new Vector3(x, 0f, z).normalized * (moveSpeed *2.5f) * Time.deltaTime;
                surRigid.MovePosition(transform.position + surPos);
                break;
            case PlayerStates.Sit:
                Renderer render = GetComponent<Renderer>();
                render.material.color = Color.yellow;

                surPos = new Vector3(x, 0f, z).normalized * (moveSpeed*0.5f )* Time.deltaTime;
                surRigid.MovePosition(transform.position + surPos);
                break;


        }


    }

    private void CheckState()
    {
        float run = Input.GetAxis("Run");
        bool shiftInput = !Mathf.Approximately(run, 0f);

        float sit = Input.GetAxis("Sit");
        bool ctrlInput = !Mathf.Approximately(sit, 0f);

        if (shiftInput && ctrlInput)
        {
            playerState = PlayerStates.Sit;
        }
        else if (shiftInput)
        {
            playerState = PlayerStates.Run;
        }
        else if (ctrlInput)
        {
            playerState = PlayerStates.Sit;
        }
        else
        {
            playerState = PlayerStates.Idle;
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.white;
        }
    }


    private void CheckHealth()
    {
        switch(health)
        {
            case 2:
                break;
            case 1:
                break;
            case 0:
                break;
        }
    }

    public void Sethealth()
    {
        health--;
        Debug.Log("살인마의 공격으로 다쳤습니다.");
        Debug.Log(health);
        // 무적상태로 만들기
    }
}
