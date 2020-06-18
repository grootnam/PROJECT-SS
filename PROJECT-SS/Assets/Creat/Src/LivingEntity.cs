using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth;
    public float startingShield;
    public float startingHungry;
    public float startingThirsty;
    public float health;   // 체력
    public int day;        //생존한 날 수
    public float shield;   // 보호막
    public float hungry;   // 배고픔
    public float thirsty;  // 목마름
    public int gold;       // 골드
    public bool isDead;    // 죽었는가?
    public int hungryDecreasePerDay;
    public int thirstyDecreasePerDay;

    // status ui 
    public Slider hpBar;
    public Slider hungryBar;
    public Slider thirstyBar;
    public Text surviveDay;
    public Text goldtext;

    public GameObject ReceiveDamageEffect;

    // Upgrade
    public const int upgradeListNum = 9;
    public bool[] upgradeAct = new bool[upgradeListNum]; // 강화 리스트 중 3개를 선택할 때, 선택된 리스트를 true로 만듦
    public int[] upgradeLevel = new int[upgradeListNum]; // 현재 강화된 레벨
    public int[] upgradeMaxLevel = new int[upgradeListNum]; // 강화 최대 레벨

    public GameObject[] listIcon1;
    public GameObject[] listIcon2;
    public GameObject[] listIcon3;
    public GameObject[] expIcon1;
    public GameObject[] expIcon2;
    public GameObject[] expIcon3;
    public GameObject[] expTitle1;
    public GameObject[] expTitle2;
    public GameObject[] expTitle3;
    public GameObject[] expExp1;
    public GameObject[] expExp2;
    public GameObject[] expExp3;

    // upgrade selected index
    public int upgradeChoiced1;
    public int upgradeChoiced2;
    public int upgradeChoiced3;

    Gun gunStatus;
    PlayerMovement playerMovement;


    protected virtual void Start()
    {
        // variable
        health = startingHealth;
        shield = startingShield;
        hungry = startingHungry;
        thirsty = startingThirsty;
        hungryDecreasePerDay = 7;
        thirstyDecreasePerDay = 7;
        gold = 0;
        day = 0;
        ReceiveDamageEffect.SetActive(false);

        gunStatus = GameObject.Find("weapon_m4").GetComponent<Gun>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        // ui
        surviveDay.text = day.ToString();
        goldtext.text = gold.ToString();

        for (int i=0; i<9; i++)
        {
            // 강화 관련 변수 초기화
            upgradeAct[i] = false;
            upgradeLevel[i] = 0; //initail level = 0
            switch(i) //max level
            {
                case 6:
                case 7:
                    upgradeMaxLevel[i] = 2;
                    break;

                case 10:
                case 11:
                    upgradeMaxLevel[i] = 1;
                    break;

                default:
                    upgradeMaxLevel[i] = 5;
                    break;
            }
        }
        GetUpgradeList(); // 게임 시작 시 최초로 업그레이드 리스트 갱신
    }  

    public void TakeHit(float damage)
    {
        health -= damage;
        hpBar.value = health;

        GameObject instanthit = Object.Instantiate<GameObject>(ReceiveDamageEffect);
        instanthit.SetActive(true);
        instanthit.transform.position = transform.position+Vector3.up;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        isDead = true;
        SceneManager.LoadScene("End");
    }


    // 강화할 수 있는 내용이 남아있는가?
    public bool isRemainList()
    {
        for (int i = 0; i < upgradeListNum; i++)
        {
            if (!upgradeAct[i] && (upgradeLevel[i] != upgradeMaxLevel[i]))
            {
                return true;
                break;
            }
        }

        return false;
    }

    // 첫 시작 또는 refresh 할 때, 강화 가능 리스트를 setup 한다.
    // 뽑히지 않을 조건 : 이미 뽑힌 리스트이거나, maxLevel까지 강화되었거나.
    public void GetUpgradeList()
    {
        // 첫 번째 칸 선택
        while (true)
        {
            int randIdx = UnityEngine.Random.Range(0, upgradeListNum);

            if (!isRemainList())
                break;

            if (!upgradeAct[randIdx] && (upgradeLevel[randIdx] != upgradeMaxLevel[randIdx]))
            {
                upgradeChoiced1 = randIdx;
                upgradeAct[randIdx] = true;
                Debug.Log(upgradeChoiced1);
                break;
            }
        }
         
        // 두 번째 칸 선택
        while (true)
        {
            int randIdx = UnityEngine.Random.Range(0, upgradeListNum);

            if (!isRemainList())
                break;

            if (!upgradeAct[randIdx] && (upgradeLevel[randIdx] != upgradeMaxLevel[randIdx]))
            {
                upgradeChoiced2 = randIdx;
                upgradeAct[randIdx] = true;
                Debug.Log(upgradeChoiced2);
                break;
            }
        }

        // 세 번째 칸 선택
        while (true)
        {
            int randIdx = UnityEngine.Random.Range(0, upgradeListNum);

            if (!isRemainList())
                break;

            if (!upgradeAct[randIdx] && (upgradeLevel[randIdx] != upgradeMaxLevel[randIdx]))
            {
                upgradeChoiced3 = randIdx;
                upgradeAct[randIdx] = true;
                Debug.Log(upgradeChoiced3);
                break;
            }
        }
    }

    public void ApplyUpgrade(int upgradeIdx)
    {
        switch(upgradeIdx)
        {
            case 0: //무기 공격력 증가
                gunStatus.damage += 1;
                break;

            case 1: //무기 연사력 증가 = 발사간격 감소
                gunStatus.fireDelayTime *= 0.9f;
                break;

            case 2: //무기 장전시간 감소
                gunStatus.reloadTime *= 0.9f;
                break;

            case 3: //플레이어 이속 증가
                playerMovement.walkSpeed *= 1.1f;
                playerMovement.runSpeed *= 1.1f;
                playerMovement.turnSpeed *= 1.1f;
                break;

            case 4: //플레이어 체력 증가
                health *= 1.1f;
                hpBar.maxValue *= 1.1f;
                break;

            case 5: //배고픔 감소량 감소
                hungryDecreasePerDay -= 1;
                break;

            case 6: //목마름 감소량 감소
                thirstyDecreasePerDay -= 1;
                break;

            case 7: //화폐 획득량 증가
                break;

            case 8: //장전하는 탄 수 증가
                gunStatus.magSize += 3;
                break;
        }
    }

}


