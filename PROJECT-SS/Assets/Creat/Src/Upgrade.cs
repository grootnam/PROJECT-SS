using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    LivingEntity livingEntity;

    // ui
    private GameObject UpgradeUI;
    private GameObject UpgradeUIPanel;
    private GameObject UpgradeExp;

    // ui 열고 닫음에 관여하는 변수
    static bool isClosed = false;
    static bool flag = false;

    void Start()
    {
        livingEntity = GameObject.FindWithTag("Player").GetComponent<LivingEntity>();
        UpgradeUI = GameObject.Find("Upgrade").transform.Find("upgrade_base").gameObject;
        UpgradeUIPanel = GameObject.Find("Upgrade").transform.Find("upgrade_panel").gameObject;
        UpgradeExp = GameObject.Find("Upgrade").transform.Find("upgrade_explicate").gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        transform.GetComponent<Outline>().enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
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

    private void OnTriggerExit(Collider other)
    {
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
        if (Input.GetKeyDown(KeyCode.E) && !flag)
        {
            Time.timeScale = 1f;
            CloseUpgradeUI();
        }
        flag = false;
    }
}
