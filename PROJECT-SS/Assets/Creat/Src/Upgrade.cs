using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private GameObject UpgradeUI;
    private GameObject UpgradeUIPanel;

    static bool isClosed = false;
    static bool flag = false;

    void Start()
    {
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
    private void OpenUpgradeUI()
    {
        UpgradeUI.SetActive(true);
        UpgradeUIPanel.SetActive(true);
    }
    private void CloseUpgradeUI()
    {
        UpgradeUI.SetActive(false);
        UpgradeUIPanel.SetActive(false);
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
