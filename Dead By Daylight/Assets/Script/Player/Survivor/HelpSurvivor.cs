using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelpSurvivor : MonoBehaviour
{
    private SurvivorStat mySurvStat;

    [SerializeField] private GameObject friend;
    private SurvivorStat friend_Stat;
    private HookGauge friend_Hook;
    public bool ishelp;

    private void Start()
    {
        mySurvStat = GetComponent<SurvivorStat>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Survivor")
        {
            friend = collision.gameObject;
            friend_Stat = friend.GetComponent<SurvivorStat>();
            friend_Hook = friend.GetComponent<HookGauge>();

            if(friend_Stat.isHang == false)
            {
                if (friend_Stat.health <= 1)
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
        friend_Stat = null;
        friend_Hook = null;
    }


    private void Treat()
    {
        if (Input.GetMouseButton(0))
        {
            friend_Stat.currentHealth += mySurvStat.speedFriendCare * Time.deltaTime;
            if (friend_Stat.currentHealth >= friend_Stat.maxHealth)
            {
                friend_Stat.currentHealth = 0;
                friend_Stat.health++;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            friend = null;
            friend_Stat = null;
        }
    }

    private void SaveFriend()
    {
        if(Input.GetMouseButton(0))
        {
            mySurvStat.currentSave += mySurvStat.speedSave * Time.deltaTime;
            if(mySurvStat.currentSave >= mySurvStat.maxSave)
            {
                friend_Hook.EscapeHook();
                friend_Hook = null;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            mySurvStat.currentSave = 0;

        }
    }

}
