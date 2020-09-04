using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSynergy : Synergy, SynergyInterface
{
    public void synergyeffect()
    {
        //캐논 데미지 증가
        if (TowerManager.m_Propertycount[5] >= 3 && m_arrCheck[0] == 0)
        {
            twobj.SetAttack(twobj.Attack + m_SynergyeArr[0, twobj.m_nLevel]);
            m_arrCheck[0] = 1;
            twobj.Onsyner[0] = true;
        }
        else if (TowerManager.m_Propertycount[5] < 3 && m_arrCheck[0] == 1)
        {
            twobj.SetAttack(twobj.Attack - m_SynergyeArr[0, twobj.m_nLevel]);
            m_arrCheck[0] = 0;
            twobj.Onsyner[0] = true;
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
