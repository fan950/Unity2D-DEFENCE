using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Bullet
{
    public GameObject Bulleteff;
    public Transform BezierPoint_2;
    public Transform BezierPoint_3;
    public Vector3[] point = new Vector3[4];
    public float m_fTime = 0.3f;

    public AudioClip audioClip;

    public void SetBezier(Cannon cannon)
    {
        point = cannon.BezierPoint;        
    }

    public override void Move()
    {
        m_BulletMove.center = transform.position;
        m_nBulletStern = m_Tower.m_nStern;

        m_fTime += Time.deltaTime * 1.5f;

        transform.position = Bezier(m_fTime);

        point[3] = m_Mobmove.transform.position;
        if (m_Mob != null)
        {
            if (m_BulletMove.Intersects(m_Mob.m_Mobbounds))
            {
                m_fTime = 0;
                AudioManager.Instance.PlayEffect(audioClip);
                Destroy(Instantiate(Bulleteff, m_Mob.m_Mobbounds.center, Quaternion.identity), 3.0f);
                gameObject.SetActive(false);
                m_Mob.HpDecrease(this);
            }
        }
    }
    //배지어 곡선 구현
    Vector2 Bezier(float _fTime)
    {
        _fTime = Mathf.Clamp01(_fTime);
        float s = 1f - _fTime;
        float t2 = _fTime * _fTime;
        float u2 = s * s;
        float u3 = u2 * s;
        float t3 = t2 * _fTime;

        Vector3 result =
            (u3) * point[0] +
            (3f * u2 * _fTime) * point[1] +
            (3f * s * t2) * point[2] +
            (t3) * point[3];

        return result;
    }
}
