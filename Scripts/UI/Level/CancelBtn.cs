using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelBtn : MonoBehaviour
{
    private GameObject[] m_arrBaseTowerObj;
    private GameObject m_TowerSwich;

    private void Start()
    {
        m_TowerSwich = transform.parent.gameObject;
    }
    private void OnEnable()
    {
        m_arrBaseTowerObj = GameObject.FindGameObjectsWithTag("BaseTower");
    }

    public void Cancel()
    {   
        if (TowerPick.m_bSwich == true)
        {
            TowerManager.m_nGold += TowerClonIcon.m_nCancelPrice;
            for (int i = 0; i < m_arrBaseTowerObj.Length; ++i)
            {
                if (m_arrBaseTowerObj[i].transform.GetChild(0) != null)
                {
                    Destroy(m_arrBaseTowerObj[i].transform.GetChild(0).gameObject);
                }
            }         
        }
        else if (TowerPick.m_bSwich == false)
        {
            m_TowerSwich.transform.GetChild(0).gameObject.SetActive(false);
            m_TowerSwich.transform.GetChild(2).gameObject.SetActive(false);
            TowerPick.m_bSwich = true;
        }
        gameObject.SetActive(false);       
    }

    public void Cancel1()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.transform.parent.transform.GetChild(2).gameObject.SetActive(false);
       
        TowerPick.m_bSwich = true;
        TowerPick.selectPoint = SelectPoint.Nono.ToString();
    }
}
