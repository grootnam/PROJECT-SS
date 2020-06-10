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
    public int day; //생존한 날 수
    public float shield; // 보호막
    public float hungry; // 배고픔
    public float thirsty; // 목마름
    public bool isDead; // 죽었는가?

    public Slider hpBar;
    public Text surviveDay;

    public GameObject ReceiveDamageEffect;

    protected virtual void Start()
    {
        health = startingHealth;
        shield = startingShield;
        hungry = startingHungry;
        thirsty = startingThirsty;
        day = 0;
        ReceiveDamageEffect.SetActive(false);

        surviveDay.text = day.ToString();
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        hpBar.value = health;

        GameObject instanthit = Object.Instantiate<GameObject>(ReceiveDamageEffect);
        instanthit.SetActive(true);
        instanthit.transform.position = transform.position+Vector3.up;

        if (health <= 0 && !isDead)
            Die();
    }

    protected void Die()
    {
        isDead = true;
        //GameObject.Destroy(gameObject);
    }
}
