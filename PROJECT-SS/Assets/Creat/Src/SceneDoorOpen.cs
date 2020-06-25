using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoorOpen : MonoBehaviour
{
    private bool NoEnemy;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        //닫힌문
        transform.Find("Door (1)").transform.Find("Door_6_L").transform.localPosition=new Vector3(-0.2f, 0.0f, 0.25f);
        transform.Find("Door (1)").transform.Find("Door_6_R").transform.localPosition = new Vector3(-0.2f, 0.0f, -1f);
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        //적이없으면 문을연다.
        NoEnemy = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerMovement>().NoEnemy;
        //개발자만 아는 L누르면 맵 문열림 ^^
        if(Input.GetKeyDown(KeyCode.L))
        {
            open = !open;
        }

        if (NoEnemy|| open)
        {

            transform.Find("Door (1)").transform.Find("Door_6_L").transform.localPosition = new Vector3(-0.2f, 0.0f, 1.75f);
            transform.Find("Door (1)").transform.Find("Door_6_R").transform.localPosition = new Vector3(-0.2f, 0.0f, -2.5f);
        }
        else if(!NoEnemy || !open)
        {
            transform.Find("Door (1)").transform.Find("Door_6_L").transform.localPosition = new Vector3(-0.2f, 0.0f, 0.25f);
            transform.Find("Door (1)").transform.Find("Door_6_R").transform.localPosition = new Vector3(-0.2f, 0.0f, -1f);
        }
    }
}
