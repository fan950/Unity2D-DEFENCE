using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject Hpbar;
    private int m_nUnitCount;
    private Vector3 vecBottom;

    public void SetMake(GameObject obj, ref GameObject hpobj)
    {
        GameObject hpbar = Instantiate(Hpbar);
        hpbar.transform.SetParent(transform);
        hpbar.GetComponent<ReckoningHp>().SetUnit(obj);
        hpobj = hpbar;
        hpbar.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
    }
    public void SetLive(Unit unit)
    {
        unit.m_Hpobj.m_HpImg.fillAmount = 1;
        unit.m_Hpobj.gameObject.SetActive(true);
    }
    public void SetDie(Unit unit, School school)
    {
        unit.m_Hpobj.gameObject.transform.position = Camera.main.WorldToScreenPoint(school.m_Launch.transform.position);
        unit.m_Hpobj.gameObject.SetActive(false);
    }
}
