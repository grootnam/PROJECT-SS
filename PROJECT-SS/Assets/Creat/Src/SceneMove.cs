using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    GameObject player;
    public GameObject destination;
    LivingEntity livingEntity;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        livingEntity = player.GetComponent<LivingEntity>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            livingEntity.day++;
            livingEntity.hungry -= 7;
            livingEntity.thirsty -= 7;

            if(livingEntity.hungry <=0 || livingEntity.thirsty<=0)
            {
                livingEntity.isDead = true;
            }

            switch (SceneManager.GetActiveScene().name)
            {
                case "Stage 1-3":
                    switch (destination.name)
                    {
                        case "Goto_2-3":
                            SceneManager.LoadScene("Stage 2-3");
                            break;
                    }
                    break;

                case "Stage 2-3":
                    switch (destination.name)
                    {
                        case "Goto_1-3":
                            SceneManager.LoadScene("Stage 1-3");
                            break;
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            break;
                    }
                    break;

                case "Stage 3-1":
                    switch (destination.name)
                    {
                        case "Goto_3-2":
                            SceneManager.LoadScene("Stage 3-2");
                            break;
                    }
                    break;

                case "Stage 3-2":
                    switch (destination.name)
                    {
                        case "Goto_3-1":
                            SceneManager.LoadScene("Stage 3-1");
                            break;
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            break;
                    }
                    break;

                case "Stage 3-3":
                case "StartStage":
                    switch(destination.name)
                    {
                        case "Goto_2-3":
                            SceneManager.LoadScene("Stage 2-3");
                            break;
                        case "Goto_3-2":
                            SceneManager.LoadScene("Stage 3-2");
                            break;
                        case "Goto_3-4":
                            SceneManager.LoadScene("Stage 3-4");
                            break;
                        case "Goto_4-3":
                            SceneManager.LoadScene("Stage 4-3");
                            break;
                    }
                    break;

                case "Stage 3-4":
                    switch (destination.name)
                    {
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            break;
                        case "Goto_3-5":
                            SceneManager.LoadScene("Stage 3-5");
                            break;
                    }
                    break;

                case "Stage 3-5":
                    switch (destination.name)
                    {
                        case "Goto_3-4":
                            SceneManager.LoadScene("Stage 3-4");
                            break;
                    }
                    break;

                case "Stage 4-3":
                    switch (destination.name)
                    {
                        case "Goto_3-3":
                            SceneManager.LoadScene("Stage 3-3");
                            break;
                        case "Goto_5-3":
                            SceneManager.LoadScene("Stage 5-3");
                            break;
                    }
                    break;

                case "Stage 5-3":
                    switch (destination.name)
                    {
                        case "Goto_4-3":
                            SceneManager.LoadScene("Stage 4-3");
                            break;
                    }
                    break;
            }


        }
    }
}
