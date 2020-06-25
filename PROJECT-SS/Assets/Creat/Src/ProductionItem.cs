using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionItem : MonoBehaviour
{
    private GameObject Maker;
    public static bool MakerActivated = false;
    bool OpenInventory;

    private bool NoEnemy;
    private GameObject Ui_interactive;
    static bool flag = false;
    public int WhatMaker_0isWater_1isFood_2isMedicine; //0은 WaterMaker, 1은 FoodMaker, 2은 Medicine
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        if (WhatMaker_0isWater_1isFood_2isMedicine == 0)
        {
            Maker = GameObject.Find("ItemProduction").transform.Find("WaterMaker").gameObject;
        }
        else if (WhatMaker_0isWater_1isFood_2isMedicine == 1)
        {
            Maker = GameObject.Find("ItemProduction").transform.Find("FoodMaker").gameObject;
        }
        else
        {
            Maker = GameObject.Find("ItemProduction").transform.Find("MedicineMaker").gameObject;
        }
        Ui_interactive = GameObject.Find("ItemProduction").transform.Find("UI_InterActive").gameObject;
        open = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && NoEnemy)
        {
            Ui_interactive.SetActive(true);
            transform.GetComponent<Outline>().enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                open = true;
            }
            if (open)
            {
                Ui_interactive.SetActive(false);
                // 열린 건 닫고, 닫힌 건 열기
                MakerActivated = !MakerActivated;
                if (MakerActivated)
                {
                    if (other.CompareTag("Player"))
                    {
                        flag = true;
                        Time.timeScale = 0f;
                        OpenMaker();
                    }
                }

            }
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            Ui_interactive.SetActive(false);
            transform.GetComponent<Outline>().enabled = false;
            CloseMaker();
        }
    }
    private void OpenMaker()
    {
        Maker.SetActive(true);
        GameObject.Find("ItemProduction").transform.Find("ProductPanel").gameObject.SetActive(true);
    }
    private void CloseMaker()
    {
        Maker.SetActive(false);
        GameObject.Find("ItemProduction").transform.Find("ProductPanel").gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //적이 없을땐 Maker 콜라이더 비활성화
        NoEnemy= GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerMovement>().NoEnemy;
        if (NoEnemy){
            transform.GetComponent<CapsuleCollider>().enabled = true;
        }

       
        OpenInventory = GameObject.Find("ingameUIcanvas").transform.Find("inventory").GetComponent<Inventory>().inventoryActivated;
        //인벤토리가 열려있고 상호작용 Ui가 활성화 되어있으면 상호작용 Ui 끄기
        if (OpenInventory&& Ui_interactive.active) 
        {
            Ui_interactive.SetActive(false);
        }

        //인벤토리가 열려있지 않을때만 Maker 닫기가능
        if (!OpenInventory && open && !flag)
        {
            if (Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                CloseMaker();
                open = !open;  
            }
        }
        flag = false;
    }

}