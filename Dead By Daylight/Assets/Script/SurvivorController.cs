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

    private bool s_Move = true;

    private bool s_Walk;
    private bool s_Run;
    private bool s_Sit;
    private bool s_SitWalk;

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

}
