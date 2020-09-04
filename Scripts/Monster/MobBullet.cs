using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBullet : MonoBehaviour
{
    private GameObject m_target;
    private Guard m_TargetBun;
    public Bounds ButPos;
    private Mob m_Monster;
    public GameObject m_EffBulletObj;
    public AudioClip m_AudioClip;

    public void SetMob(GameObject target,Mob mob)
    {
        if (target != null)
        {
            m_target = target;
            m_TargetBun = target.GetComponent<Guard>();
            m_Monster = mob;
        }
    }

    void Start()
    {
        ButPos.center = transform.position;
    }

    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (m_target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position, 3.0f * Time.deltaTime);
                ButPos.center = transform.position;
                if (ButPos.Intersects(m_TargetBun.m_GuardBoun))
                {
                    if (m_AudioClip != null)
                    {
                        AudioManager.Instance.PlayEffect(m_AudioClip);
                    }
                    Destroy(gameObject);
                   m_TargetBun.DecreaseHp(m_Monster.m_Attack);
                    Destroy(Instantiate(m_EffBulletObj, m_target.transform.position,Quaternion.identity), 2.0f);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
