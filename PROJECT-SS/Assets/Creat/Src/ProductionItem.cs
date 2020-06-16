using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionItem : MonoBehaviour
{
    public GameObject Maker;
    public static bool MakerActivated = false;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Maker = GameObject.Find("ItemProduction").transform.Find("WaterMaker").gameObject;
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 열린 건 닫고, 닫힌 건 열기
            MakerActivated = !MakerActivated;
            if (MakerActivated)
            {
                if (other.CompareTag("Player"))
                {
                    OpenMaker();
                }
            }
            else
            {
                CloseMaker();
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CloseMaker();
    }
    private void OpenMaker()
    {
        Maker.SetActive(true);
    }
    private void CloseMaker()
    {
        Maker.SetActive(false);
    }
}
