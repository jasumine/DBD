using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Healthy,
    Hurt,
    Down
}

public class SurvivorController : MonoBehaviour
{
    public float moveSpeed;

    private bool s_Move;

    private bool s_Walk;
    private bool s_Run;
    private bool s_Sit;
    private bool s_SitWalk;


    private void SurvivorMove()
    {
       if (s_Move)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
        }

    }


}
