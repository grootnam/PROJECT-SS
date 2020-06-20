using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private bool interactionActivated = false;
    private RaycastHit hitInfo;             // 충돌체 정보 저장
    private LivingEntity PlayerStats;

    [SerializeField]
    private float range;

    [SerializeField]
    private LayerMask objectLayerMask;      // 상호작용 레이어에만 반응하도록 레이어 마스크를 설정


    void Update()
    {
        CheckObject();
        TryAction();
    }

    private void TryAction()
    {
        // F를 누르면,
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 상호작용할 수 있는 Object를 확인하고,
            CheckObject();

            // 할 수 있다면,
            CanInteract();
        }
    }

    private void CheckObject()
    {
        // 오브젝트 레이어 마스크에서, 플레이어의 방향으로 RayCast 했을 때,
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, objectLayerMask))
        {
            // 충돌한 물체가 "Object" 태그를 갖는다면
            if (hitInfo.transform.tag == "Object")
            {
                // 상호작용 할 수 있는 오브젝트인 것을 표시
                ObjectInfoAppear();
            }
        }
        // RayCast 결과 충돌하지 않았다면, 그 반대
        else
        {
            ObjectInfoDisappear();
        }
    }
    private void ObjectInfoAppear()
    {
        interactionActivated = true;
        /*
         * Object 위에 UI 효과를 넣어주는 것 구현!
         */
    }
    private void ObjectInfoDisappear()
    {
        interactionActivated = false;
        /*
         * Object 위에 UI 효과를 꺼주는 것 구현!
         */
    }

    // 상호작용이 가능하다면,
    private void CanInteract()
    {
        if (interactionActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득");

                /* 
                 * 오브젝트의 상호작용 기능 함수 호출 구현
                 */

                // 획득 했으면 아이템 삭제 및 정보 꺼주기
                Destroy(hitInfo.transform.gameObject);
                ObjectInfoDisappear();
            }
        }
    }
}

