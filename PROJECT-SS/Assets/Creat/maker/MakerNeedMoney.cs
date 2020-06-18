using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MakerNeedMoney : MonoBehaviour
{
    public Text itemcount;
    private Text Needmoney;
    private int price;
    // Start is called before the first frame update
    void Start()
    {
        price = GetComponentInParent<Price>().price;
        Needmoney = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Needmoney.text=(int.Parse(itemcount.text) * price).ToString();
    }
}
