using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNumCountSet : MonoBehaviour
{
    [NonSerialized]
    public Text textcount;

    int count;  // 상품갯수
    int price;  // 상품가격
    private int currentJewerly;  //현재 보석개수

    public Sprite WaterImage, FoodImage, MedicineImage; // 제작 아이템의 이미지
    private Item item;

    [SerializeField]
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {    
        textcount=GetComponent<Text>();
        price = GetComponentInParent<Price>().price;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textcount.text =  count.ToString();
        currentJewerly = int.Parse(GameObject.Find("jewerly_text").GetComponent<Text>().text);
    }

    // 제작할 개수를 +하는 함수
    public void PlusText()
    {
        // 지불 금액만큼 보석이 있을 때만 +
        if(price * (count + 1) <= currentJewerly)
            count += 1;
    }
    // 제작할 개수를 -하는 함수
    public void MinusText()
    {
        // 0 이하가 안되게 -
        if (count >= 1)
        {
            count -= 1;
        }
    }
    
    public void makeitem()
    {
        // 새로운 아이템을 제작한다.
        item = ScriptableObject.CreateInstance<Item>();
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
        // 인벤토리에 추가한다.
        theInventory.AcquireItem(item, count);

        // 제작할 개수를 다시 0으로 초기화한다.
        count = 0;
    }
}
