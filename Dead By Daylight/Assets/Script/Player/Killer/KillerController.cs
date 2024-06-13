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

    //  ������ �Լ�
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
        // ���θ��� �ൿ������ ������
        // ��Ŭ���� �ϸ� Ÿ���� �����Ѵ�.
        if(!killer_Stat.isActive && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack()) ;
        }

        // ���θ��� �ൿ������ ������
        // ��Ŭ���� ��� ������ �ִٸ�� Ư�� �ɷ��� �����Ѵ�.
        if(!killer_Stat.isActive && Input.GetMouseButton(1))
        {
            // Ư���ɷ��� 2�ʵ��� ������ �ִٸ� ����ǵ����Ѵ�.
            killer_Stat.curSpecialAbility += Time.deltaTime * killer_Stat.specialAbilitySpeed;
            if(killer_Stat.curSpecialAbility >= killer_Stat.maxSpecialAbility)
            {
                StartCoroutine("SpecialAbility");
            }
        }
        // �߰��� ���콺�� ���ٸ� �ٽ� 0���� �����Ѵ�.
        else
        {
            killer_Stat.curSpecialAbility = 0;
        }
    }

    IEnumerator Attack()
    {
        killer_Stat.isActive = true;

        killerAnimator.SetTrigger("TriggerAttack");

        Debug.Log("��Ŭ�� Ÿ�� ����");

        // ��Ŭ���� ������ �� Weapon collider�� active �ǵ��� �ϱ�
        // active�� �Ǹ� ����� ���� �������� hp�� ���ҵǵ��� ��.
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(killer_Stat.activeCount);
        weaponCollider.enabled = false;
        

        // �����ൿ�� ���� ���ϵ��� ��Ÿ���� �����Ŀ� false�� �ǵ��� ������ �ɾ��.
        killer_Stat.isActive = false;
    }

    IEnumerator SpecialAbility()
    {
        /*�ѱ��� ��ġ����

1. ���� ����Ʈ($)�� ������
2. 0.n�ʵ� �Ѿ��� ���ͼ� ������ ���Ѵ�

3. �� �Ѿ��� �����ڿ� ������, 
���� ����Ʈ(&) + hp ����
���Ŀ� ������ ����Ʈ(*)

4. ���� ������,
���� �ð��Ŀ� ������ ����Ʈ(*)*/
        // Ư�� �ɷ� ����
        killer_Stat.isActive = true;

        // ���ܵ� ���� ������ �߻� �ִϸ��̼��� ���Ѵ�.
        anotherWeapon.SetActive(true);
        killerAnimator.SetBool("BShoot", true);

        yield return new WaitForSeconds(0.2f);
        // �ѱ��� ��ġ���� ���� ����Ʈ�� �Բ� �Ѿ��� ���´�.
        effectList[0].SetActive(true);
        effectList[1].SetActive(true);


        GameObject leftBullet = Instantiate(bulletPrefab, leftShootPos);
        GameObject rightBullet = Instantiate(bulletPrefab, rightShootPos);

        // �Ѿ��� ������ ���ϵ��� �Ѵ�.
        Rigidbody leftRigid = leftBullet.GetComponent<Rigidbody>();
        Rigidbody rightRigid = rightBullet.GetComponent<Rigidbody>();

        leftRigid.velocity = leftBullet.transform.forward * 7f;
        rightRigid.velocity = rightBullet.transform.forward * 7f;

        leftBullet.transform.parent = null;
        rightBullet.transform.parent = null;


        // �Ѿ��� ���� ��� ������
        // �Ѿ��� �浹ó���� bullet ���� ���ش�.
        //
        // <bullet�Լ�>
        // 1.5�� ���� �ƹ��͵� ���� ������ ������,
        // ���� �����ڿ� ��Ҵٸ� �� ��� �����ڴ� �װ�, ������.
        

        // ������ ������ ������ �ٸ� ���� ����� ����������.
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
        Debug.Log("�������� �������� ���ƽ��ϴ�.");
    }

}

