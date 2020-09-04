using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Tower
{
    public BowSynergy bowSynergy;
    private GameObject m_Bullet;
    public GameObject Charter;

    public override void MakeBullet()
    {
        AudioManager.Instance.PlayEffect(ShotSound);

        //멀티샷
        if (TowerManager.m_Propertycount[1] >= 4 && TowerManager.m_Propertycount[2] >= 2)
        {
            if (bulletBox.Count < playAttack.coll.Count)
            {
                for (int i = 0; i < 5; ++i)
                {
                    m_Bullet = Instantiate(m_BulletObj);
                    m_Bullet.transform.parent = transform;
                    m_Bullet.name = m_BulletObj.name;
                    m_Bullet.SetActive(false);
                    bulletBox.Add(m_Bullet);
                }
            }

            for (int i = 0; i < playAttack.coll.Count; ++i)
            {
                bulletBox[i].SetActive(true);
                bulletBox[i].transform.position = m_Launch.transform.position;

                bulletBox[i].GetComponent<Bullet>().SetTower(this);
                bulletBox[i].GetComponent<BowBullet>().SetMulti(playAttack.coll[i]);
            }

        }
        else
        {
            if (m_Bullet == null)
            {
                m_Bullet = Instantiate(m_BulletObj, m_Launch.transform.position, Quaternion.identity);
                m_Bullet.transform.parent = transform;
                m_Bullet.GetComponent<Bullet>().SetTower(this);
                m_Bullet.name = m_BulletObj.name;
            }
            else
            {
                m_Bullet.SetActive(true);
                m_Bullet.transform.position =  m_Launch.transform.position;
                m_Bullet.GetComponent<Bullet>().SetTower(this);
            }
        }
    }
    public override void synergyeffect()
    {
        bowSynergy.synergyeffect();
    }

}
