using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyUI : MonoBehaviour
{
    public GameObject[] m_SynergyObj = new GameObject[8];
    private GameObject[] m_arrTwObj;

    private int[] m_arrTwimp;

    public Image[] m_BackImg = new Image[8];
    public Image[] m_SynergyInImg = new Image[8];

    public Sprite m_BackGroundY;
    public Sprite m_BackGroundW;

    public GameObject m_CenterObj;
    private bool[] m_bLimit;

    public GameObject m_TowerPlus;
    public GameObject m_TowerMinus;

    private SpriteRenderer m_PlustW;
    private SpriteRenderer m_MinustW;

    private Vector3[] m_arrTwPlusPos;
    private Vector3[] m_arrTwMinusPos;

    public void Init(TowerManager towerMng)
    {
        m_arrTwObj = new GameObject[towerMng.m_LisTwNumber.Count];       

        m_arrTwPlusPos = new Vector3[m_arrTwObj.Length];
        m_arrTwMinusPos = new Vector3[m_arrTwObj.Length];
        m_arrTwimp = new int[m_arrTwObj.Length];

        m_PlustW = m_TowerPlus.GetComponentInChildren<SpriteRenderer>();
        m_MinustW = m_TowerMinus.GetComponentInChildren<SpriteRenderer>();

        m_bLimit = new bool[m_SynergyObj.Length];

        for (int i = 0; i < towerMng.m_LisTwNumber.Count; ++i)
        {
            m_arrTwObj[i] = towerMng.m_LisTwNumber[i];
            m_arrTwPlusPos[i] = new Vector3(m_arrTwObj[i].transform.position.x, m_arrTwObj[i].transform.position.y - 0.6f, m_arrTwObj[i].transform.position.z);
            m_arrTwMinusPos[i] = new Vector3(m_arrTwObj[i].transform.position.x, m_arrTwObj[i].transform.position.y + 0.365f, m_arrTwObj[i].transform.position.z);
        }
    }

    public void OnSynerge(int index, int background)
    {
        if (background == 1)
        {
            m_SynergyObj[index].SetActive(true);
            m_BackImg[index].sprite = m_BackGroundW;
        }
        else
        {
            m_BackImg[index].sprite = m_BackGroundY;
            m_bLimit[index] = true;
        }
        m_PlustW.sprite = m_SynergyInImg[index].sprite;
        m_MinustW.sprite = m_SynergyInImg[index].sprite;
    }

    public void offSynerge(int index)
    {
        m_bLimit[index] = false;
        m_SynergyObj[index].SetActive(false);
    }

    public void twPlusMake(int index)
    {
        GameObject twPlus = Instantiate(m_TowerPlus, m_arrTwPlusPos[index], Quaternion.identity);
        Destroy(twPlus, 3.0f);
    }

    public void twMinusMake(int index)
    {
        GameObject twMinus = Instantiate(m_TowerMinus, m_arrTwMinusPos[index], Quaternion.identity);
        Destroy(twMinus, 3.0f);
    }

    public void synergyIcon()
    {
        //캐논 데미지 증가
        if (TowerManager.m_Propertycount[5] >= 3 && m_bLimit[0] == false)
        {
            OnSynerge(0, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'E')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[5] < 3 && TowerManager.m_Propertycount[5] >= 1)
        {
            OnSynerge(0, 1);
            if (m_bLimit[0] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'E')
                        {
                            twMinusMake(i);
                        }
                    }
                }
            }
            m_bLimit[0] = false;

        }
        else if (TowerManager.m_Propertycount[5] == 0 && m_SynergyObj[0].activeSelf == true)
        {
            offSynerge(0);
        }

        //병사 체력증가       
        if (TowerManager.m_Propertycount[4] >= 3 && m_bLimit[1] == false)
        {
            OnSynerge(1, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'D')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[4] < 3 && TowerManager.m_Propertycount[4] >= 1)
        {
            OnSynerge(1, 1);
            if (m_bLimit[1] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'D')
                        {
                            twMinusMake(i);
                        }
                    }
                }
            }
            m_bLimit[1] = false;
        }
        else if (TowerManager.m_Propertycount[4] == 0 && m_SynergyObj[1].activeSelf == true)
        {
            offSynerge(1);
        }

        //보우 석궁 멀티 공격       
        if (TowerManager.m_Propertycount[1] >= 4 && TowerManager.m_Propertycount[2] >= 2 && m_bLimit[2] == false)
        {
            OnSynerge(2, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'B' || TowerManager.m_DicSynergyCount[i] == 'A')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[1] > 0 && TowerManager.m_Propertycount[1] < 4 || TowerManager.m_Propertycount[2] > 0 && TowerManager.m_Propertycount[2] < 2)
        {
            OnSynerge(2, 1);
            if (m_bLimit[2] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'B' || TowerManager.m_DicSynergyCount[i] == 'A')
                        {
                            twMinusMake(i);
                        }

                    }
                }
            }
            m_bLimit[2] = false;
        }
        else if (TowerManager.m_Propertycount[1] == 0 && TowerManager.m_Propertycount[2] == 0 && m_SynergyObj[2].activeSelf == true)
        {
            offSynerge(2);
        }

        //지속뎀지 적용      
        if (TowerManager.m_Propertycount[3] >= 2 && TowerManager.m_Propertycount[1] >= 2 && TowerManager.m_Propertycount[2] >= 2 && m_bLimit[3] == false)
        {
            OnSynerge(3, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'A' || TowerManager.m_DicSynergyCount[i] == 'B' || TowerManager.m_DicSynergyCount[i] == 'C')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[1] < 2 && TowerManager.m_Propertycount[1] > 0 ||
         TowerManager.m_Propertycount[2] < 2 && TowerManager.m_Propertycount[2] > 0 ||
         TowerManager.m_Propertycount[3] < 2 && TowerManager.m_Propertycount[3] > 0)
        {
            OnSynerge(3, 1);
            if (m_bLimit[3] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {

                        if (TowerManager.m_DicSynergyCount[i] == 'A' || TowerManager.m_DicSynergyCount[i] == 'B' || TowerManager.m_DicSynergyCount[i] == 'C')
                        {
                            twMinusMake(i);
                        }

                    }
                }
            }
            m_bLimit[3] = false;
        }
        else if (TowerManager.m_Propertycount[3] == 0 && TowerManager.m_Propertycount[1] == 0 && TowerManager.m_Propertycount[2] == 0 && m_SynergyObj[3].activeSelf == true)
        {
            offSynerge(3);
        }

        //석궁 범위 증가      
        if (TowerManager.m_Propertycount[2] >= 3 && m_bLimit[4] == false)
        {
            OnSynerge(4, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'B')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[2] < 3 && TowerManager.m_Propertycount[2] >= 1)
        {
            OnSynerge(4, 1);
            if (m_bLimit[4] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'B')
                        {
                            twMinusMake(i);
                        }
                    }
                }
            }
            m_bLimit[4] = false;
        }
        else if (TowerManager.m_Propertycount[2] == 0 && m_SynergyObj[4].activeSelf == true)
        {
            offSynerge(4);
        }

        // 마법사 슬로우   
        if (TowerManager.m_Propertycount[3] >= 3 && m_bLimit[5] == false)
        {
            OnSynerge(5, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'C')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[3] < 3 && TowerManager.m_Propertycount[3] >= 1)
        {
            OnSynerge(5, 1);
            if (m_bLimit[5] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'C')
                        {
                            twMinusMake(i);
                        }
                    }
                }
            }
            m_bLimit[5] = false;
        }
        else if (TowerManager.m_Propertycount[3] == 0 && m_SynergyObj[5].activeSelf == true)
        {
            offSynerge(5);
        }

        // 공속 증가
        if (TowerManager.m_Propertycount[1] >= 3 && m_bLimit[6] == false)
        {
            OnSynerge(6, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'A')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[1] < 3 && TowerManager.m_Propertycount[1] >= 1)
        {
            OnSynerge(6, 1);
            if (m_bLimit[6] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'A')
                        {
                            twMinusMake(i);
                        }
                    }
                }
            }
            m_bLimit[6] = false;
        }
        else if (TowerManager.m_Propertycount[1] == 0 && m_SynergyObj[6].activeSelf == true)
        {
            offSynerge(6);
        }

        //스턴      
        if (TowerManager.m_Propertycount[2] >= 2 && TowerManager.m_Propertycount[3] >= 2 && TowerManager.m_Propertycount[5] >= 2 && m_bLimit[7] == false)
        {
            OnSynerge(7, 2);
            for (int i = 0; i < m_arrTwObj.Length; ++i)
            {
                if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                {
                    if (TowerManager.m_DicSynergyCount[i] == 'E' || TowerManager.m_DicSynergyCount[i] == 'B' || TowerManager.m_DicSynergyCount[i] == 'C')
                    {
                        twPlusMake(i);
                    }
                }
            }
        }
        else if (TowerManager.m_Propertycount[2] < 2 && TowerManager.m_Propertycount[2] > 0 ||
         TowerManager.m_Propertycount[3] < 2 && TowerManager.m_Propertycount[3] > 0 ||
         TowerManager.m_Propertycount[5] < 2 && TowerManager.m_Propertycount[5] > 0)
        {
            OnSynerge(7, 1);
            if (m_bLimit[7] == true)
            {
                for (int i = 0; i < m_arrTwObj.Length; ++i)
                {
                    if (TowerManager.m_DicSynergyCount.ContainsKey(i))
                    {
                        if (TowerManager.m_DicSynergyCount[i] == 'E' || TowerManager.m_DicSynergyCount[i] == 'B' || TowerManager.m_DicSynergyCount[i] == 'C')
                        {
                            twMinusMake(i);
                        }

                    }
                }
            }
            m_bLimit[7] = false;
        }

        else if (TowerManager.m_Propertycount[2] == 0 && TowerManager.m_Propertycount[3] == 0 && TowerManager.m_Propertycount[5] == 0 && m_SynergyObj[7].activeSelf == true)
        {
            offSynerge(7);
        }
    }
}
