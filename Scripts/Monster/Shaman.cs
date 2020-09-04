using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : Mob
{
    private float m_fHealTime;
    public GameObject m_HealObj;

    public override void AttackAction()
    {
        Move();
    }
    public override void Move()
    {
        if (m_Hp < DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "HP"))
        {
            m_fHealTime += Time.deltaTime;
            if (m_fHealTime > 3.0f)
            {
                m_fHealTime = 0;
                m_SetHp(m_Hp + 50);
                Destroy(Instantiate(m_HealObj, transform.position, Quaternion.identity, transform), 2.0f);
            }
        }
             

        transform.position = Vector2.MoveTowards(transform.position, m_arrPoint[m_nWayIndex].position, Time.deltaTime * m_Speed);
        transform.localRotation = Quaternion.identity;
        Look(m_arrPoint[m_nWayIndex].transform.position);

        if (transform.position == m_arrPoint[m_nWayIndex].position)
        {
            ++m_nWayIndex;
            if (m_arrPoint.Length <= m_nWayIndex)
            {
                m_SetHp(DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "HP"));
                --m_MobMng.m_mLife;
                m_nWayIndex =1;
                m_MobMng.PlaseDie(gameObject, this);
            }
        }
        m_Mobbounds.center = transform.position;
    }
}
