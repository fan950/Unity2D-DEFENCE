using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface SynergyInterface
{
    void synergyeffect();
}

public class Synergy : MonoBehaviour
{ 
    protected enum ArrLength
    {
        porperty = 8,
        Level = 3,
    }
    public Tower twobj;

    [HideInInspector]
    public int[] m_arrCheck = new int[8];
    protected int[,] m_SynergyeArr = new int[(int)ArrLength.porperty, (int)ArrLength.Level];
    public bool m_SynergyOn = false;

    private void Start()
    {
        //효과 적용
        for (int i = 0; i < (int)ArrLength.porperty; ++i)
        {
            m_SynergyeArr[i, 0] = DataMng.Get(TableType.SynergyTable).ToI(i + 1, "effect1");
            m_SynergyeArr[i, 1] = DataMng.Get(TableType.SynergyTable).ToI(i + 1, "effect2");
            m_SynergyeArr[i, 2] = DataMng.Get(TableType.SynergyTable).ToI(i + 1, "effect3");
        }
    }
}
