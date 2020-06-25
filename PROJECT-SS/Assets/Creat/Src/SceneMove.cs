using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    GameObject player;
    public GameObject destination;
    LivingEntity livingEntity;

    // 미니맵 관련 변수
    bool visited301 = false;
    bool visited305 = false;
    bool visited103 = false;
    bool visited503 = false;
    GameObject map301;
    GameObject map305;
    GameObject map103;
    GameObject map503;
    Vector3 meIcon_vector;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        livingEntity = player.GetComponent<LivingEntity>();
        map301 = GameObject.Find("3-1_hide");
        map305 = GameObject.Find("3-5_hide");
        map103 = GameObject.Find("1-3_hide");
        map503 = GameObject.Find("5-3_hide");
        meIcon_vector = GameObject.Find("meIcon").transform.position;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            // set variable
            livingEntity.day++;
            livingEntity.hungry -= livingEntity.hungryDecreasePerDay;
            livingEntity.thirsty -= livingEntity.thirstyDecreasePerDay;

            // set ui
            livingEntity.surviveDay.text = livingEntity.day.ToString();
            livingEntity.hungryBar.value = livingEntity.hungry;
            livingEntity.thirstyBar.value = livingEntity.thirsty;

            switch (SceneManager.GetActiveScene().name)
            {
                // (1, 3)에서, 
                case "Stage 1-3":
                    switch (destination.name)
                    {
                        // (2, 3)으로 갈때, 
                        case "Goto_2-3":
                            SceneManager.LoadScene("Stage 2-3");    // Scene을 불러온다.
                            meIcon_vector.y -= 50f;                  // 현 위치 표시를 50f만큼 내려준다.
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;

                case "Stage 2-3":
                    switch (destination.name)
                    {
                        case "Goto_1-3":
                            SceneManager.LoadScene("Stage 1-3");
                            meIcon_vector.y += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            // (1, 3)을 방문했다면,
                            if (!visited103)
                            {
                                Destroy(map103);    // 미니맵 (1, 3)을 삭제해 가려둔 맵을 보여준다.
                                visited103 = true;
                            }
                            break;
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            meIcon_vector.y -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;

                case "Stage 3-1":
                    switch (destination.name)
                    {
                        case "Goto_3-2":
                            SceneManager.LoadScene("Stage 3-2");
                            meIcon_vector.x += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;

                case "Stage 3-2":
                    switch (destination.name)
                    {
                        case "Goto_3-1":
                            SceneManager.LoadScene("Stage 3-1");
                            if (!visited301)
                            {
                                Destroy(map301);
                                visited301 = true;
                            }
                            meIcon_vector.x -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            meIcon_vector.x += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;

                case "Stage 3-3":
                case "StartStage":
                    switch(destination.name)
                    {
                        case "Goto_2-3":
                            SceneManager.LoadScene("Stage 2-3");
                            meIcon_vector.y += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                        case "Goto_3-2":
                            SceneManager.LoadScene("Stage 3-2");
                            meIcon_vector.x -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                        case "Goto_3-4":
                            SceneManager.LoadScene("Stage 3-4");
                            meIcon_vector.x += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                        case "Goto_4-3":
                            SceneManager.LoadScene("Stage 4-3");
                            meIcon_vector.y -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;

                case "Stage 3-4":
                    switch (destination.name)
                    {
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            meIcon_vector.x -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                        case "Goto_3-5":
                            SceneManager.LoadScene("Stage 3-5");
                            meIcon_vector.x += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            if (!visited305)
                            {
                                Destroy(map305);
                                visited305 = true;
                            }
                            break;
                    }
                    break;

                case "Stage 3-5":
                    switch (destination.name)
                    {
                        case "Goto_3-4":
                            SceneManager.LoadScene("Stage 3-4");
                            meIcon_vector.x -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;

                case "Stage 4-3":
                    switch (destination.name)
                    {
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            meIcon_vector.y += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                        case "Goto_5-3":
                            SceneManager.LoadScene("Stage 5-3");
                            meIcon_vector.y -= 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            if (!visited503)
                            {
                                Destroy(map503);
                                visited503 = true;
                            }
                            break;
                    }
                    break;

                case "Stage 5-3":
                    switch (destination.name)
                    {
                        case "Goto_4-3":
                            SceneManager.LoadScene("Stage 4-3");
                            meIcon_vector.y += 50f;
                            GameObject.Find("meIcon").transform.position = meIcon_vector;
                            break;
                    }
                    break;
            }


            if (livingEntity.hungry <= 0 || livingEntity.thirsty <= 0)
            {
                livingEntity.Die();
            }


        }
    }
}
