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
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
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

        /*
        * 발사 시 총구 스파크 효과
        * 발사 시 총성 오디오 효과
       
        구현 해 주시면 됩니다!
        */
 
    }

    private void Hit()
    {
        if(Physics.Raycast(currentGun.transform.position, currentGun.transform.up * -1, out hitInfo, currentGun.range))
        {
            Debug.Log(hitInfo.transform.name);
            /*
            * 인수인계 사항
            * - "currentGun.transform.up * -1" 이 총구의 방향입니다
            * - hitInfo 위에 선언되어 있고, Raycast한 정보 이용해서 피격 Effect 구현해 넣어주세요!

            구현 해 주시면 됩니다!
            */
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
