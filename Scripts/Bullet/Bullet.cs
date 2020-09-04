using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Bounds m_BulletMove;
    protected Mob m_Mob;
    protected Tower m_Tower;

    protected Transform m_Mobmove;

    [HideInInspector]
    public int m_nAttack;
    [HideInInspector]
    public int m_nBulletPoison;
    [HideInInspector]
    public float m_nBulletSlow;
    [HideInInspector]
    public int m_nBulletStern;

    protected Vector3 dir;

    public void SetTower(Tower tower)
    {
        if (tower == null)
        {
            return;
        }
        if (m_Tower!=tower)
        {
            m_Tower = tower;          
            m_nAttack = m_Tower.Attack;        
            m_Mob = m_Tower.playAttack.mobObj.GetComponent<Mob>();
            m_Mobmove = m_Tower.playAttack.mobObj.transform;
        }

        if (m_Mob.gameObject != m_Tower.playAttack.mobObj)
        {
            if (m_Tower.playAttack.mobObj == null)
            {
                return;
            }          
            m_Mob = m_Tower.playAttack.mobObj.GetComponent<Mob>();
            m_Mobmove = m_Tower.playAttack.mobObj.transform;
        }
    }
    void Update()
    {
        if (SeeUI.m_nPause == false)
        {                 
            if (m_Mobmove == null|| m_Mobmove.gameObject.activeSelf==false)
            {
                gameObject.SetActive(false);
                return;
            }

            Move();        
        }
    }

    public virtual void Move(){ }      
}
