using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSynergy : Synergy, SynergyInterface
{

    public void synergyeffect()
    {
        //지속뎀지 적용
        if (TowerManager.m_Propertycount[1] >= 2 && TowerManager.m_Propertycount[2] >= 2 && TowerManager.m_Propertycount[3] >= 2 && m_arrCheck[3] == 0)
        {
            twobj.m_nPoison = m_SynergyeArr[3, twobj.m_nLevel];
            m_arrCheck[3] = 1;
            twobj.Onsyner[3] = true;
        }
        else if (m_arrCheck[3] == 1)
        {
            if (TowerManager.m_Propertycount[1] < 2 || TowerManager.m_Propertycount[2] < 2 || TowerManager.m_Propertycount[3] < 2)
            {
                twobj.m_nPoison = 0;
                m_arrCheck[3] = 0;
                twobj.Onsyner[3] = false;
            }
        }

        // 마법사 슬로우
        if (TowerManager.m_Propertycount[3] >= 3 && m_arrCheck[5] == 0)
        {
            twobj.m_nSlow = m_SynergyeArr[5, twobj.m_nLevel];
            m_arrCheck[5] = 1;
            twobj.Onsyner[5] = true;

        }
        else if (TowerManager.m_Propertycount[3] < 3 && m_arrCheck[5] == 1)
        {
            twobj.m_nSlow = 0;
            m_arrCheck[5] = 0;
            twobj.Onsyner[5] = false;

        }

        //스턴
        if (TowerManager.m_Propertycount[2] >= 2 && TowerManager.m_Propertycount[3] >= 2 && TowerManager.m_Propertycount[5] >= 2 && m_arrCheck[7] == 0)
        {
            twobj.m_nStern = (m_SynergyeArr[7, twobj.m_nLevel]);
            m_arrCheck[7] = 1;
            twobj.Onsyner[7] = true;
        }
        else if (m_arrCheck[7] == 1)
        {
            if (TowerManager.m_Propertycount[2] < 2 || TowerManager.m_Propertycount[3] < 2 || TowerManager.m_Propertycount[5] < 2)
            {
                twobj.m_nStern = 0;
                m_arrCheck[7] = 0;
                twobj.Onsyner[7] = false;
            }
        }

    }
}
