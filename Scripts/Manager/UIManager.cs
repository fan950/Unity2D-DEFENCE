using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public TowerBtn m_TwBtn;
    [HideInInspector]
    public SeeUI m_SeeUi; 
    [HideInInspector]
    public SynergyUI m_SynergyUI;
    [HideInInspector]
    public Slot m_SlotUI;

    public MonsterManager m_MobMng;
    public TowerManager m_TwMng;

    public void Awake()
    {
       UIAdd.DicClear();

        foreach (UIType t in Enum.GetValues(typeof(UIType)))
            UIAdd.Load<Component>(t);

        m_TwBtn = UIAdd.Get<TowerBtn>(UIType.TowerBtn);
        m_SeeUi = UIAdd.Get<SeeUI>(UIType.SeeUI);
        m_SynergyUI = UIAdd.Get<SynergyUI>(UIType.SynergyUI);
    }
    private void Start()
    {
        if (SceneMng.m_uIScreen != null)
        {
            SceneMng.m_uIScreen.Execute(false);
        }
        m_SeeUi.Init();

        m_SynergyUI.Init(m_TwMng);
    }
    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            m_SeeUi.ChangeRound(m_MobMng.m_nRound);

            m_SeeUi.ChangeLife(m_MobMng.m_mLife);

            m_SeeUi.ShowGold();

            m_SeeUi.ChangeName();
           //움직이는것은 시너지 오프가 아니기 때문에
           if (m_TwBtn.m_SMABtn.activeSelf == false)
           {
              m_SynergyUI.synergyIcon();
            }

            if (m_MobMng.m_Boss != null)
            {
                m_SeeUi.Comple(m_MobMng.m_Boss.m_Hp);
            }
        }

    }

}
