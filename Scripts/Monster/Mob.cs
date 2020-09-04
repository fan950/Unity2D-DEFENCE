using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Unit
{
    protected Action action = Action.Move;

    [HideInInspector]
    public MonsterManager m_MobMng;
    [HideInInspector]
    protected int m_nWayIndex = 2;
    [HideInInspector]
    public Transform[] m_arrPoint;

    public AudioClip m_DieClip;
    public AudioClip m_GoalClip;

    protected float m_nSpeed;
    protected string m_sName;
    protected int m_nPrice;
    public int m_nIndex;
    public float m_Speed { get { return m_nSpeed; } }
    public void m_SetSpeed(float speed) { m_nSpeed = speed; }
    protected float _AttackTime = 0;

    public Bounds m_Mobbounds;

    protected GameObject m_poisonObj;
    protected GameObject m_slowObj;
    protected GameObject m_stunObj;
    public GameObject m_Poisonobj;
    public GameObject m_SlowObj;
    public GameObject m_StunObj;


    protected bool m_bPoisonOn = false;
    protected bool m_bSlowOn = true;
    protected bool m_bStunOn = true;

    protected float m_fBaseSpeed;
    protected float m_fSlowTime;
    protected float m_fStunTime;
    protected float m_fPoisonTime;
    protected int m_nPoison = 0;
    protected float m_fSlow = 0;
    protected int m_nStun = 0;

    public void Init(GameObject wayPointObj)
    {
        m_nAttack = DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "ATTACK");
        m_nSpeed = DataMng.Get(TableType.MonsterTable).ToF(m_nIndex, "SPEED");
        m_nHp = DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "HP");
        m_nPrice = DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "Price");

        m_arrPoint = wayPointObj.GetComponentsInChildren<Transform>();

        m_MobMng = transform.root.gameObject.GetComponent<MonsterManager>();
        m_vecLookat = transform.localScale;
        m_Mobbounds.center = transform.position;

        m_fBaseSpeed = m_Speed;
    }

    public void hpcatch(ReckoningHp hpobj)
    {
        m_Hpobj = hpobj;
    }

    public void Clear()
    {
        if (m_StunObj != null)
        {
            Destroy(m_stunObj);
        }
        if (m_slowObj != null)
        {
            Destroy(m_slowObj);
        }
        if (m_poisonObj != null)
        {
            Destroy(m_poisonObj);
        }

        m_bPoisonOn = false;
        m_bSlowOn = true;
        m_bStunOn = true;

        m_fSlowTime = 0;
        m_fStunTime = 0;
        m_nPoison = 0;
        m_fSlow = 0;
        m_nStun = 0;
        m_SetSpeed(m_fBaseSpeed);

    }
   
    public void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (m_Ani.enabled == false)
            {
                m_Ani.enabled = true;
            }

            switch (action)
            {             
                case Action.Move:
                    Move();
                    break;
                case Action.Attack:
                    AttackAction();
                    break;
                case Action.Die:
                    Die();
                    break;
                case Action.AttackWait:
                    AttackWait();
                    break;
            }

            if (m_nPoison!=0)
            {
                m_fPoisonTime += Time.deltaTime;

                if (m_fPoisonTime >= 0.2f)
                {
                    m_SetHp(m_Hp - m_nPoison);
                    m_fPoisonTime = 0;

                    if (m_Hp <= 0)
                    {
                        action = Action.Die;
                    }
                }
            }

            //슬로우 시간 지정
            if (m_fSlow != 0)
            {
                m_fSlowTime += Time.deltaTime;

                if (m_fSlowTime > 2.0f)
                {
                    m_SetSpeed(m_fBaseSpeed);
                    m_fSlowTime = 0;
                    m_bSlowOn = true;

                    Destroy(m_slowObj);
                }
            }


            if (m_nStun != 0)
            {
                m_fStunTime += Time.deltaTime;
                if (m_fStunTime > m_nStun)
                {
                    m_SetSpeed(m_fBaseSpeed);
                    m_fStunTime = 0;
                    m_bStunOn = true;

                    Destroy(m_stunObj);
                }
            }
        }
        else
        {
            m_Ani.enabled = false;
        }
    }

    public override void Move()
    {
        m_Ani.SetInteger("mob_action", 1);
        Look(m_arrPoint[m_nWayIndex].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, m_arrPoint[m_nWayIndex].position, Time.deltaTime * m_Speed);
        transform.localRotation = Quaternion.identity;

        if (transform.position == m_arrPoint[m_nWayIndex].position)
        {
            ++m_nWayIndex;
            if (m_arrPoint.Length <= m_nWayIndex)
            {
                AudioManager.Instance.PlayEffect(m_GoalClip);
                --m_MobMng.m_mLife;
                m_nWayIndex = 1;
                m_SetHp(DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "HP"));
                m_MobMng.PlaseDie(gameObject, this);
            }
        }
        m_Mobbounds.center = transform.position;

    }

    public virtual void AttackAction()
    {
        if (m_TargetObj.activeSelf)
        {
            action = Action.AttackWait;
            m_Ani.SetInteger("mob_action", 2);
            Look(m_TargetObj.transform.position);
        }
        else
        {
            action = Action.Move;
        }
    }

    public override void AttackWait()
    {
        _AttackTime += Time.deltaTime;
        AnimatorStateInfo stateInfo = m_Ani.GetCurrentAnimatorStateInfo(0);
      
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime > stateInfo.length+0.2f)
        {
            m_Ani.SetInteger("mob_action", 0);
        }

        if (_AttackTime > 1.0f)
        {
            action = Action.Attack;
            _AttackTime = 0;
        }
    }

    //한번만 실행하기 위해서
    public void DieSound()
    {
        TowerManager.m_nGold += DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "Price");
        AudioManager.Instance.PlayEffect(m_DieClip);
    }

    public override void Die()
    {
        m_Ani.SetInteger("mob_action", 3);
        AnimatorStateInfo stateInfo = m_Ani.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Die") && stateInfo.normalizedTime > stateInfo.length)
        {
            m_SetHp(DataMng.Get(TableType.MonsterTable).ToI(m_nIndex, "HP"));
            Clear();
            m_MobMng.PlaseDie(gameObject, this);
            m_nWayIndex = 1;
            action = Action.Move;
        }
    }

    //총알과의 감소
    public void HpDecrease(Bullet TwBullet)
    {
        m_SetHp(m_Hp - TwBullet.m_nAttack);
        m_nPoison = TwBullet.m_nBulletPoison;

        //독뎀
        if (TwBullet.m_nBulletPoison != 0)
        {
            m_bPoisonOn = true;
            m_nPoison = TwBullet.m_nBulletPoison;
            if (m_poisonObj == null)
            {
                m_poisonObj = Instantiate(m_Poisonobj, gameObject.transform.position, Quaternion.identity);
                m_poisonObj.name = m_Poisonobj.name;
                m_poisonObj.transform.parent = gameObject.transform;
            }
        }
        else if (TwBullet.m_nBulletPoison == 0 )
        {
            m_bPoisonOn = false;
            if (m_poisonObj != null)
            {
                Destroy(m_poisonObj);
            }
        }

        //슬로우
        if (TwBullet.m_nBulletSlow != 0)
        {
            if (m_bSlowOn == true)
            {
                m_SetSpeed(m_Speed - (TwBullet.m_nBulletSlow * 0.01f * m_Speed));

                m_bSlowOn = false;
                m_fSlowTime = 0;
                m_fSlow = TwBullet.m_nBulletSlow * 0.01f * m_Speed;
                if (m_slowObj == null)
                {
                    m_slowObj = Instantiate(m_SlowObj, transform.position, Quaternion.identity);
                    m_slowObj.name = m_SlowObj.name;
                    m_slowObj.transform.parent = transform;
                }

            }
            if (m_fSlow < TwBullet.m_nBulletSlow)
            {
                m_SetSpeed(m_Speed + m_fSlow - (TwBullet.m_nBulletSlow * 0.01f * m_Speed));
                m_fSlow = TwBullet.m_nBulletSlow * 0.01f * m_Speed;
            }
        }

        //스턴
        if (TwBullet.m_nBulletStern != 0)
        {
            if (m_bStunOn == true)
            {
                m_SetSpeed(0);
                m_bStunOn = false;
                m_nStun = TwBullet.m_nBulletStern;
                m_fStunTime = 0;
                if (m_stunObj == null)
                {
                    m_stunObj = Instantiate(m_StunObj, transform.position, Quaternion.identity);
                    m_stunObj.name = m_StunObj.name;
                    m_stunObj.transform.parent = gameObject.transform;
                }
            }
        }
        if (m_Hp <= 0)
        {         
            action = Action.Die;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_TargetObj == null || collision.gameObject.activeSelf == false)
        {
            if (collision.tag.Equals("Guard"))
            {
                m_TargetObj = collision.gameObject;
                action = Action.Attack;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_TargetObj != null || collision.gameObject.activeSelf == true)
        {
            if (collision.tag.Equals("Guard") && m_TargetObj != null)
            {
                m_TargetObj = null;
                action = Action.Move;
            }
        }

    }

    public override void DecreaseHp(int damage)
    {
        m_SetHp(m_Hp - damage);
        if (m_Hp <= 0)
        {
            action = Action.Die;
        }

    }
}
