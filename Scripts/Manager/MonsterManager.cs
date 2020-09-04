using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_WayPointObj;
    [SerializeField]
    private Transform[] m_MobMakePos;
    [HideInInspector]
    public GameObject m_BossMob;
    [SerializeField]
    private Map m_Map;
    [HideInInspector]
    public int m_mLife = 3;
    [HideInInspector]
    public int m_nRound = 1;
    [HideInInspector]
    public HPBar m_Hpbar;
    [HideInInspector]
    public GameObject m_HpCsObj;
    [HideInInspector]
    public Mob m_Boss;

    private int m_nRandomPos;
    private int m_nCount = 1;

    private float m_fMaking;

    private bool m_bLetGo = true;

    private GameObject m_MobObj;
    public GameObject m_LiveMngObj;
    public GameObject m_DieMngObj;
    private GameObject m_MobMakeObj;

    private SeeUI m_SeeUi;

    private List<string> m_LisPooling = new List<string>();
    private List<GameObject> m_LisPoolingObj = new List<GameObject>();
    private List<Mob> m_LisPoolingMob = new List<Mob>();

    public AudioClip m_WayStartClip;

    private void Start()
    {
        DataMng.AddTable(TableType.StageTable);
        DataMng.AddTable(TableType.MonsterTable);

        m_Hpbar = UIAdd.Get<HPBar>(UIType.HpBarUI);
        m_SeeUi = UIAdd.Get<SeeUI>(UIType.SeeUI);

        m_nCount = m_Map.m_nStartCount;
        m_nRound = 0;
    }

    private void Update()
    {
        //일시정지를 위한것
        if (SeeUI.m_nPause == false)
        {
            m_fMaking += Time.deltaTime;
            if (DataMng.Get(TableType.StageTable).ToS(m_nCount, "Name").Equals("Null"))
            {
                m_bLetGo = false;
                m_SeeUi.m_TimeImg.fillAmount = 1 - m_fMaking/15;
              
                if (m_fMaking >= 15.0f)
                {
                    StartCoroutine("TimeOn");
                    AudioManager.Instance.PlayEffect(m_WayStartClip);
                    m_bLetGo = true;
                    ++m_nRound;
                    m_SeeUi.m_TimeImg.fillAmount = 1;
                    ++m_nCount;
                }
            }
            else if (m_fMaking >= 4.0f && m_nCount <= m_Map.m_nLevelIndex && m_bLetGo == true)
            {
                m_MobObj = Resources.Load("Monster/" + DataMng.Get(TableType.StageTable).ToS(m_nCount, "Name")) as GameObject;

                if (!(m_LisPooling.Contains(m_MobObj.name)))
                {
                    if (DataMng.Get(TableType.StageTable).ToI(m_nCount, "Boss") == 0)
                    {
                        m_nRandomPos= Random.Range(0, m_WayPointObj.Length);

                        m_MobMakeObj = Instantiate(m_MobObj, m_MobMakePos[m_nRandomPos].position, Quaternion.identity);
                        m_MobMakeObj.transform.parent = m_LiveMngObj.transform;
                        m_MobMakeObj.name = m_MobObj.name;
                        Mob mon = m_MobMakeObj.GetComponent<Mob>();
                        mon.Init(m_WayPointObj[m_nRandomPos]);
                        m_Hpbar.SetMake(m_MobMakeObj, ref m_HpCsObj);
                        mon.hpcatch(m_HpCsObj.GetComponent<ReckoningHp>());
                    }
                    else
                    {
                        m_nRandomPos = Random.Range(0, m_WayPointObj.Length);
                        m_BossMob = Instantiate(m_MobObj, transform.position, Quaternion.identity);
                        m_BossMob.transform.parent = m_LiveMngObj.transform;
                        m_BossMob.name = m_MobObj.name;

                        m_Boss = m_BossMob.GetComponent<Mob>();
                        m_Boss.Init(m_WayPointObj[m_nRandomPos]);
                        m_Hpbar.SetMake(m_BossMob, ref m_HpCsObj);
                        m_Boss.hpcatch(m_HpCsObj.GetComponent<ReckoningHp>());
                    }
                }
                else
                {
                    for (int i = 0; i < m_LisPoolingObj.Count; ++i)
                    {
                        if (m_LisPooling[i].Equals(m_MobObj.name))
                        {
                            m_nRandomPos = Random.Range(0, m_WayPointObj.Length);
                            Mob _mon;
                            m_MobMakeObj = m_LisPoolingObj[i];

                            _mon = m_LisPoolingMob[i];

                            m_MobMakeObj.transform.position = m_MobMakePos[m_nRandomPos].position;
                            m_MobMakeObj.transform.parent = m_LiveMngObj.transform;

                            m_Hpbar.SetLive(_mon);
                            m_MobMakeObj.SetActive(true);

                            m_LisPooling.Remove(m_LisPooling[i]);
                            m_LisPoolingObj.Remove(m_LisPoolingObj[i]);
                            m_LisPoolingMob.Remove(m_LisPoolingMob[i]);
                            break;
                        }
                    }
                }
                ++m_nCount;
                m_fMaking = 0;
            }

        }

    }

    public void PlaseDie(GameObject mob, Mob monCs)
    {
        mob.transform.position = monCs.m_arrPoint[1].position;
        mob.transform.parent = m_DieMngObj.transform;
        mob.SetActive(false);
       
        m_LisPooling.Add(mob.name);
        m_LisPoolingObj.Add(mob);
        m_LisPoolingMob.Add(monCs);
    }

    IEnumerator TimeOn()
    {
        m_SeeUi.m_TimeOn.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        m_SeeUi.m_TimeOn.SetActive(false);
    }
}
