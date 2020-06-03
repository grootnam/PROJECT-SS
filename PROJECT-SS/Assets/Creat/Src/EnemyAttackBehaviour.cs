using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : MonoBehaviour
{
    bool isPlayerInRange;
    private Transform player;

    private Animator Animator;

    //플레이어가 콜라이더에 감지됐다면
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponentInParent<Animator>();
        Animator.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange)
        {
            player = GetComponentInParent<EnemyBehaviour>().follow.transform;
            // 처음 충돌하는 GameObject가 PLAYER인지 확인 벽이면 공격안함
            Vector3 direction = (player.position + Vector3.up) - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform.tag == "Player")
                {
                    Animator.SetBool("Attack", true);
                }
            }

        }
        else
        {
            Animator.SetBool("Attack", false);
        }
    }
}
