using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_do : MonoBehaviour
{
    LivingEntity livingEntity;


    void Start()
    {
        livingEntity = GameObject.FindWithTag("Player").GetComponent<LivingEntity>();
    }

    public void Choice1()
    {
        if (int.Parse(livingEntity.goldtext.text) >= 100)
        {
            livingEntity.goldtext.text = (int.Parse(livingEntity.goldtext.text) - 100).ToString();
            livingEntity.upgradeLevel[livingEntity.upgradeChoiced1]++;
            livingEntity.listIcon1[livingEntity.upgradeChoiced1].SetActive(false);
            livingEntity.ApplyUpgrade(livingEntity.upgradeChoiced1);
        }
    }

    public void Choice2()
    {
        if (int.Parse(livingEntity.goldtext.text) >= 100)
        {
            livingEntity.goldtext.text = (int.Parse(livingEntity.goldtext.text) - 100).ToString();
            livingEntity.upgradeLevel[livingEntity.upgradeChoiced2]++;
            livingEntity.listIcon2[livingEntity.upgradeChoiced2].SetActive(false);
            livingEntity.ApplyUpgrade(livingEntity.upgradeChoiced2);
        }
    }

    public void Choice3()
    {
        if (int.Parse(livingEntity.goldtext.text) >= 100)
        {
            livingEntity.goldtext.text = (int.Parse(livingEntity.goldtext.text) - 100).ToString();
            livingEntity.upgradeLevel[livingEntity.upgradeChoiced3]++;
            livingEntity.listIcon3[livingEntity.upgradeChoiced3].SetActive(false);
            livingEntity.ApplyUpgrade(livingEntity.upgradeChoiced3);
        }
    }

}
