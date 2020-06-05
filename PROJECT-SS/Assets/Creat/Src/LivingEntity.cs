using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth;
    public float startingShield;
    public float startingHungry;
    public float startingThirsty;
    public float health; // 체력
    protected float shield; // 보호막
    protected float hungry; // 배고픔
    protected float thirsty; // 목마름
    protected bool isDead; // 죽었는가?

    public Slider hpBar;



    protected virtual void Start()
    {
        health = startingHealth;
        shield = startingShield;
        hungry = startingHungry;
        thirsty = startingThirsty;
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        hpBar.value = health;

        if (health <= 0 && !isDead)
            Die();
    }

    protected void Die()
    {
        isDead = true;
        //GameObject.Destroy(gameObject);
    }
}
