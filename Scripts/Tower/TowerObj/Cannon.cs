using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Tower
{
    private GameObject bullet;
    public CannonSynergy cannonSynergy;
    public Transform []BezierPointPos=new Transform[2];

    [HideInInspector]
    public Vector3[] BezierPoint = new Vector3[4];

    public override void MakeBullet()
    {
        AudioManager.Instance.PlayEffect(ShotSound);

        BezierPoint[0] = m_Launch.transform.position;
        BezierPoint[1] = BezierPointPos[0].position;
        BezierPoint[2] = BezierPointPos[1].position;
        if (playAttack.mobObj != null)
        {
            BezierPoint[3] = playAttack.mobObj.transform.position;
        }
        else
        {
            return;
        }

   
        //오브젝트 풀링
       if (bullet == null)
       {
           bullet = Instantiate(m_BulletObj, BezierPoint[0], Quaternion.identity);
           bullet.transform.parent = transform;
           bullet.GetComponent<Bullet>().SetTower(this);
           bullet.GetComponent<CannonBullet>().SetBezier(this);
           bullet.name = m_BulletObj.name;
       }
       else
       {
         bullet.SetActive(true);
         bullet.transform.position =m_Launch.transform.position;
         bullet.GetComponent<Bullet>().SetTower(this);
         bullet.GetComponent<CannonBullet>().SetBezier(this);
       }
    }

    public override void synergyeffect()
    {
        cannonSynergy.synergyeffect();
    }
}
