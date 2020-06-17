using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // ui
    private GameObject UpgradeUI;
    private GameObject UpgradeUIPanel;

    // ui 열고 닫음에 관여하는 변수
    static bool isClosed = false;
    static bool flag = false;

    // 선택된 번호 
    LivingEntity livingEntity;



    void Start()
    {
        livingEntity = GameObject.FindWithTag("Player").GetComponent<LivingEntity>();
        UpgradeUI = GameObject.Find("Upgrade").transform.Find("upgrade_base").gameObject;
        UpgradeUIPanel = GameObject.Find("Upgrade").transform.Find("upgrade_panel").gameObject;
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
        livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(true);
        livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(true);
        livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(true);
    }

    // close
    private void CloseUpgradeUI()
    {
        UpgradeUI.SetActive(false);
        UpgradeUIPanel.SetActive(false);
        livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(false);
        livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(false);
        livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(false);
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
