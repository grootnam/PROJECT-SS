using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    LivingEntity livingEntity;
    public GameObject player;
    public Text result;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        livingEntity = player.GetComponent<LivingEntity>();
        result.text = livingEntity.day.ToString();
        result.text += "일";
    }



}
