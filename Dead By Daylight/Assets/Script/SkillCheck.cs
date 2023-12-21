using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Build;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    public RectTransform arrowRectTrans;
    public RectTransform circleRectTrans;

    private void Update()
    {
        //if(RectOverlaps())
        //{
        //    Debug.Log("ȭ��ǥ�� ��ųüũ �浹");
        //}

    }

    bool RectOverlaps()
    {
        Rect arrowRect = arrowRectTrans.rect;
        Rect circleRect = circleRectTrans.rect;





        return arrowRect.Overlaps(circleRect);
    }


    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag=="Arrow")
        {
            Debug.Log("collision �浹");
        }
    }


}
