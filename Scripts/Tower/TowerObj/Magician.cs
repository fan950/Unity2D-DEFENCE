using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : Tower
{
    private GameObject bullet;
    public MagicianSynergy magicianSynergy;

    public Vector3[] BulletPoint = new Vector3[4];

    public override void MakeBullet()
    {
        AudioManager.Instance.PlayEffect(ShotSound);

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

    public override void synergyeffect()
    {
        magicianSynergy.synergyeffect();
    }
}
