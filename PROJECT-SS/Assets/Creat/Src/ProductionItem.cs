using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionItem : MonoBehaviour
{
    private GameObject Maker;
    public static bool MakerActivated = false;

    static bool flag=false;
    public int WhatMaker_0isWater_1isFood_2isMedicine; //0은 WaterMaker, 1은 FoodMaker, 2은 Medicine
    // Start is called before the first frame update
    void Start()
    {
        if (WhatMaker_0isWater_1isFood_2isMedicine == 0)
        {
            Maker = GameObject.Find("ItemProduction").transform.Find("WaterMaker").gameObject;
        }
        else if(WhatMaker_0isWater_1isFood_2isMedicine == 1)
        {
            Maker = GameObject.Find("ItemProduction").transform.Find("FoodMaker").gameObject;
        }
        else
        {
            Maker = GameObject.Find("ItemProduction").transform.Find("MedicineMaker").gameObject;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        transform.GetComponent<Outline>().enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
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
    private void OnTriggerExit(Collider other)
    {
        transform.GetComponent<Outline>().enabled = false;
        CloseMaker();
    }
    private void OpenMaker()
    {
        Maker.SetActive(true);
    }
    private void CloseMaker()
    {
        Maker.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)&& !flag)
        {
            Time.timeScale = 1f;
            CloseMaker();
        }
        flag = false;
    }
 
   
}
