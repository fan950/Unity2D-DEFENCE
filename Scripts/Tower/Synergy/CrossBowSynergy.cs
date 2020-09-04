using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowSynergy : Synergy, SynergyInterface
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
        else if (TowerManager.m_Propertycount[1] < 4 && TowerManager.m_Propertycount[2] < 2 && m_arrCheck[2] == 1)
        {
            twobj.m_bSynergyOn = 0;
            m_arrCheck[2] = 0;
            twobj.Onsyner[2] = true;
        }
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

        //석궁 범위 증가
        if (TowerManager.m_Propertycount[2] >= 3 && m_arrCheck[4] == 0)
        {

            twobj.AttackRange.x = (twobj.AttackRange.x + m_SynergyeArr[4, twobj.m_nLevel] * 0.01f * twobj.AttackRange.x);
            twobj.AttackRange.y = (twobj.AttackRange.y + m_SynergyeArr[4, twobj.m_nLevel] * 0.01f * twobj.AttackRange.y);

            m_arrCheck[4] = 1;
            twobj.Onsyner[4] = true;
        }
        else if (TowerManager.m_Propertycount[2] < 3 && m_arrCheck[4] == 1)
        {

            twobj.AttackRange.x = (twobj.AttackRange.x - m_SynergyeArr[4, twobj.m_nLevel] * 0.01f * twobj.AttackRange.x);
            twobj.AttackRange.y = (twobj.AttackRange.y - m_SynergyeArr[4, twobj.m_nLevel] * 0.01f * twobj.AttackRange.y);

            m_arrCheck[4] = 0;
            twobj.Onsyner[4] = false;
        }

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
