using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : Tower
{
    [HideInInspector]
    public Transform m_DieWait
        ;
    private int m_nHp;
    public int m_Hp { get { return m_nHp; } }
    public void m_SetHp(int hp) { m_nHp = hp; }
    int m_nGuardCount = 0;

    public SchoolSynergy schoolSynergy;
    public HPBar m_Hpbar;
    private GameObject[] m_arrGuard = new GameObject[2];
    private Guard[] m_arrGuardCs = new Guard[2];
    private Vector3 m_GuardPos;

    public void Start()
    {
        m_Hpbar = UIAdd.Get<HPBar>(UIType.HpBarUI);
        m_nHp = DataMng.Get(TableType.TowerTable).ToI(Index, "HP");

        m_GuardPos = m_Launch.transform.position;
    }

    public override void Attackupade()
    {
        if (m_nGuardCount < 2)
        {
            m_fAttackTime += Time.deltaTime;

            if (m_fAttackTime > 4.0f)
            {
                m_fAttackTime = 0.0f;

                m_Ani.SetTrigger("attack");
            }
        }
        synergyeffect();
    }
    //스쿨 병사 생성
    public void Guard()
    {
        if (m_arrGuard[m_nGuardCount] == null)
        {
            m_arrGuard[m_nGuardCount] = Instantiate(m_BulletObj, m_GuardPos, Quaternion.identity);
            m_arrGuard[m_nGuardCount].transform.parent = transform;
            m_arrGuard[m_nGuardCount].name = m_BulletObj.name;
            m_arrGuardCs[m_nGuardCount] = m_arrGuard[m_nGuardCount].GetComponent<Guard>();
            m_arrGuardCs[m_nGuardCount].Init(m_Hpbar);
            ++m_nGuardCount;
        }
        else
        {
            //병사가 두명이 있기 때문에 둘중에 어떤것인지 찾아야한다.
            for (int i = 0; i < m_arrGuard.Length; ++i)
            {
                if (m_arrGuard[i].activeSelf == false)
                {
                    m_arrGuard[i].SetActive(true);
                    m_arrGuard[i].transform.position = m_Launch.transform.position;
                    m_Hpbar.SetLive(m_arrGuardCs[i]);
                    ++m_nGuardCount;
                    break;
                }
            }

        }
    }

    public override void synergyeffect()
    {
        schoolSynergy.synergyeffect();
    }

    public void GuardRemove(GameObject guardObj, Guard guard)
    {
        guardObj.SetActive(false);
        guardObj.transform.position = m_GuardPos;

        m_Hpbar.SetDie(guard, this);
        --m_nGuardCount;
    }

}
