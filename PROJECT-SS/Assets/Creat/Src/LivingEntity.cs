using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth;
    public float startingShield;
    public float startingHungry;
    public float startingThirsty;
    public float health;   // 체력
    public int day;        //생존한 날 수
    public float shield;   // 보호막
    public float hungry;   // 배고픔
    public float thirsty;  // 목마름
    public int gold;       // 골드
    public bool isDead;    // 죽었는가?

    // status ui 
    public Slider hpBar;
    public Slider hungryBar;
    public Slider thirstyBar;
    public Text surviveDay;
    public Text goldtext;

    public GameObject ReceiveDamageEffect;

    protected virtual void Start()
    {
        health = startingHealth;
        shield = startingShield;
        hungry = startingHungry;
        thirsty = startingThirsty;
        gold = 0;
        day = 0;
        ReceiveDamageEffect.SetActive(false);

        surviveDay.text = day.ToString();
        goldtext.text = gold.ToString();
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        hpBar.value = health;

        GameObject instanthit = Object.Instantiate<GameObject>(ReceiveDamageEffect);
        instanthit.SetActive(true);
        instanthit.transform.position = transform.position+Vector3.up;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        isDead = true;
        SceneManager.LoadScene("End");
    }
}
