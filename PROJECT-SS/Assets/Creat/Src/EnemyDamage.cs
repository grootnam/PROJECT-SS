using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float Damage;
    private CapsuleCollider Collider;
    private bool Dead;
    private bool Attack;
    EnemyBehaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GameObject.FindWithTag("Enemy").GetComponent<EnemyBehaviour>();

        //EnemyBehaviour에서 데미지를 가져온다.
        Damage = enemyBehaviour.Damage;

        //몬스터 사망 시 캡슐 콜라이더 비활성화(사망 시 콜라이더위를 지나가면 올라탐)
        Collider = GetComponent<CapsuleCollider>();

    }
    void Update()
    {
        //Enemy 죽었나?
        Dead = GetComponentInParent<EnemyBehaviour>().dead;
        //Enemy가 공격모션중인가?
        Attack = GetComponentInParent<Animator>().GetBool("Attack");

        //죽었으면 콜라이더 비활성화를 통해 시체위를 지나가도 피해 안입음
        if (Dead)
            Collider.enabled = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        //접근한 콜라이더가 플레이어인가와 Enemy가 공격모션중인지 확인 (그냥 손흔드는거에 데미지가 들어오는것을 확인해서)
        if (other.transform.tag == "Player"&&Attack)
        {
            other.gameObject.GetComponent<LivingEntity>().TakeHit(Damage);
        }
    }
}
