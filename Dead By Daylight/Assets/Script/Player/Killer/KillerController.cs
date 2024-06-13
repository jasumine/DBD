using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;





public class KillerController : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;
    public GameObject anotherWeapon;
    public Camera camera;

    KillerStat killer_Stat;

    public Animator killerAnimator;

    public GameObject bulletPrefab;
    public List<GameObject> effectList;
    public Transform leftShootPos;
    public Transform rightShootPos;


    Rigidbody killRigid;
    Vector3 killPos;

    private void Start()
    {
        killer_Stat = GetComponent<KillerStat>();
        killRigid = GetComponent<Rigidbody>();
        killerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (killer_Stat.isAI == false)
        {
            KillerMove();
            KillerAbillity();
        }
        if (killer_Stat.isWarrios == true)
        {
            KillerRotate();
            KillerMove();
            KillerAbillity();
        }

    }

    // https://funfunhanblog.tistory.com/40
    private void KillerRotate()
    {
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))

        {

            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

        }

    }

    //  움직임 함수
    private void KillerMove()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * x;
        Vector3 moveVertical = transform.forward * z;

        Vector3 movement = moveHorizontal + moveVertical;

        if(movement.magnitude > 0)
        {
            killerAnimator.SetBool("BMove", true);
        }
        else
        {
            killerAnimator.SetBool("BMove", false);
        }

        killPos = (moveHorizontal + moveVertical).normalized * killer_Stat.moveSpeed * Time.deltaTime;
        killRigid.MovePosition(transform.position + killPos);
    }

private void KillerAbillity()
    {
        // 살인마가 행동중이지 않을때
        // 좌클릭을 하면 타격을 실행한다.
        if(!killer_Stat.isActive && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack()) ;
        }

        // 살인마가 행동중이지 않을떄
        // 우클릭을 계속 누르고 있다면면 특수 능력을 실행한다.
        if(!killer_Stat.isActive && Input.GetMouseButton(1))
        {
            // 특수능력을 2초동안 누르고 있다면 실행되도록한다.
            killer_Stat.curSpecialAbility += Time.deltaTime * killer_Stat.specialAbilitySpeed;
            if(killer_Stat.curSpecialAbility >= killer_Stat.maxSpecialAbility)
            {
                StartCoroutine("SpecialAbility");
            }
        }
        // 중간에 마우스를 뗀다면 다시 0부터 시작한다.
        else
        {
            killer_Stat.curSpecialAbility = 0;
        }
    }

    IEnumerator Attack()
    {
        killer_Stat.isActive = true;

        killerAnimator.SetTrigger("TriggerAttack");

        Debug.Log("좌클릭 타격 실행");

        // 좌클릭을 눌렀을 때 Weapon collider가 active 되도록 하기
        // active가 되면 무기와 닿은 생존자의 hp가 감소되도록 함.
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(killer_Stat.activeCount);
        weaponCollider.enabled = false;
        

        // 연속행동을 하지 못하도록 쿨타임이 지난후에 false가 되도록 조건을 걸어둠.
        killer_Stat.isActive = false;
    }

    IEnumerator SpecialAbility()
    {
        /*총구의 위치에서

1. 발포 이펙트($)가 나오고
2. 0.n초뒤 총알이 나와서 앞으로 향한다

3. 이 총알은 생존자에 닿으면, 
맞은 이펙트(&) + hp 감소
이후에 터지는 이펙트(*)

4. 닿지 않으면,
일정 시간후에 터지는 이펙트(*)*/
        // 특수 능력 실행
        killer_Stat.isActive = true;

        // 숨겨둔 총을 꺼내서 발사 애니메이션을 취한다.
        anotherWeapon.SetActive(true);
        killerAnimator.SetBool("BShoot", true);

        yield return new WaitForSeconds(0.2f);
        // 총구의 위치에서 발포 이펙트와 함께 총알이 나온다.
        effectList[0].SetActive(true);
        effectList[1].SetActive(true);


        GameObject leftBullet = Instantiate(bulletPrefab, leftShootPos);
        GameObject rightBullet = Instantiate(bulletPrefab, rightShootPos);

        // 총알이 앞으로 향하도록 한다.
        Rigidbody leftRigid = leftBullet.GetComponent<Rigidbody>();
        Rigidbody rightRigid = rightBullet.GetComponent<Rigidbody>();

        leftRigid.velocity = leftBullet.transform.forward * 7f;
        rightRigid.velocity = rightBullet.transform.forward * 7f;

        leftBullet.transform.parent = null;
        rightBullet.transform.parent = null;


        // 총알이 생성 됬기 때문에
        // 총알의 충돌처리를 bullet 에서 해준다.
        //
        // <bullet함수>
        // 1.5초 동안 아무것도 닿지 않으면 터지고,
        // 만약 생존자와 닿았다면 그 즉시 생존자는 죽고, 터진다.
        

        // 발포가 끝났기 때문에 다른 무기 사용이 가능해진다.
        yield return new WaitForSeconds(1.5f);
        anotherWeapon.SetActive(false);

        killerAnimator.SetBool("BShoot", false);

        effectList[0].SetActive(false);
        effectList[1].SetActive(false);

        killer_Stat.isActive = false;

    }

    public void SetKillerHealth()
    {
        killer_Stat.health--;
        Debug.Log("생존자의 공격으로 다쳤습니다.");
    }

}

