using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public static bool m_bLimlit = true;
    private int[] m_nRandom = new int[5];
    public GameObject[] m_arrBtn = new GameObject[5];
    public GameObject[] m_arrSlot = new GameObject[5];
    private GameObject[] m_arrIcon = new GameObject[5];


    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            m_nRandom[i] = Random.Range(0, 5);
            m_arrIcon[i] = Instantiate(m_arrBtn[m_nRandom[i]], m_arrSlot[i].transform.position, Quaternion.identity);
            m_arrIcon[i].transform.SetParent(m_arrSlot[i].transform);
            m_arrIcon[i].name = m_arrBtn[m_nRandom[i]].name;
            m_arrIcon[i].transform.localScale = new Vector3(0.56f, 2.9f, 1.0f);
        }
    }

    public void OnClickDown()
    {
        int price = 2;
        if (price <= TowerManager.m_nGold)
        {
            TowerManager.m_nGold -= price;
            for (int i = 0; i < 5; i++)
            {
                Destroy(m_arrIcon[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                m_nRandom[i] = Random.Range(0, 5);

                m_arrIcon[i] = Instantiate(m_arrBtn[m_nRandom[i]], m_arrSlot[i].transform.position, Quaternion.identity);

                m_arrIcon[i].transform.SetParent(m_arrSlot[i].transform);
                m_arrIcon[i].transform.localScale = new Vector3(0.56f, 2.9f, 1.0f);
            }
        }
    }
}
