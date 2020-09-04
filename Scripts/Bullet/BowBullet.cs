using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBullet : Bullet
{
    private Mob m_SubMonster;
    private Transform m_MonsterMove;

    public void SetMulti(GameObject mon)
    {
        m_MonsterMove = mon.transform;
        m_SubMonster = mon.GetComponent<Mob>();
    }

    public override void Move()
    {
        if (m_Tower.Onsyner[3] == true)
        {
            m_nBulletPoison = m_Tower.m_nPoison;
        }
        else
        {
            m_nBulletPoison = 0;
        }
        if (TowerManager.m_Propertycount[1] >= 4 && TowerManager.m_Propertycount[2] >= 2)
        {
            if (m_MonsterMove == null)
            {
                gameObject.SetActive(false);
                return;
            }

            MonsterCs(m_SubMonster, m_MonsterMove);
        }
        else
        {
            MonsterCs(m_Mob,m_Mobmove);
        }
    }

    public void MonsterCs(Mob mon,Transform monMove)
    {
        m_BulletMove.center = transform.position;

        if (mon != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, monMove.position, 3.0f * Time.deltaTime);
            dir = monMove.position - transform.position;
            dir.Normalize();
            // 타겟 방향으로 회전
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (m_BulletMove.Intersects(mon.m_Mobbounds))
            {
                gameObject.SetActive(false);
                mon.HpDecrease(this);
            }
        }
    }
}
