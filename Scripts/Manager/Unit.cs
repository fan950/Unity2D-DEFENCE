using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum Action
    {
        Move,
        Attack,
        Die,
        Idle,
        AttackWait
    }
    [HideInInspector]
    public GameObject m_TargetObj;
    [HideInInspector]
    public ReckoningHp m_Hpobj;
    public Animator m_Ani;
  
    public AudioClip m_AttackClip;

    protected Vector3 m_vecLookat;
    protected bool m_bFixLook = true;

    protected int m_nAttack;
    protected int m_nHp;
    public int m_Attack { get { return m_nAttack; } }
    public int m_Hp { get { return m_nHp; } }
    public void m_SetAttack(int attack) { m_nAttack = attack; }
    public void m_SetHp(int hp) { m_nHp = hp; }

    public void Look(Vector3 Vec3)
    {
        if (transform.position.x > Vec3.x)
        {
            if (m_bFixLook == true)
            {
                transform.localScale = new Vector3(m_vecLookat.x * -1, transform.localScale.y, transform.localScale.z);
                m_bFixLook = false;
            }
        }
        else
        {
            if (m_bFixLook == false)
            {
                transform.localScale = new Vector3(m_vecLookat.x, transform.localScale.y, transform.localScale.z);
                m_bFixLook = true;
            }
        }
    }
    public virtual void Move(){}

    public void AttackMessage()
    {
        if (m_TargetObj != null)
        {
            AudioManager.Instance.PlayEffect(m_AttackClip);
            m_TargetObj.transform.SendMessage("DecreaseHp", m_Attack, SendMessageOptions.DontRequireReceiver);
        }
    }

    public virtual void Die() { }
    public virtual void DecreaseHp(int damage) { }
    public virtual void AttackBullet(){ }
    public virtual void AttackWait(){ }
}
