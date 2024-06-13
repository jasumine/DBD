using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject tailEffect;
    public GameObject explosionEffect;

    public float lifeTime =0f;

    private bool isCoroutine = false;

    private void Start()
    {
        lifeTime = 0;
    }

    private void Update()
    {
        Lifetime();
    }


    private void Lifetime()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > 1.5f && isCoroutine == false)
        {
            StartCoroutine("BombBullet");
        }
    }





    private void OnTriggerEnter(Collider other)
    {
       if(other.tag =="Survivor")
        {
            Debug.Log("생존자가 총에 맞았습니다.");
            SurvivorController surv = other.GetComponent<SurvivorController>();

            if(isCoroutine== false)
            {
                surv.Sethealth();
                surv.Sethealth();

                StartCoroutine("BombBullet");

            }
        }
    }


    IEnumerator BombBullet()
    {
        isCoroutine = true;

        tailEffect.SetActive(false);
        explosionEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        isCoroutine = false;

        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);

        //isCoroutine = false;

    }
}
