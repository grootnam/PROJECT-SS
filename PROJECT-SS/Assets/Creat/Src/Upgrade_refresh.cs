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
        livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(false);
        livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(false);
        livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(false);
        livingEntity.upgradeAct[livingEntity.upgradeChoiced1] = false;
        livingEntity.upgradeAct[livingEntity.upgradeChoiced2] = false;
        livingEntity.upgradeAct[livingEntity.upgradeChoiced3] = false;
         

        livingEntity.GetUpgradeList();
        livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(true);
        livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(true);
        livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(true);
    }

}
