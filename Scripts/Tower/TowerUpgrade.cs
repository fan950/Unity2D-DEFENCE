using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    enum TowerCountMax
    {
        max = 5,
        max2 = 10,
        max3 = 15,
    }

    public GameObject[] Towers;
    public GameObject baseTowers;
    public GameObject[] TowerLight = new GameObject[2];
    public GameObject[] TowerObj = new GameObject[(int)TowerCountMax.max3];

    private int m_nTwIndex = 0;
    private int m_LightIndex = 0;

    public void Init()
    {
        Towers = GameObject.FindGameObjectsWithTag("Tower");
    }

    public void Towerupgrade(GameObject a_nTwObj)
    {
        for (int i = 0; i < Towers.Length; ++i)
        {
            if (a_nTwObj.transform.name == Towers[i].name)
            {
                m_nTwIndex = i;
            }
        }

        if (TowerManager.m_nCount[TowerManager.m_nTwindex] == 3)
        {
            m_LightIndex = 0;
            for (int i = 0; i < Towers.Length; ++i)
            {
                if (TowerObj[TowerManager.m_nTwindex].name.Equals(Towers[i].transform.GetChild(0).gameObject.name) && i != m_nTwIndex)
                {
                    switch (TowerManager.m_DicSynergyCount[TowerManager.m_DicTowerNumber[Towers[i].name]])
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
                    //시너지를 위한 중복 키 제거 
                    TowerManager.m_DicSynergyCount.Remove(TowerManager.m_DicTowerNumber[Towers[i].name]);

                    Destroy(Towers[i].transform.GetChild(0).gameObject);
                    GameObject BaseTowers = Instantiate(baseTowers, Towers[i].transform.position, Quaternion.identity);
                    BaseTowers.transform.SetParent(Towers[i].transform);
                    BaseTowers.name = baseTowers.name;
                }
            }

            GameObject tower2 = Instantiate(TowerObj[TowerManager.m_nTwindex + (int)TowerCountMax.max], Towers[m_nTwIndex].transform.position, Quaternion.identity);
            tower2.transform.SetParent(Towers[m_nTwIndex].transform);
            tower2.name = TowerObj[TowerManager.m_nTwindex + (int)TowerCountMax.max].name;

            TowerManager.m_nCount[TowerManager.m_nTwindex] = 0;
            TowerManager.m_nTwindex += (int)TowerCountMax.max;
            ++TowerManager.m_nCount[TowerManager.m_nTwindex];

            if (!TowerManager.m_DicCheck.ContainsKey(tower2.name))
            {
                TowerManager.m_DicCheck.Add(tower2.name, TowerManager.m_nTwindex);
                TowerManager.m_DicTwCsGet.Add(TowerManager.m_nTwindex, tower2.GetComponent<Tower>());
                SeeUI.m_TowerObj = tower2.GetComponent<Tower>();
            }

            if (TowerManager.m_nCount[TowerManager.m_nTwindex] == 3)
            {
                m_LightIndex = 1;

                for (int i = 0; i < Towers.Length; ++i)
                {
                    if (TowerObj[TowerManager.m_nTwindex].name.Equals(Towers[i].transform.GetChild(0).gameObject.name) && i != m_nTwIndex)
                    {
                        switch (TowerManager.m_DicSynergyCount[TowerManager.m_DicTowerNumber[Towers[i].name]])
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

                        TowerManager.m_DicSynergyCount.Remove(TowerManager.m_DicTowerNumber[Towers[i].name]);

                        Destroy(Towers[i].transform.GetChild(0).gameObject);

                        GameObject BaseTowers = Instantiate(baseTowers,
                        Towers[i].transform.position,
                            Quaternion.identity);
                        BaseTowers.transform.SetParent(Towers[i].transform);
                        BaseTowers.name = baseTowers.name;
                    }

                }
                Destroy(tower2);

                GameObject tower3 = Instantiate(TowerObj[TowerManager.m_nTwindex + (int)TowerCountMax.max], Towers[m_nTwIndex].transform.position, Quaternion.identity);
                tower3.transform.SetParent(Towers[m_nTwIndex].transform);
                tower3.name = TowerObj[TowerManager.m_nTwindex + (int)TowerCountMax.max].name;

                TowerManager.m_nCount[TowerManager.m_nTwindex] = 0;
                TowerManager.m_nTwindex += (int)TowerCountMax.max;

                if (!TowerManager.m_DicCheck.ContainsKey(tower3.name))
                {
                    TowerManager.m_DicCheck.Add(tower3.name, TowerManager.m_nTwindex);
                    TowerManager.m_DicTwCsGet.Add(TowerManager.m_nTwindex, tower3.GetComponent<Tower>());
                    SeeUI.m_TowerObj = tower3.GetComponent<Tower>();
                }
            }
            GameObject TwLight = Instantiate(TowerLight[m_LightIndex], Towers[m_nTwIndex].transform.position, Quaternion.identity);
        }

    }
}