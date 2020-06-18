using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItem : MonoBehaviour
{
    public Sprite WaterImage, FoodImage, MedicineImage;
    private Item item;
    private int itemcount;
    [SerializeField]
    private Inventory theInventory;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void makeitem()
    {
        itemcount = int.Parse(GameObject.Find("ItemCount").GetComponent<TextNumCountSet>().textcount.text);
        if (transform.parent.GetComponent<ProductionItem>().WhatMaker_0isWater_1isFood_2isMedicine == 0)
        {
            item.itemName = "Water";
            item.itemImage = WaterImage;
            item.itemType = Item.ItemType.Used;
            item.itemCost = 50;
        }
        else if (transform.parent.GetComponent<ProductionItem>().WhatMaker_0isWater_1isFood_2isMedicine == 1)
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
        theInventory.AcquireItem(item, itemcount);
    }
}
