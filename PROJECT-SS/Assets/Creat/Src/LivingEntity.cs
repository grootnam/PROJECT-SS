using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.ComponentModel;
using System.Runtime.InteropServices;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth;    // 기본 체력
    public float startingHungry;    // 기본 배고픔
    public float startingThirsty;   // 기본 목마름

    public int day;        // 생존한 날 수
    
    public float health;   // 체력       
    public float hungry;   // 배고픔
    public float thirsty;  // 목마름

    public int gold;       // 골드
    public bool isDead;    // 생존여부

    public int hungryDecreasePerDay;    // !배고픔 감소량
    public int thirstyDecreasePerDay;   // !목마름 감소량

    // 상태값 UI
    public Slider hpBar;
    public Slider hungryBar;
    public Slider thirstyBar;
    public Text surviveDay;
    public Text goldtext;
    
    // 강화값 UI
    [SerializeField]
    private Text[] enhancements;

    // 강화 초기값
    private float originGunDamage;      // 0번 강화 : 총 데미지
    private float originFireSpeed;      // 1번 강화 : 총 연사 속도
    private float originReloadSpeed;    // 2번 강화 : 총 장전 속도
    private float originWalkSpeed;      // 3번 강화 : 플레이어 이동 속도
    private float originMaxHealth;      // 4번 강화 : 최대 체력
    private float originHungryDecrease; // 5번 강화 : 최대 허기
    private float originThirstyDecrease;// 6번 강화 : 최대 목마름
    private int originMagSize;          // 7번 강화 : 탄창 사이즈
    

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

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    Gun gunStatus; 
    PlayerMovement playerMovement;
    public Inventory inventory;


    protected virtual void Start()
    {
        // 시작하면 변수들을 기초값으로 초기화한다.
        health = startingHealth;
        hungry = startingHungry;
        thirsty = startingThirsty;

        hungryDecreasePerDay = 7;
        thirstyDecreasePerDay = 7;

        gold = 0;
        day = 0;

        ReceiveDamageEffect.SetActive(false);

        gunStatus = GameObject.Find("weapon_m4").GetComponent<Gun>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();


        originGunDamage = gunStatus.damage;
        enhancements[0].text = originGunDamage.ToString();

        originFireSpeed = gunStatus.fireDelayTime;
        enhancements[1].text = originFireSpeed.ToString();

        originReloadSpeed = gunStatus.reloadTime;
        enhancements[2].text = originReloadSpeed.ToString();

        originWalkSpeed = playerMovement.walkSpeed;
        enhancements[3].text = originWalkSpeed.ToString();

        originMaxHealth = hpBar.maxValue;
        enhancements[4].text = originMaxHealth.ToString();

        originHungryDecrease = hungryDecreasePerDay;
        enhancements[5].text = originHungryDecrease.ToString();

        originThirstyDecrease = thirstyDecreasePerDay;
        enhancements[6].text = originThirstyDecrease.ToString();

        enhancements[7].text = "10";

        originMagSize = gunStatus.magSize;
        enhancements[8].text = originMagSize.ToString();

        // ui
        surviveDay.text = day.ToString();
        goldtext.text = gold.ToString();

        // 강화 관련 변수 초기화
        for (int i=0; i<9; i++)
        {
            upgradeAct[i] = false;
            upgradeLevel[i] = 0; //initail level = 0
            switch(i) //max level
            {
                case 0:
                case 2:
                case 4:
                    upgradeMaxLevel[i] = 100;
                    break;
                case 5:
                case 6:
                    upgradeMaxLevel[i] = 2;
                    break;

                case 9:
                case 10:
                    upgradeMaxLevel[i] = 1;
                    break;

                default:
                    upgradeMaxLevel[i] = 5;
                    break;
            }
        }
        GetUpgradeList(); // 게임 시작 시 최초로 업그레이드 리스트 갱신
    }  
    
    // 상태값을 변경할 때 쓰는 함수
    public void plusHealth(float plus)
    {
        health += plus;
        if (health > hpBar.maxValue)
            health = hpBar.maxValue;
        if (health < 0)
            health = 0;
        hpBar.value = health;
    }
    public void plusHungry(float plus)
    {
        hungry += plus;
        if (hungry > hungryBar.maxValue)
            hungry = hungryBar.maxValue;
        if (hungry < 0)
            hungry = 0;
        hungryBar.value = hungry;
    }
    public void plusThirsty(float plus)
    {
        thirsty += plus;
        if (thirsty > thirstyBar.maxValue)
            thirsty = thirstyBar.maxValue;
        if (thirsty < 0)
            thirsty = 0;
        thirstyBar.value = thirsty;
        
    }
    public void plusGold(int plus)
    {
        gold += plus;
        goldtext.text = gold.ToString();
    }
    public void plusDay(int plus)
    {
        day += plus;
        surviveDay.text = day.ToString();
    }

    // 공격 받았을 때의 함수
    public void TakeHit(float damage)
    {
        plusHealth(-damage);

        GameObject instanthit = Object.Instantiate<GameObject>(ReceiveDamageEffect);
        instanthit.SetActive(true);
        instanthit.transform.position = transform.position+Vector3.up;

        if (health <= 0)
            Die();
    }

    // 캐릭터가 죽어 게임이 끝나는 함수
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
            if ((upgradeLevel[i] != upgradeMaxLevel[i]))
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
                enhancements[0].text = string.Format("{0:f2}", gunStatus.damage) + "<color=yellow>(+" + string.Format("{0:f2}", (gunStatus.damage - originGunDamage))+ ")</color>";
                Debug.Log("무기 공격력 증가");
                break;

            case 1: //무기 연사력 증가 = 발사간격 감소
                gunStatus.fireDelayTime *= 0.9f;
                enhancements[1].text = string.Format("{0:f2}", gunStatus.fireDelayTime) + "<color=yellow>(-" + string.Format("{0:f2}", (originFireSpeed - gunStatus.fireDelayTime)) + ")</color>";
                Debug.Log("무기 연사력 증가");
                break;

            case 2: //무기 장전시간 감소
                gunStatus.reloadTime *= 0.9f;
                enhancements[2].text = string.Format("{0:f2}", gunStatus.reloadTime) + "<color=yellow>(-" + string.Format("{0:f2}", (originReloadSpeed - gunStatus.reloadTime)) + ")</color>";
                Debug.Log("무기 장전시간 감소");
                break;

            case 3: //플레이어 이속 증가
                playerMovement.walkSpeed *= 1.1f;
                playerMovement.runSpeed *= 1.1f;
                playerMovement.turnSpeed *= 1.1f;
                enhancements[3].text = string.Format("{0:f2}", playerMovement.walkSpeed) + "<color=yellow>(+" + string.Format("{0:f2}", (playerMovement.walkSpeed - originWalkSpeed)) + ")</color>";
                Debug.Log("이속 증가");
                break;

            case 4: //플레이어 체력 증가
                health *= 1.1f;
                hpBar.maxValue *= 1.1f;
                enhancements[4].text = string.Format("{0:f2}", hpBar.maxValue) + "<color=yellow>(+" + string.Format("{0:f2}", (hpBar.maxValue - originMaxHealth)) + ")</color>";
                Debug.Log("체력 증가");
                break;

            case 5: //배고픔 감소량 감소
                hungryDecreasePerDay -= 1;
                enhancements[5].text = hungryDecreasePerDay.ToString() + "<color=yellow>(-" + (originHungryDecrease - hungryDecreasePerDay).ToString() + ")</color>";
                Debug.Log("배고픔 감소");
                break;

            case 6: //목마름 감소량 감소
                thirstyDecreasePerDay -= 1;
                enhancements[6].text = thirstyDecreasePerDay.ToString() + "<color=yellow>(-" + (originThirstyDecrease - thirstyDecreasePerDay).ToString() + ")</color>";
                Debug.Log("목마름 감소");
                break;

            case 7: //화폐 획득량 증가
                inventory.getMoreMoney += 2;
                enhancements[7].text = (10 + inventory.getMoreMoney).ToString() + "<color=yellow>(+" + (inventory.getMoreMoney).ToString() + ")</color>";
                Debug.Log("화폐 획득량 증가");
                break;

            case 8: //장전하는 탄 수 증가
                gunStatus.magSize += 3;
                enhancements[8].text = gunStatus.magSize.ToString() + "<color=yellow>(+" + (gunStatus.magSize - originMagSize).ToString() + ")</color>";
                Debug.Log("장전 탄 수 증가");
                break;

            

        }
    }

}


