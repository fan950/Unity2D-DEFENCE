using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSynergy : Synergy
{
    public void synergyeffect()
    {
        //보우 석궁 멀티 공격
        if (TowerManager.m_Propertycount[1] >= 4 && TowerManager.m_Propertycount[2] >= 2 && m_arrCheck[2] == 0)
        {
            twobj.m_bSynergyOn = (m_SynergyeArr[2, twobj.m_nLevel]);
            m_arrCheck[2] = 1;
            twobj.Onsyner[2] = true;
        }
        else if (TowerManager.m_Propertycount[1] < 4 || TowerManager.m_Propertycount[2] < 2 && m_arrCheck[2] == 1)
        {
            twobj.m_bSynergyOn = 0;
            m_arrCheck[2] = 0;
            twobj.Onsyner[2] = true;
        }
        //지속뎀지 적용
        if (TowerManager.m_Propertycount[3] >= 2 && TowerManager.m_Propertycount[1] >= 2 && TowerManager.m_Propertycount[2] >= 2 && m_arrCheck[3] == 0)
        {
            twobj.m_nPoison = (m_SynergyeArr[3, twobj.m_nLevel]);
            m_arrCheck[3] = 1;
            twobj.Onsyner[3] = true;
        }
        else if (m_arrCheck[3] == 1)
        {
            if (TowerManager.m_Propertycount[3] < 2 || TowerManager.m_Propertycount[1] < 2 || TowerManager.m_Propertycount[2] < 2)
            {
                twobj.m_nPoison = 0;
                m_arrCheck[3] = 0;
                twobj.Onsyner[3] = false;
            }
        }
        // 공속 증가
        if (TowerManager.m_Propertycount[1] >= 3 && m_arrCheck[6] == 0)
        {
            twobj.SetAttackSpeed(twobj.AttackSpeed + m_SynergyeArr[6, twobj.m_nLevel] * 0.01f * twobj.AttackSpeed);
            m_arrCheck[6] = 1;
            twobj.Onsyner[6] = true;
        }
        else if (TowerManager.m_Propertycount[1] < 3 && m_arrCheck[6] == 1)
        {
            twobj.SetAttackSpeed(twobj.AttackSpeed - m_SynergyeArr[6, twobj.m_nLevel] * 0.01f * twobj.AttackSpeed);
            m_arrCheck[6] = 0;
            twobj.Onsyner[6] = true;
        }
    }
}
