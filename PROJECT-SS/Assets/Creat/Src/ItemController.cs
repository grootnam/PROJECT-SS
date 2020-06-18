using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private float range;
    private bool pickupActivated = false;   // 습득 가능할 시 true
    private RaycastHit hitInfo;             // 충돌체 정보 저장

    [SerializeField]
    private LayerMask itemLayerMask;        // 아이템 레이어에만 반응하도록 레이어 마스크를 설정

    [SerializeField]
    private Inventory theInventory;         // 인벤토리

    
    void Update()
    {
        CheckItem();
        TryAction();        
    }

    private void TryAction()
    {
        // E를 누르면,
        if(Input.GetKeyDown(KeyCode.E))
        {
            // 획득할 수 있는 Item을 확인하고,
            CheckItem();

            // 할 수 있다면,
            CanPickUp();
        }
    }

    GameObject rayhit;
    private void CheckItem()
    {
        // 아이템 레이어 마스크에서, 플레이어의 방향으로 RayCast 했을 때,
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, itemLayerMask))
        {
            // 충돌한 물체가 "Item" 태그를 갖는다면
            if (hitInfo.transform.tag == "Item")
            {
                //먹을수 있는 아이템인 경우 테두리 효과 ON!
                hitInfo.transform.GetComponent<Outline>().enabled = true;
                rayhit = hitInfo.transform.gameObject;
                // 먹을 수 있는 아이템인 것을 표시
                ItemInfoAppear();
            }
        }
        // RayCast 결과 충돌하지 않았다면, 그 반대
        else
        {
            if(rayhit!=null)
                rayhit.GetComponent<Outline>().enabled = false;
            ItemInfoDisappear();
        }

    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        /*
         * 아이템 위에 UI 효과를 넣어주는 것 구현!
         */
    }
    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        /*
         * 아이템 위에 UI 효과를 꺼주는 것 구현!
         */
    }

    // 아이템 획득 가능하다면,
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득");

                // 인벤토리에 넣어주기
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item, 1);
                
                // 획득 했으면 아이템 삭제 및 정보 꺼주기
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}
