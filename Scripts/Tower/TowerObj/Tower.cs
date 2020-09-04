using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    public char m_cProperty;
    [HideInInspector]
    public int m_bSynergyOn = 0;
    [HideInInspector]
    public int m_nPoison = 0;
    [HideInInspector]
    public int m_nAttackRange = 0;
    [HideInInspector]
    public float m_nSlow = 0;
    [HideInInspector]
    public int m_nStern = 0;
    [HideInInspector]
    public int m_nHpUp = 0;
    [HideInInspector]
    public int m_nLevel;
    [HideInInspector]
    public Vector3 AttackRange = new Vector3(1.8f, 1.8f, 1);

    protected int m_nAttack;
    protected float m_nAttackSpeed;
    protected string m_sName;
    protected int m_nPrice;

    public int Index;

    protected float m_fAttackTime = 1.0f;

    public int Attack { get { return m_nAttack; } }
    public float AttackSpeed { get { return m_nAttackSpeed; } }

    public void SetAttack(int attack) { m_nAttack = attack; }
    public void SetAttackSpeed(float attackspeed) { m_nAttackSpeed = attackspeed; }

    public GameObject m_BulletObj;
    public GameObject m_Launch;
    public CircleCollider2D AttackSensor;
    public AttackRange playAttack;
    protected Animator m_Ani;
  
    public bool[] Onsyner;

    public AudioClip ShotSound;
    public List<GameObject> bulletBox = new List<GameObject>();

    private void Awake()
    {
        m_nAttack = DataMng.Get(TableType.TowerTable).ToI(Index, "ATTACK");
        m_nAttackSpeed = DataMng.Get(TableType.TowerTable).ToF(Index, "ATTACKSPEED");
        m_cProperty = DataMng.Get(TableType.TowerTable).ToC(Index, "Property");
        m_nLevel = DataMng.Get(TableType.TowerTable).ToI(Index, "Level");
        m_sName = DataMng.Get(TableType.TowerTable).ToS(Index, "NAME");
        m_nPrice = DataMng.Get(TableType.TowerTable).ToI(Index, "Price");

        Onsyner = new bool[8];
        if (!TowerManager.m_DicTowerGold.ContainsKey(m_sName))
        {
            TowerManager.m_DicTowerGold.Add(m_sName, m_nPrice);
        }
        if (!TowerManager.m_DicTowerProperty.ContainsKey(m_sName))
        {
            TowerManager.m_DicTowerProperty.Add(m_sName, m_cProperty);
        }

        m_Ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            Attackupade();
        }
    }

    public virtual void Attackupade()
    {

        m_fAttackTime += Time.deltaTime;
        if (1.0f - (1.0f - (1.0f / AttackSpeed)) < m_fAttackTime)
        {
            m_fAttackTime = 0.0f;

            if (playAttack.m_Mon == null)
            {
                return;
            }

            if (playAttack.m_bPlayAttack == true)
            {
                m_Ani.SetTrigger("attack");
            }

        }
        synergyeffect();
    }

    public virtual void MakeBullet() { }
    public virtual void synergyeffect() { }
}
