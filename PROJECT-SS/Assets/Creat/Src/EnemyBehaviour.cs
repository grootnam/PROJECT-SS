using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Security.Cryptography;

public class EnemyBehaviour : MonoBehaviour
{
    //체력
    public float health = 100f;
    //몬스터 공격력
    public float Damage = 10;

    //날짜별 증가되는 적의 강함...
    public float DayPlusDamage, DayPlusSpeed, DayPlusHealth;
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

    //오디오소스
    AudioSource[] audioSources;

    //LivingEntity에서 day가져옴
    private int day;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EnemyRigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        Collider = GetComponent<CapsuleCollider>();
        follow = GameObject.FindGameObjectWithTag("Player");
        audioSources = GetComponents<AudioSource>();
        day = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>().day;
        Damage += day * DayPlusDamage;
        nav.speed += day * DayPlusSpeed;
        health += day * DayPlusHealth;
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
            if (!audioSources[0].isPlaying)
            {
                audioSources[0].Play();
            }
            audioSources[1].Stop();
            if (nav.stoppingDistance >= Vector3.Distance(follow.transform.position, transform.position))
            {
                nav.Stop();
            }
            else
            {
                nav.Resume();
                nav.SetDestination(follow.transform.position);
            }
            
        }
        else
        {
            if (!dead && Attack)
            {
                if (!audioSources[1].isPlaying)
                {
                    audioSources[1].Play();
                }
            }
            else
            {
                audioSources[1].Stop();
            }
            audioSources[0].Stop();
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
                Collider.enabled = false;

                audioSources[2].Play();
                //몬스터 오브젝트 삭제(5초후)
                Destroy(gameObject, 5f);
            }
        }
    }
}
