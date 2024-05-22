using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriosCamera : MonoBehaviour
{
    public Transform plyaerPos;
    public Vector3 cameraPos;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }


    private void FollowPlayer()
    {
        transform.position = plyaerPos.position + cameraPos;
    }

}
