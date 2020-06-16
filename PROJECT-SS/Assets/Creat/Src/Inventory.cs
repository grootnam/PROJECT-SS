using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    private int money = 0;  // 화폐 보유량
    private Slot[] slots;   // 인벤토리 슬롯의 배열

    [SerializeField]
    private GameObject go_InventoryBase;    // 인벤토리 창

    [SerializeField]
    private GameObject go_SlotsParent;      // 인벤토리 슬롯들의 컨테이너

    [SerializeField]
    private Text go_jewerlyText;            // 화페 UI


    // 인벤토리에 각 슬롯을 저장
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        // 'I'를 누르면,
        if(Input.GetKeyDown(KeyCode.I))
        {
            // 열린 건 닫고, 닫힌 건 열기
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                Time.timeScale = 0f;
                OpenInventory();
            }
            else
            {
                Time.timeScale = 1f;
                CloseInventory();
            }
        }
    }

    // 인벤토리 창 열기
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }
    // 인벤토리 창 닫기
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    // 아이템을 획득(ItemController에서 사용)
    public void AcquireItem(Item _item, int _count = 1)
    {
        // 획득한 Item이 화폐면, 
        if(Item.ItemType.Money == _item.itemType)
        {
            // 보유금액 올려주고, UI에 반영
            money += _item.itemCost;
            go_jewerlyText.text = money.ToString();
            return;
        }

        // 획득한 Item이 장비가 아니면,
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                // 인벤토리에 이미 같은 item이 있을 때
                if(slots[i].item != null)
                {
                    // Count만 증가시킴
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
              
            }
        }
        // 빈 칸이 있다면 칸을 앞으로 당겨주기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
