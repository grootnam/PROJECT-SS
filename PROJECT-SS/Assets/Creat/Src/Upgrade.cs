using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    LivingEntity livingEntity;

    // ui
    private GameObject UpgradeUI;
    private GameObject UpgradeUIPanel;
    private GameObject UpgradeExp;
    private GameObject Ui_interactive;

    // ui 열고 닫음에 관여하는 변수
    static bool isClosed = false;
    static bool flag = false;
    bool OpenInventory;
    private bool NoEnemy;

    private GameObject Upgrader;

    
    void Start()
    {
        livingEntity = GameObject.FindWithTag("Player").GetComponent<LivingEntity>();
        UpgradeUI = GameObject.Find("ingameUIcanvas").transform.Find("Upgrade").transform.Find("upgrade_base").gameObject;
        UpgradeUIPanel = GameObject.Find("ingameUIcanvas").transform.Find("Upgrade").transform.Find("upgrade_panel").gameObject;
        UpgradeExp = GameObject.Find("ingameUIcanvas").transform.Find("Upgrade").transform.Find("upgrade_explicate").gameObject;
        Ui_interactive = GameObject.Find("ingameUIcanvas").transform.Find("ItemProduction").transform.Find("UI_InterActive").gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && NoEnemy)
        {
            Ui_interactive.SetActive(true);
            transform.GetComponent<Outline>().enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ui_interactive.SetActive(false);
                // 열린 건 닫고, 닫힌 건 열기
                isClosed = !isClosed;
                if (!isClosed)
                {
                    if (other.CompareTag("Player"))
                    {
                        flag = true;
                        Time.timeScale = 0f;
                        OpenUpgradeUI();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ui_interactive.SetActive(false);
        transform.GetComponent<Outline>().enabled = false;
        CloseUpgradeUI();
    }

    // open
    private void OpenUpgradeUI()
    {
        UpgradeUI.SetActive(true);
        UpgradeUIPanel.SetActive(true);
        UpgradeExp.SetActive(true);

        livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(true);
        livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(true);
        livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(true);

        livingEntity.expIcon1[livingEntity.upgradeChoiced1].SetActive(true);
        livingEntity.expIcon2[livingEntity.upgradeChoiced2].SetActive(true);
        livingEntity.expIcon3[livingEntity.upgradeChoiced3].SetActive(true);

        livingEntity.expTitle1[livingEntity.upgradeChoiced1].SetActive(true);
        livingEntity.expTitle2[livingEntity.upgradeChoiced2].SetActive(true);
        livingEntity.expTitle3[livingEntity.upgradeChoiced3].SetActive(true);

        livingEntity.expExp1[livingEntity.upgradeChoiced1].SetActive(true);
        livingEntity.expExp2[livingEntity.upgradeChoiced2].SetActive(true);
        livingEntity.expExp3[livingEntity.upgradeChoiced3].SetActive(true);

        if (int.Parse(livingEntity.goldtext.text) < 100)
        {
            livingEntity.button1.SetActive(false);
            livingEntity.button2.SetActive(false);
            livingEntity.button3.SetActive(false);
        }
        else
        {
            livingEntity.button1.SetActive(true);
            livingEntity.button2.SetActive(true);
            livingEntity.button3.SetActive(true);
        }
    }

    // close
    private void CloseUpgradeUI()
    {
        UpgradeUI.SetActive(false);
        UpgradeUIPanel.SetActive(false);
        UpgradeExp.SetActive(false);
        livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(false);
        livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(false);
        livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(false);
        livingEntity.expIcon1[livingEntity.upgradeChoiced1].SetActive(false);
        livingEntity.expIcon2[livingEntity.upgradeChoiced2].SetActive(false);
        livingEntity.expIcon3[livingEntity.upgradeChoiced3].SetActive(false);
        livingEntity.expExp1[livingEntity.upgradeChoiced1].SetActive(false);
        livingEntity.expExp2[livingEntity.upgradeChoiced2].SetActive(false);
        livingEntity.expExp3[livingEntity.upgradeChoiced3].SetActive(false);
        livingEntity.upgradeAct[livingEntity.upgradeChoiced1] = false;
        livingEntity.upgradeAct[livingEntity.upgradeChoiced2] = false;
        livingEntity.upgradeAct[livingEntity.upgradeChoiced3] = false;
    }


    void Update()
    {
        NoEnemy = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerMovement>().NoEnemy;
        if (NoEnemy)
        {
            transform.GetComponent<CapsuleCollider>().enabled = true;
        }

        OpenInventory = GameObject.Find("ingameUIcanvas").transform.Find("inventory").GetComponent<Inventory>().inventoryActivated;
        //인벤토리가 열려있고 상호작용 Ui가 활성화 되어있으면 상호작용 Ui 끄기
        if (OpenInventory && Ui_interactive.active)
        {
            Ui_interactive.SetActive(false);
        }

        if (!OpenInventory && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && !flag)
        {
            Time.timeScale = 1f;
            CloseUpgradeUI();
        }
        flag = false;
    }
}
