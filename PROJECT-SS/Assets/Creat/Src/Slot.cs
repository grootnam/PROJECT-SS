using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item;       // 획득한 아이템
    public int itemCount;   // 획득한 아이템의 개수
    public Image itemImage; // 인벤토리 창에 띄울 이미지

    // 필요한 컴포넌트
    [SerializeField]
    private Text text_count;            // 아이템 개수 표시
    [SerializeField] 
    private GameObject go_CountImage;   // 아이템 개수 표시 배경
    [SerializeField]
    private ItemEffectDataBase theItemEffectDataBase;   // 아이템의 효과 및 정보

    private void Start()
    {
        // 아이템 정보에 대한 스크립트를 저장한다.
        theItemEffectDataBase = FindObjectOfType<ItemEffectDataBase>(); 
    }

    // * 이미지 투명도 조절 함수
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
 
    }

    // * 아이템 획득 함수
    public void AddItem(Item _item, int _count = 1)
    {
        // 더할 item이 같다면, 
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        // item의 종류가 장비가 아니라면,
        if(item.itemType != Item.ItemType.Equipment)
        {
            // 슬롯 구석에 아이템 개수 칸을 표시한다.
            go_CountImage.SetActive(true);
            text_count.text = itemCount.ToString();
        }
        else
        {
            text_count.text = "0";
            go_CountImage.SetActive(false);
        }
        SetColor(1);
    }
    
    // * 아이템 개수 조정 함수
    public void SetSlotCount(int _count)
    {
        // 아이템을 더해주고, 아이템 개수 칸을 업데이트 한다.
        itemCount += _count;
        text_count.text = itemCount.ToString();

        // 0개라면 슬롯을 비운다.
        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // * 슬롯 초기화 함수
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        go_CountImage.SetActive(false);
        text_count.text = "0";
    }

    // * 아이템 사용관련 이벤트 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        // 이 슬롯에 대하여, 마우스 우클릭이 발생했고,
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            // 슬롯에 아이템이 있다면,
            if(item != null)
            {
                // 그리고 아이템 타입이 소모품이라면,
                if(item.itemType == Item.ItemType.Used)
                {
                    // 아이템의 정보를 가진 DB함수로 보내, 아이템을 사용한다.
                    theItemEffectDataBase.UseItem(item);
                    SetSlotCount(-1);
                }
            }
        }
    }
}
