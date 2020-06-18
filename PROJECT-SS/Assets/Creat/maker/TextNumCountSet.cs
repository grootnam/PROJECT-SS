using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNumCountSet : MonoBehaviour
{
    [NonSerialized]
    public Text textcount;

    //상품갯수
    int Count;

    //상품가격
    int price;

    //현재 보석개수
    private int CurrentJewerly;

    public Sprite WaterImage, FoodImage, MedicineImage;
    private Item item;
    [SerializeField]
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        
        textcount=GetComponent<Text>();
        price = GetComponentInParent<Price>().price;
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textcount.text =  Count.ToString();
        CurrentJewerly = int.Parse(GameObject.Find("jewerly_text").GetComponent<Text>().text);
    }
    public void PlusText()
    {
        //보석보다 돈이 모자르면 증가안함.
        if(price*(Count+1)<=CurrentJewerly)
            Count += 1;
    }
    public void MinusText()
    {
        //0이하 안돼
        if (Count >= 1)
        {
            Count -= 1;
        }
    }
    
    public void makeitem()
    {
        item = new Item();
        if (GameObject.FindGameObjectWithTag("Maker").GetComponent<ProductionItem>().WhatMaker_0isWater_1isFood_2isMedicine==0)
        {
            item.itemName = "Water";
            item.itemImage = WaterImage;
            item.itemType = Item.ItemType.Used;
            item.itemCost = 50;
        }
        else if (GameObject.FindGameObjectWithTag("Maker").GetComponent<ProductionItem>().WhatMaker_0isWater_1isFood_2isMedicine == 1)
        {
            item.itemName = "Food";
            item.itemImage = FoodImage;
            item.itemType = Item.ItemType.Used;
            item.itemCost = 100;
        }
        else
        {
            item.itemName = "Medicine";
            item.itemImage = MedicineImage;
            item.itemType = Item.ItemType.Used;
            item.itemCost = 200;
        }
        theInventory.AcquireItem(item, Count);
        Count = 0;
    }
}
