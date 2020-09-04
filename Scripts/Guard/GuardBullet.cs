using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBullet : MonoBehaviour
{
    private Mob m_Monster;
    public Bounds m_BulletBounds;
    private int m_Attack;

    public void SetGuard(Mob monster, int attack)
    {
        m_Monster = monster;
        m_BulletBounds.center = transform.position;
        m_Attack = attack;
    }

    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (m_Monster != null)
            {
                m_BulletBounds.center = transform.position;
                transform.position = Vector2.MoveTowards(transform.position, m_Monster.gameObject.transform.position, Time.deltaTime * 2.0f);


                Vector3 dir = m_Monster.gameObject.transform.position - transform.position;
                dir.Normalize();
                // 타겟 방향으로 회전
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                if (m_BulletBounds.Intersects(m_Monster.m_Mobbounds))
                {
                    Destroy(gameObject);
                    m_Monster.gameObject.transform.SendMessage("DecreaseHp", m_Attack, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
