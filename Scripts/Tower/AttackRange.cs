using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [HideInInspector]
    public bool m_bPlayAttack = true;
    [HideInInspector]
    public bool m_bBullMove;
    [HideInInspector]
    public GameObject mobObj;
    [SerializeField]
    private CircleCollider2D AttackSensor;
    [SerializeField]
    private Tower TowerCs;
    [HideInInspector]
    public List<GameObject> coll = new List<GameObject>();
    [HideInInspector]
    public Mob m_Mon;

    private readonly float m_Difference = 0.3f;

    private void OutMonster(GameObject obj)
    {
        m_bPlayAttack = false;
        mobObj = null;
        m_Mon = null;
        coll.Remove(obj);
    }

    private void OnEnable()
    {
        AttackSensor.radius = TowerCs.AttackRange.x - m_Difference;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (m_Mon == null)
            {
                m_Mon = collision.GetComponent<Mob>();
            }
            m_bPlayAttack = true;
            coll.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            OutMonster(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (mobObj == null)
            {
                m_bPlayAttack = true;
                mobObj = collision.gameObject;
                m_Mon = mobObj.GetComponent<Mob>();
            }

            if (m_Mon.m_Hp <= 0)
            {
                OutMonster(collision.gameObject);
            }

        }
    }
}
