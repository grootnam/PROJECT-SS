using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;
using System;
public class EnemyBehaviour : MonoBehaviour
{
    //체력
    public float health = 100f;
    //몬스터 공격력
    public float Damage = 10;

    private Animator animator;

    [NonSerialized]
    //해치웠나?
    public bool dead = false;
    private CapsuleCollider Collider;
    
    //따라가는 대상
    public GameObject follow;
    private NavMeshAgent nav;

    //움직임
    Quaternion rotation = Quaternion.identity;
    Rigidbody EnemyRigidbody;

    private bool Attack;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EnemyRigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        Collider = GetComponent<CapsuleCollider>();
        follow = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Attack = animator.GetBool("Attack");
        move();
        rotate();
    }
    void move()
    {
        //죽지 않았을때, 공격 하지않을때 쫒아온다.
        if (!dead&&!Attack)
        {
            nav.SetDestination(follow.transform.position);
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
                nav.enabled = false;
                //isTrigger를 활성화하여 물리적으로 부딪힘이 없어짐
                Collider.isTrigger = true;

                //몬스터 오브젝트 삭제(5초후)
                Destroy(gameObject, 5f);
            }
        }
    }
}
