using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private float range;
    private bool pickupActivated = false;   // 습득 가능할 시 true

    [SerializeField]
    private LayerMask itemLayerMask;        // 아이템 레이어에만 반응하도록 레이어 마스크를 설정

    [SerializeField]
    private Inventory theInventory;         // 인벤토리



    void Update()
    {
        CheckItem();
    }

    private void CheckItem()
    {
        // Scene내의 Item을 불러와 검사한다.
        GameObject[] taggedItems = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject taggedItem in taggedItems)
        {
            // Player와의 거리를 구한다.
            Vector3 playerPos = transform.position;
            Vector3 objectPos = taggedItem.transform.position;
            Vector3 distance = objectPos - playerPos;

            // Item을 획득할 수 있는 사정거리 + 1 에 들어왔다면,
            if (distance.magnitude < range + 1)
            {
                // 초록색 Outline을 켜준다.
                taggedItem.GetComponent<Outline>().enabled = true;

                // 사거리 안에 들어왔다면,
                if(distance.magnitude < range)
                {
                    Debug.Log(taggedItem.transform.GetComponent<ItemPickUp>().item.itemName + "획득");

                    // 인벤토리에 넣어준다.
                    theInventory.AcquireItem(taggedItem.transform.GetComponent<ItemPickUp>().item, 1);

                    // 획득한 아이템을 삭제해준다.
                    Destroy(taggedItem);
                }
            }
            // 사정거리 밖이라면,
            else
            {
                // 초록색 Outline을 꺼준다.
                taggedItem.GetComponent<Outline>().enabled = false;
            }
        }
    }
}