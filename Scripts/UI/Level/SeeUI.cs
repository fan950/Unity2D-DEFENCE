using UnityEngine;
using UnityEngine.UI;

public class SeeUI : MonoBehaviour
{
    public Text m_GlodTxt;
    public Text m_LifeTxt;
    public Text m_RoundTxt;

    public Text m_NameTxt;
    public Text m_AttackTxt;
    public Text m_AttackSpeedTxt;

    private float m_Attkspeed;

    public static Tower m_TowerObj;

    public Image m_TimeImg;

    public GameObject m_Complet;
    public GameObject m_Defeat;
    private GameObject m_DefeatObj;
    private GameObject m_CompletObj;

    public GameObject m_Pause;

    public GameObject m_TimeOn;

    public AudioClip m_DEFEATClip;
    public AudioClip m_CompleClip;

    public static bool m_nPause = false;

    public void Init()
    {
        m_NameTxt.text = string.Empty;
        m_AttackTxt.text = string.Empty;
        m_AttackSpeedTxt.text = string.Empty;
    }
    public void pause()
    {
        m_nPause = true;
        GameObject pauseSence = Instantiate(m_Pause);
    }

    public void ShowGold()
    {
        m_GlodTxt.text = TowerManager.m_nGold.ToString();
    }

    public void ChangeRound(int a_round)
    {
        m_RoundTxt.text = a_round.ToString();
    }

    public void ChangeLife(int a)
    {
        m_LifeTxt.text = a.ToString();
        if (a <= 0 && m_DefeatObj == null)
        {
            AudioManager.Instance.PlayEffect(m_DEFEATClip);
            m_DefeatObj = Instantiate(m_Defeat);
            m_DefeatObj.GetComponent<Defeat>().Init();
        }

    }

    public void Comple(int bossHp)
    {
        if (bossHp <= 0 && m_CompletObj == null)
        {
            AudioManager.Instance.PlayEffect(m_CompleClip);
            m_CompletObj = Instantiate(m_Complet);
            m_CompletObj.GetComponent<Complet>().Init();
        }

    }
    public void ChangeName()
    {
        if (m_TowerObj != null)
        {
            m_NameTxt.text = m_TowerObj.gameObject.name;
            m_AttackTxt.text = m_TowerObj.Attack.ToString();
            m_AttackSpeedTxt.text = m_TowerObj.AttackSpeed.ToString();
        }
    }
}
