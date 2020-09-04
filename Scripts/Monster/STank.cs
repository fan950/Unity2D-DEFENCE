using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STank : Mob
{
    public GameObject m_BulletObj;
    public GameObject m_LaunchObj;
    private GameObject m_Bullet;

    public override void AttackBullet()
    {
        AudioManager.Instance.PlayEffect(m_AttackClip);
        m_Bullet = Instantiate(m_BulletObj, m_LaunchObj.transform.position, Quaternion.identity);
        m_Bullet.transform.parent = m_LaunchObj.transform;
        m_Bullet.name = m_BulletObj.name;
        m_Bullet.GetComponent<MobBullet>().SetMob(m_TargetObj, this);
    }

}
