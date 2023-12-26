using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelpSurvivor : MonoBehaviour
{
    [SerializeField] private GameObject friend;
    private SurvivorController friend_Controller;
    private HookGauge friend_Hook;
    public bool ishelp;

    public float saveValue;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Survivor")
        {
            friend = collision.gameObject;
            friend_Controller = friend.GetComponent<SurvivorController>();
            friend_Hook = friend.GetComponent<HookGauge>();

            if(friend_Hook.isHang == false)
            {
                if (friend_Controller.health <= 1)
                {
                    Treat();
                }
            }
            else
            {
                SaveFriend();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        friend = null;
        friend_Controller = null;
        friend_Hook = null;
    }


    private void Treat()
    {
        if (Input.GetMouseButton(0))
        {
            friend_Controller.healthValue += 10 * Time.deltaTime;
            if (friend_Controller.healthValue >= 100)
            {
                friend_Controller.healthValue = 0;
                friend_Controller.health++;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            friend = null;
            friend_Controller = null;
        }
    }

    private void SaveFriend()
    {
        if(Input.GetMouseButton(0))
        {
            saveValue += 2 * Time.deltaTime;
            if(saveValue >=5)
            {
                friend_Hook.EscapeHook();
                friend_Hook = null;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            saveValue = 0;

        }
    }

}
