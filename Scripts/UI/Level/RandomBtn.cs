using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomBtn : MonoBehaviour
{
    private GameObject[] BtnChuildoObj = new GameObject[2];
    private void Start()
    {
        BtnChuildoObj[0] = gameObject.transform.GetChild(0).gameObject;
        BtnChuildoObj[1] = gameObject.transform.GetChild(1).gameObject;
    }  
}
