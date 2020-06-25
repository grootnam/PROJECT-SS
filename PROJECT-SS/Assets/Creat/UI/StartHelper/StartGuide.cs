using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGuide : MonoBehaviour
{
    public GameObject player;
    public GameObject guide1;
    public GameObject guide2;
    public GameObject guide3;
    public GameObject panel;
   
    private int clickNum;
    [NonSerialized]
    public bool GuideOpen;
    // 게임 시작 시, 일시정지시키고 첫 번째 가이드를 활성화
    void Start()
    {
        clickNum = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
        guide1.SetActive(true);
        panel.SetActive(true);
        GuideOpen = true;
    }

    // 마우스 입력을 받아 각각 다음 가이드를 출력한다.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrintGuide(clickNum);
            clickNum++;
        }

    }

    // 가이드를 출력한다. 클릭 회수에따라 이전 가이드를 지우고 다음 가이드를 출력
    void PrintGuide(int idx)
    {
        switch(idx)
        {
            case 0:
                Debug.Log("Guide on..." + idx);
                guide1.SetActive(false);
                guide2.SetActive(true);
                break;
            case 1:
                Debug.Log("Guide on..." + idx);
                guide2.SetActive(false);
                guide3.SetActive(true);
                break;
            case 2:
                Debug.Log("Guide on..." + idx);
                guide3.SetActive(false);
                player.GetComponent<PlayerMovement>().enabled = true;
                GuideOpen = false;
                //player.GetComponent<GunController>().enabled = true;
                gameObject.GetComponent<StartGuide>().enabled = false;
                GameObject.Find("GuidePanel").SetActive(false);
                break;
        }
    }
}
