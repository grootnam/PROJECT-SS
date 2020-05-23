using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    public float targetRange;
    Transform target;

    Vector3 movement;
    Animator animator;
    Rigidbody playerRigidbody;
    Quaternion rotation = Quaternion.identity;
    //private Transform gun, ball;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
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
        movement.Set(h, 0f, v);
        // 좌표 이동 속도 조절
        movement = movement.normalized * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * Time.deltaTime;

        bool hasHorizontalInput = !Mathf.Approximately(h, 0f);
        bool hasVerticalInput = !Mathf.Approximately(v, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        bool isRun = isWalking && Input.GetKey(KeyCode.LeftShift);

        animator.SetBool("IsRun", isRun);
        animator.SetBool("IsWalking", isWalking);
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    void Rotate()
    {
        target = null;
        getTarget();
        Vector3 desiredForward;
        if (target == null)
        {

            desiredForward = Vector3.RotateTowards(
                transform.forward,
                movement,
                turnSpeed * Time.deltaTime,
                0f);
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f;
            direction = direction.normalized * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * Time.deltaTime;
            desiredForward = Vector3.RotateTowards(
                transform.forward,
                direction,
                turnSpeed * Time.deltaTime,
                0f);
        }
        rotation = Quaternion.LookRotation(desiredForward);
        playerRigidbody.MoveRotation(rotation);
    }

    void getTarget()
    {
        GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        // Enemy 태그가 붙은 Object들 각각 검사
        foreach (GameObject taggedEnemy in taggedEnemys)
        {
            // Player와 Enemy의 pos저장
            Vector3 playerPos = transform.position;
            Vector3 objectPos = taggedEnemy.transform.position;

            // 장애물이 사이에 있는지 Ray로 검사
            Vector3 direction = objectPos - playerPos;
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
            if (true)
            {
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
        }
        target = closestEnemy;
    }
}
