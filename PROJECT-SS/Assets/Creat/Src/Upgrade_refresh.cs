using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Upgrade_refresh : MonoBehaviour
{
    LivingEntity livingEntity;

    public void Start() 
    {
        livingEntity = GameObject.FindWithTag("Player").GetComponent<LivingEntity>();
    }

    public void refresh()
    {
        if (int.Parse(livingEntity.goldtext.text) >= 50)
        {
            livingEntity.goldtext.text = (int.Parse(livingEntity.goldtext.text) - 50).ToString();
            livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(false);
            livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(false);
            livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(false);
            livingEntity.expIcon1[livingEntity.upgradeChoiced1].SetActive(false);
            livingEntity.expIcon2[livingEntity.upgradeChoiced2].SetActive(false);
            livingEntity.expIcon3[livingEntity.upgradeChoiced3].SetActive(false);
            livingEntity.expTitle1[livingEntity.upgradeChoiced1].SetActive(false);
            livingEntity.expTitle2[livingEntity.upgradeChoiced2].SetActive(false);
            livingEntity.expTitle3[livingEntity.upgradeChoiced3].SetActive(false);
            livingEntity.expExp1[livingEntity.upgradeChoiced1].SetActive(false);
            livingEntity.expExp2[livingEntity.upgradeChoiced2].SetActive(false);
            livingEntity.expExp3[livingEntity.upgradeChoiced3].SetActive(false);
            livingEntity.upgradeAct[livingEntity.upgradeChoiced1] = false;
            livingEntity.upgradeAct[livingEntity.upgradeChoiced2] = false;
            livingEntity.upgradeAct[livingEntity.upgradeChoiced3] = false;

            // refresh
            livingEntity.GetUpgradeList();
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
    }

}
