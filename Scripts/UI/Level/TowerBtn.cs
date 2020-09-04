using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_BaseTowerObj;
    [SerializeField]
    private GameObject[] TowerClon = new GameObject[15];
    [SerializeField]
    private GameObject[] BulidTower = new GameObject[15];
    [HideInInspector]
    public GameObject m_SelectObject;

    public GameObject m_UICancle;
    private GameObject[] m_arrTower;

    public GameObject m_SMABtn;
    public Image m_Sellimage;
    public Text m_MoveText;
   
    public GameObject m_AttackRangeobj;

    Dictionary<string, int> m_DicMoveclon = new Dictionary<string, int>();

    public AudioClip m_SellClip;

    private void Awake()
    {
        m_arrTower = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < TowerClon.Length; i++)
        {
            m_DicMoveclon.Add(BulidTower[i].ToString(), i);
        }
    }

    public void SellBtn()
    {
        AudioManager.Instance.PlayEffect(m_SellClip);
        GameObject baseTw = Instantiate(m_BaseTowerObj, m_SelectObject.transform.parent.position, Quaternion.identity);

        baseTw.name = m_BaseTowerObj.name;

        baseTw.transform.SetParent(m_SelectObject.transform.parent.transform);

        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        TowerManager.m_nGold += TowerManager.m_DicTowerGold[m_SelectObject.name];

        Destroy(m_SelectObject);

        m_UICancle.SetActive(false);

        TowerPick.m_bSwich = true;
        --TowerManager.m_nCount[TowerManager.m_nTwindex];

        switch (TowerManager.m_DicSynergyCount[TowerManager.m_DicTowerNumber[m_SelectObject.transform.parent.name]])
        {
            case 'A':
                --TowerManager.m_Propertycount[1];
                break;
            case 'B':
                --TowerManager.m_Propertycount[2];
                break;
            case 'C':
                --TowerManager.m_Propertycount[3];
                break;
            case 'D':
                --TowerManager.m_Propertycount[4];
                break;
            case 'E':
                --TowerManager.m_Propertycount[5];
                break;
        }

        TowerManager.m_DicSynergyCount.Remove(TowerManager.m_DicTowerNumber[m_SelectObject.transform.parent.name]);

    }

    public void MoveBtn()
    {
        switch (TowerManager.m_DicSynergyCount[TowerManager.m_DicTowerNumber[m_SelectObject.transform.parent.name]])
        {
            case 'A':
                --TowerManager.m_Propertycount[1];
                break;
            case 'B':
                --TowerManager.m_Propertycount[2];
                break;
            case 'C':
                --TowerManager.m_Propertycount[3];
                break;
            case 'D':
                --TowerManager.m_Propertycount[4];
                break;
            case 'E':
                --TowerManager.m_Propertycount[5];
                break;
        }

        TowerManager.m_DicSynergyCount.Remove(TowerManager.m_DicTowerNumber[m_SelectObject.transform.parent.name]);

        GameObject baseTw = Instantiate(m_BaseTowerObj,
                   m_SelectObject.transform.position,
                    Quaternion.identity);
        baseTw.name = m_BaseTowerObj.name;
        baseTw.transform.SetParent(m_SelectObject.transform.parent.transform);

        GameObject Twclon = Instantiate(TowerClon[m_DicMoveclon[m_SelectObject.ToString()]],
                 m_SelectObject.transform.parent.position,
                   Quaternion.identity);
        Twclon.name = TowerClon[m_DicMoveclon[m_SelectObject.ToString()]].name;
        Twclon.transform.SetParent(baseTw.transform);

        m_UICancle.SetActive(false);

        if (gameObject.transform.GetChild(0).gameObject.activeSelf == true)
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>().enabled = false;
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Button>().enabled = false;
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            m_Sellimage.color = new Color(m_Sellimage.color.r, m_Sellimage.color.g, m_Sellimage.color.b, 0.5f);
            m_MoveText.color = new Color(m_MoveText.color.r, m_MoveText.color.g, m_MoveText.color.b, 0.5f);
        }

        Destroy(m_SelectObject);

        for (int i = 0; i < m_arrTower.Length; ++i)
        {
            if (m_arrTower[i].transform.GetChild(0).name.Equals(m_BaseTowerObj.name))
            {
                Twclon = Instantiate(TowerClon[m_DicMoveclon[m_SelectObject.ToString()]],
                m_arrTower[i].transform.position,
                   Quaternion.identity);
                Twclon.name = TowerClon[m_DicMoveclon[m_SelectObject.ToString()]].name;
                Twclon.transform.SetParent(m_arrTower[i].transform.GetChild(0).transform);
            }
        }
        TowerManager.m_nTwindex = m_DicMoveclon[m_SelectObject.ToString()];

    }
}
