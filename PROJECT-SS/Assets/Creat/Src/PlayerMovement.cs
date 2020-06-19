using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    public float targetRange;
    Transform target;

    [NonSerialized]
    public bool NoEnemy;

    Vector3 movement;
    Animator animator;
    Rigidbody playerRigidbody;
    AudioSource audioSource;
    Quaternion rotation = Quaternion.identity;
    //private Transform gun, ball;
    private float originpitch;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        originpitch = audioSource.pitch;
        //gun = transform.Find("gun");
        //ball = transform.Find("ball");
    }

    void FixedUpdate()
    {
        // 키 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 캐릭터 위치변경
        Move(h, v);

        // 캐릭터 회전
        Rotate();

        //GetComponent<CapsuleCollider>().enabled = !Input.GetMouseButton(1);
    }

    void Move(float h, float v)
    {
        // 좌표 이동
        movement.Set(h, 0.0f, v);
        // 좌표 이동 속도 조절
        movement = movement.normalized * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * Time.deltaTime;

        bool hasHorizontalInput = !Mathf.Approximately(h, 0f);
        bool hasVerticalInput = !Mathf.Approximately(v, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        bool isRun = isWalking && Input.GetKey(KeyCode.LeftShift);
        if (isWalking && !isRun)
        {
            audioSource.pitch = originpitch;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if(isRun){
            audioSource.pitch = originpitch * 2.0f;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        animator.SetBool("IsRun", isRun);
        animator.SetBool("IsWalking", isWalking);
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    void Rotate()
    {
        target = null;
        GetTarget();
        Vector3 desiredForward;
   
        // GetTarget()함수에서 target이 설정되지 않았다면,
        if (target == null)
        {
            // 이동방향(movement)으로 회전벡터 설정
            desiredForward = Vector3.RotateTowards(
                transform.forward,
                movement,
                turnSpeed * Time.deltaTime,
                0f);
        }
        // GetTarget()함수에서 target이 설정됐다면,
        else
        {
            // 플레이어로부터 target으로의 벡터를 생성하고
            Vector3 direction = target.position - transform.position;
            direction.y = 0f;
            direction = direction.normalized * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * Time.deltaTime;
            // 그 벡터 방향으로 회전벡터 설정
            desiredForward = Vector3.RotateTowards(
                transform.forward,
                direction,
                turnSpeed * Time.deltaTime,
                0f);
        }

        rotation = Quaternion.LookRotation(desiredForward);
        playerRigidbody.MoveRotation(rotation);
    }

    void GetTarget()
    {
        
        GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        //씬 안에 몬스터가 없으면
        if (taggedEnemys.Length == 0)
        {
            NoEnemy = true;
        }
        else
        {
            NoEnemy = false;
        }


        // Enemy 태그가 붙은 Object들 각각 검사
        foreach (GameObject taggedEnemy in taggedEnemys)
        {
            // Player와 Enemy의 pos저장
            Vector3 playerPos = transform.position;
            Vector3 objectPos = taggedEnemy.transform.position;
            Vector3 direction = objectPos - playerPos;
    
            /*
            // 장애물이 사이에 있는지 Ray로 검사
            Ray ray = new Ray(playerPos, direction);
            RaycastHit raycastHit;
            bool rayBlocked = true;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform.position == objectPos)
                {
                    rayBlocked = false;
                }
            }
            // Ray검사 통과했다면 ,
            if (!rayBlocked)
            {
            }
            */

            float dist = direction.magnitude;
            // targetRange안의 Object와, Player의 거리 비교
            if (dist < targetRange)
            {
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestEnemy = taggedEnemy.transform;
                }
            }
            
        }
        target = closestEnemy;

        // target의 설정 여부에 따라, 조준 상태를 변경
        if (target == null)
        {
            animator.SetBool("IsAiming", false);
        }
        else
        {
            animator.SetBool("IsAiming", true);
        }
    }

}
