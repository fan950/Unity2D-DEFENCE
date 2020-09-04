using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolSynergy : Synergy, SynergyInterface
{
    public void synergyeffect()
    {
        //병사 체력증가
        if (TowerManager.m_Propertycount[4] >= 3 && m_arrCheck[1] == 0)
        {
            twobj.m_nHpUp = m_SynergyeArr[1, twobj.m_nLevel];
            m_arrCheck[1] = 1;
            twobj.Onsyner[1] = true;
        }
        else if (TowerManager.m_Propertycount[4] < 3 && m_arrCheck[1] == 1)
        {
            twobj.m_nHpUp = -m_SynergyeArr[1, twobj.m_nLevel];
            m_arrCheck[1] = 0;
            twobj.Onsyner[1] = true;
        }
    }
}
