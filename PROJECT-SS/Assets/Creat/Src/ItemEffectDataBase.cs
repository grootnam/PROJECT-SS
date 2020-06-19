using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템의 DataBase를 Inspector 창에서 다음의 형식으로 입력 받는다.
[System.Serializable]
class ItemEffect
{
    public string itemName; // 아이템 이름
    public string part;     // 아이템 적용 부분
    public int num;         // 아이템 적용값
}


public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;   // 아이템 정보에 대한 배열

    private LivingEntity playerStatus;  // 플레이어의 상태값
    private const string                // 상태값 항목을 구별할 문자열
        HP = "HP",
        THIRSTY = "THIRSTY",
        HUNGRY = "HUNGRY";

    private void Start()
    {
        // 플레이어의 상태값을 수정할 수 있도록 저장한다.
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>();
    }

    public void UseItem(Item _item)
    {
        // 아이템이 소모품이라면,
        if(_item.itemType == Item.ItemType.Used)
        {
            // Inspector에서 정의한 아이템 항목을 검사해서,
            for(int x = 0; x < itemEffects.Length; x++)
            {
               // 이름을 비교해 지금 사용할 item의 정보를 찾고,
               if (itemEffects[x].itemName == _item.itemName)
                {
                    // part(상태값 항목)에 num(수치)만큼 적용해준다.
                    switch(itemEffects[x].part)
                    {
                        case HP:
                            playerStatus.health += itemEffects[x].num;
                            if (playerStatus.health > 100)
                                playerStatus.health = 100;
                            break;
                        case THIRSTY:
                            playerStatus.thirsty += itemEffects[x].num;
                            if (playerStatus.thirsty > 100)
                                playerStatus.thirsty = 100;
                            break;
                        case HUNGRY:
                            playerStatus.hungry += itemEffects[x].num;
                            if (playerStatus.hungry > 100)
                                playerStatus.hungry = 100;
                            break;
                    }
                    return;
                }
            }
        }
    }
}
