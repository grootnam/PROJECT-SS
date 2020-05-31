using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //체력
    public float health = 100f;

    //해치웠나?
    private bool dead = false;

    //따라가는 대상
    public GameObject follow;
    private NavMeshAgent nav;

    //움직임
    Quaternion rotation = Quaternion.identity;
    Rigidbody EnemyRigidbody;
    public float TurnSpeed=40f;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EnemyRigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotate();
    }
    void move()
    {
        Vector3 dist = follow.transform.position - this.transform.position; 
        //죽지 않았을때만 쫒아온다.
        if (!dead)
        {
            nav.SetDestination(follow.transform.position+ new Vector3(dist.x,0f,dist.z));
        }
        //죽었을 시 도착지를 자기자신으로 재설정해서 멈춤
        else
        {
            nav.SetDestination(transform.position);
        }

    }
    void rotate()
    {
        if(!dead)
            transform.LookAt(follow.transform.position);
    }
    public void ReceiveDamage(float damage)
    {
        health -= damage;

        //피가 0 이하일 때 (죽음)
        if(health<=0)
        {
            dead = true;
            //애니메이션 실행
            animator.SetBool("Dead", dead);
            //죽었으면
            if (dead)
            {
                //태그를 변경함으로서 이미 죽은 몬스터에게 조준안하게함
                this.gameObject.tag = "Untagged";

                //몬스터 오브젝트 삭제(5초후)
                Destroy(gameObject, 5f);
            }
        }
    }
}
