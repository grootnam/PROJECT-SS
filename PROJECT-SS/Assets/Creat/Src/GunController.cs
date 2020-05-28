using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;
    private Animator animator;
    private float currentFireDelayTime;
    private bool isReload = false;

    private RaycastHit hitInfo;


    // 총의 총구 위치
    private Transform gunMuzzle;
    // 발사 총구화염, 예광탄 , 피격시 총 스파크.
    public GameObject muzzleFlash, shot, sparks;

    // 총성
    public AudioClip ShotSound;
    // 재장전음
    public AudioClip ReloadSound;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // 화면상에서 보이는 샷 이펙트들을 숨김.
        muzzleFlash.SetActive(false);
        shot.SetActive(false);
        sparks.SetActive(false);

        // 총구 위치
        gunMuzzle = currentGun.transform.Find("muzzle");
    }

    void Update()
    {
        // 연사 속도를 제어
        if (currentFireDelayTime > 0)
        {
            currentFireDelayTime -= Time.deltaTime;
        }
        
        TryFire();

        TryReload();
        
        
    }

    // 총구화염 숨기기
    void muzzleflashfalse()
    {
        muzzleFlash.SetActive(false);
    }
    // ****************************************
    // 총알 발사 부분

    private void TryFire()
    {
        // 발사 조건 : Key, 연사 대기, 장전 중 X
        if(Input.GetButton("Fire1") && currentFireDelayTime <= 0 && !isReload)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if(!isReload)
        {
            // 장전 된 총알이 있다면 발사
            if (currentGun.loadedBullet > 0)
            {
                Shoot();
            }
            // 장전 된 총알이 없다면 재장전
            else
            {
                StartCoroutine(ReloadCoroutine());
            }
        }
                
    }

    private void Shoot()
    {
        // 발사 시 처리 : 총알 갯수, 연사 대기시간 초기화, 애니메이션 재생
        currentGun.loadedBullet--;
        currentFireDelayTime = currentGun.fireDelayTime;
        animator.SetTrigger("Fire");
        Hit();

        // 총성 재생
        AudioSource.PlayClipAtPoint(ShotSound, gunMuzzle.position, 5f);

        // 총구 화염효과
        muzzleFlash.SetActive(true);
        muzzleFlash.transform.SetParent(gunMuzzle);
        muzzleFlash.transform.localPosition = Vector3.zero;
        muzzleFlash.transform.localEulerAngles = Vector3.back;
        // 0.3초뒤 총구 화염 숨김함수 호출
        Invoke("muzzleflashfalse", 0.3f);

        // 예광탄
        GameObject instantShot = Object.Instantiate<GameObject>(shot);
        instantShot.SetActive(true);
        instantShot.transform.position = gunMuzzle.position;
        instantShot.transform.rotation = Quaternion.LookRotation(currentGun.transform.up * -1);
        instantShot.transform.parent = shot.transform.parent;

        
    }

    private void Hit()
    {
        if(Physics.Raycast(currentGun.transform.position, currentGun.transform.up * -1, out hitInfo, currentGun.range))
        {
            Debug.Log(hitInfo.transform.name);
            // 피격시 총알의 스파크 표시 
            GameObject instantSparks = Object.Instantiate<GameObject>(sparks);
            instantSparks.SetActive(true);
            instantSparks.transform.position = hitInfo.point;

            //적 피격
            if(hitInfo.transform.gameObject.tag=="Enemy") // 적태그가 적용된 오브젝트만 적용
                hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>().ReceiveDamage(currentGun.damage);
        }
    }

    // ****************************************
    // 총알 재장전 부분

    private void TryReload()
    {
        // 재장전 조건 : R키, 장전 중 X, 풀장전 상태 X
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.loadedBullet < currentGun.magSize)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }


    IEnumerator ReloadCoroutine()
    {
        // 총알이 남아있다면,
        if(currentGun.totalBullet > 0)
        {
            //재장전 오디오클립 재생
            AudioSource.PlayClipAtPoint(ReloadSound, gunMuzzle.position, 5f);

            // isReload로 장전 시작 표현
            isReload = true;
            animator.SetTrigger("Reload");

            // 탄이 남았을 때는 load된 총알을 전체 총알에 합치고 장전
            currentGun.totalBullet += currentGun.loadedBullet;
            currentGun.loadedBullet = 0;

            // 장전 시간만큼 대기
            yield return new WaitForSeconds(currentGun.reloadTime);

            // 탄창 사이즈만큼 총알이 있다면 풀장전
            if(currentGun.totalBullet >= currentGun.magSize)
            {
                currentGun.loadedBullet = currentGun.magSize;
                currentGun.totalBullet -= currentGun.magSize;
            }
            // 그렇지 않으면 남은 총알이라도 장전
            else
            {
                currentGun.loadedBullet = currentGun.totalBullet;
                currentGun.totalBullet = 0;
            }

            // isReload로 장전 종료 표현
            isReload = false;
        }
        else
        {
            Debug.Log("No Bullet!!");
        }
    }
}
