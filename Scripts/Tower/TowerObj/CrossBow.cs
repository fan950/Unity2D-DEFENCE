using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : Tower
{
    private GameObject bullet;
    public CrossBowSynergy crossBowSynergy;

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
                    bullet = Instantiate(m_BulletObj);
                    bullet.name = m_BulletObj.name;
                    bullet.transform.parent = transform;
                    bullet.SetActive(false);
                    bulletBox.Add(bullet);
                }
            }

            for (int i = 0; i < playAttack.coll.Count; ++i)
            {
                bulletBox[i].SetActive(true);
                bulletBox[i].transform.position = m_Launch.transform.position;

                bulletBox[i].GetComponent<Bullet>().SetTower(this);
                bulletBox[i].GetComponent<CrossbowBullet>().SetMulti(playAttack.coll[i]);
            }

        }
        else
        {
            if (bullet == null)
            {
                bullet = Instantiate(m_BulletObj, m_Launch.transform.position, Quaternion.identity);
                bullet.transform.parent = transform;
                bullet.GetComponent<Bullet>().SetTower(this);
                bullet.name = m_BulletObj.name;
            }
            else
            {
                bullet.SetActive(true);
                bullet.transform.position = m_Launch.transform.position;
                bullet.GetComponent<Bullet>().SetTower(this);
            }
        }
    }

    public override void synergyeffect()
    {
        crossBowSynergy.synergyeffect();
    }
}
