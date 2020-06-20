using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceBoard : MonoBehaviour
{
    private bool isOpen;

    [SerializeField]
    GameObject enhanceBoard;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                enhanceBoard.SetActive(true);
            }
            else
            {
                enhanceBoard.SetActive(false);
            }
        }
    }
}
