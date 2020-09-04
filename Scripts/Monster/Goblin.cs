using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Mob
{
    public GameObject m_BulletObj;
    public GameObject m_Launch;
  
    public override void AttackBullet()
    {
        AudioManager.Instance.PlayEffect(m_AttackClip);
        GameObject bullet = Instantiate(m_BulletObj, m_Launch.transform.position, Quaternion.identity);

        bullet.transform.parent = m_Launch.transform;
        bullet.name = m_BulletObj.name;
        bullet.GetComponent<MobBullet>().SetMob(m_TargetObj, this);      
    }
}
