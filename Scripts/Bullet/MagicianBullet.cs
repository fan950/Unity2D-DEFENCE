using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianBullet : Bullet
{
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
        m_nBulletSlow = m_Tower.m_nSlow;
        m_nBulletStern = m_Tower.m_nStern;

        m_BulletMove.center = transform.position;
      
        transform.position = Vector2.MoveTowards(transform.position, m_Mobmove.position, 4.0f * Time.deltaTime);       
      
        if (m_Mob != null)
        {
            if (m_BulletMove.Intersects(m_Mob.m_Mobbounds))
            {
                gameObject.SetActive(false);
                m_Mob.HpDecrease(this);
            }
        }
    }
}
