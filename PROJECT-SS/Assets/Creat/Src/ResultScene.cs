using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    LivingEntity livingEntity;
    GameObject player;
    public Text result;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        livingEntity = player.GetComponent<LivingEntity>();
        result.text = livingEntity.day.ToString();
        result.text += " days";
        
        GameObject.Destroy(GameObject.Find("Main Camera"));
        GameObject.Destroy(GameObject.Find("ingameUIcanvas"));
        GameObject.Destroy(GameObject.Find("GameController"));
        GameObject.Destroy(GameObject.Find("Soldier_marine"));
        GameObject.Destroy(GameObject.Find("InGameBGM"));
        GameObject.Destroy(GameObject.Find("EventSystem"));
    }



}
