using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowBullet : Bullet
{
    private Mob m_SubMob;
    private Transform m_MonsterMove;
    public GameObject Bulleteff;

    public void SetMulti(GameObject mon)
    {
        m_MonsterMove = mon.transform;
        m_SubMob = mon.GetComponent<Mob>();
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

        m_nBulletStern = m_Tower.m_nStern;

        if (TowerManager.m_Propertycount[1] >= 4 && TowerManager.m_Propertycount[2] >= 2)
        {
            if (m_MonsterMove == null)
            {
                gameObject.SetActive(false);
                return;
            }

            MonsterCs(m_SubMob, m_MonsterMove);
        }
        else
        {
            MonsterCs(m_Mob, m_Mobmove);
        }
    }

    public void MonsterCs(Mob mon,Transform mobmove)
    {
        m_BulletMove.center = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, mobmove.position, 4.0f * Time.deltaTime);
        dir = mobmove.position - transform.position;

        dir.Normalize();
        // 타겟 방향으로 회전함
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (mon != null)
        {
            if (m_BulletMove.Intersects(mon.m_Mobbounds))
            {
                gameObject.SetActive(false);
                Destroy(Instantiate(Bulleteff, mon.transform.position, Quaternion.identity), 3.0f);
                mon.HpDecrease(this);
            }
        }
    }
}
