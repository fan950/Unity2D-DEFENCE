using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Unit
{
    Action action = Action.Idle;

    private School m_School;

    private Vector3 m_Vec3;

    public GameObject m_BulletObj;
    private GameObject m_GuardObj;
    private GameObject m_Tower;
    public GameObject m_Bulletlack;
    private GameObject m_SetHpCsObj;

    public Bounds m_GuardBoun;
    private float m_AttackTime = 0.0f;

    private HPBar m_hpbar;
    private ReckoningHp m_HpCs;

    public void Init(HPBar hPBar)
    {
        m_hpbar = hPBar;
        m_vecLookat = transform.localScale;

        m_GuardBoun.center = transform.position;

        m_School = transform.parent.gameObject.GetComponent<School>();
        m_SetHp(m_School.m_Hp);
        m_SetAttack(m_School.Attack);
        m_hpbar.SetMake(gameObject, ref m_SetHpCsObj);
        m_HpCs = m_SetHpCsObj.GetComponent<ReckoningHp>();
        m_Hpobj = m_HpCs;
    }

    public void SetObj(Vector3 pos)
    {
        m_Vec3 = pos;
        action = Action.Move;
    }

    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (m_Ani.enabled == false)
            {
                m_Ani.enabled = true;
            }
            if (m_School.Onsyner[1] == true)
            {
                m_SetHp(m_Hp + m_School.m_nHpUp);
                m_School.Onsyner[1] = false;
            }

            switch (action)
            {
                case Action.Idle:
                    m_Ani.SetInteger("action", 0);
                    break;
                case Action.Move:
                    Move();
                    break;
                case Action.Attack:
                    Attacking(m_TargetObj);
                    break;
                case Action.AttackWait:
                    AttackWait();
                    break;
                case Action.Die:
                    Die();
                    break;

            }
        }
        else
        {
            m_Ani.enabled = false;
        }

    }

    public override void Move()
    {
        m_GuardBoun.center = transform.position;
        if (m_TargetObj != null)
        {
            m_TargetObj = null;
        }

        m_Ani.SetInteger("action", 1);
        transform.position = Vector3.MoveTowards(transform.position, m_Vec3, Time.deltaTime * 0.7f);
        transform.localRotation = Quaternion.identity;
        Look(m_Vec3);
        if (transform.position == m_Vec3)
        {
            action = Action.Idle;
        }

    }

    public void Attacking(GameObject m_target)
    {
        action = Action.AttackWait;
        m_Ani.SetInteger("action", 2);

        Look(m_target.transform.position);
    }

    public override void AttackWait()
    {
        m_AttackTime += Time.deltaTime;
        AnimatorStateInfo stateInfo = m_Ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime > stateInfo.length + 0.2f)
        {
            m_Ani.SetInteger("action", 0);
        }

        if (m_AttackTime > 1.0f - 0.1 * m_School.AttackSpeed)
        {
            action = Action.Attack;
            m_AttackTime = 0;
        }
    }

    //3번쨰 병사 공격 총알 만들기
    public override void AttackBullet()
    {
        AudioManager.Instance.PlayEffect(m_AttackClip);
        GameObject bullet = Instantiate(m_BulletObj, transform.position, Quaternion.identity);
        bullet.transform.parent = m_Bulletlack.transform;
        bullet.name = m_BulletObj.name;

        if (gameObject != null)
        {
            if (m_TargetObj == null)
            {
                return;
            }
            bullet.GetComponent<GuardBullet>().SetGuard(m_TargetObj.GetComponent<Mob>(), m_Attack);
        }
    }

    public override void DecreaseHp(int monster)
    {
        m_SetHp(m_nHp - monster);
        if (m_Hp <= 0)
        {
            action = Action.Die;
        }
    }
    public override void Die()
    {
        m_Ani.SetInteger("action", 3);
    }

    //현재 학교오브젝트에게 넘겨줌
    public void DieDesy()
    {
        m_School.GuardRemove(gameObject, this);
        m_SetHp(m_School.m_Hp);
        action = Action.Idle;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" && m_TargetObj == null)
        {
            if (action == Action.Move)
                return;

            m_TargetObj = collision.gameObject;
            action = Action.Attack;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" && m_TargetObj != null)
        {
            if (action == Action.Move)
                return;

            m_TargetObj = null;
            action = Action.Idle;
        }
    }

}
