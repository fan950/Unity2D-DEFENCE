using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerClonIcon : MonoBehaviour
{
    private GameObject[] m_arrBaseTowerObj;
    public GameObject m_TowerClonObj;

    private GameObject m_ClonObj;
    private TowerBtn m_TowerBtn;

    private GameObject[] TowerChildObj = new GameObject[2];

    public int m_nIndex;

    public int m_nPrice;
    public static int m_nCancelPrice;

    private void Start()
    {
        m_arrBaseTowerObj = GameObject.FindGameObjectsWithTag("BaseTower");
        TowerChildObj[0] = gameObject.transform.GetChild(0).gameObject;
        TowerChildObj[1] = gameObject.transform.GetChild(1).gameObject;
        m_TowerBtn = UIAdd.Get<TowerBtn>(UIType.TowerBtn);
    }

    public void OnClickDown()
    {
        if (m_nPrice <= TowerManager.m_nGold)
        {
            m_nCancelPrice = m_nPrice;

            m_arrBaseTowerObj = GameObject.FindGameObjectsWithTag("BaseTower");
            if (m_TowerBtn.transform.GetChild(1).gameObject.activeSelf != true && TowerPick.selectPoint != SelectPoint.GR.ToString())
            {
                TowerManager.m_nGold -= m_nPrice;
                for (int i = 0; i < m_arrBaseTowerObj.Length; ++i)
                {
                    if (m_arrBaseTowerObj[i] != null)
                    {
                        m_ClonObj = Instantiate(m_TowerClonObj, m_arrBaseTowerObj[i].transform.parent.transform.position, Quaternion.identity);
                        m_ClonObj.transform.SetParent(m_arrBaseTowerObj[i].transform);
                        m_ClonObj.name = m_TowerClonObj.name;

                        Destroy(gameObject);

                        if (m_arrBaseTowerObj[i].transform.childCount >= 3)
                        {
                            Destroy(m_arrBaseTowerObj[i].transform.GetChild(1).gameObject);
                        }
                    }
                }

                if (m_TowerBtn.transform.GetChild(1).gameObject.activeSelf == false)
                {
                    m_TowerBtn.transform.GetChild(1).gameObject.SetActive(true);
                }

                if (TowerPick.m_bSwich == true)
                {
                    TowerManager.m_nTwindex = m_nIndex;
                }
                TowerPick.m_bSwich = true;
            }
        }
    }

}
