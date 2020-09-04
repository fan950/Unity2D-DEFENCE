using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReckoningHp : MonoBehaviour
{
    private GameObject m_Unitobj;
    private Unit m_UnitCs;
    private float m_fAllHp;
    public Image m_HpImg;
    private Vector3 m_vecBottom;

    public void SetUnit(GameObject obj)
    {
        m_Unitobj = obj;
        m_UnitCs = obj.GetComponent<Unit>();

        m_fAllHp = m_UnitCs.m_Hp;
    }


    private void Update()
    {
        if (m_Unitobj == null)
        {
            Destroy(gameObject);
            return;
        }

        m_HpImg.fillAmount = (m_UnitCs.m_Hp * m_fAllHp) / (m_fAllHp * m_fAllHp);
        m_vecBottom = m_Unitobj.transform.position;
        m_vecBottom.y -= 0.5f;
        transform.position = Camera.main.WorldToScreenPoint(m_vecBottom);
    }
}
