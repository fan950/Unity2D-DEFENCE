using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    enum TowerCountMax
    {
        max = 5,
        max2 = 10,
        max3 = 15,
    }

    public static Dictionary<string, int> m_DicCheck = new Dictionary<string, int>();
    public static Dictionary<int, Tower> m_DicTwCsGet = new Dictionary<int, Tower>();

    public static int m_nTwindex = -1;
    public static int[] m_nCount = new int[(int)TowerCountMax.max3];

    public static int m_nGold;
    public static Dictionary<string, int> m_DicTowerGold = new Dictionary<string, int>();
    public static Dictionary<string, int> m_DicTowerNumber = new Dictionary<string, int>();
    public static Dictionary<string, char> m_DicTowerProperty = new Dictionary<string, char>();
    public static Dictionary<int, char> m_DicSynergyCount = new Dictionary<int, char>();


    public static bool m_bUISwich = true;

    private TowerUpgrade m_TwUg;
    private TowerPick m_TwMk;

    private GameObject m_Swichobj;
    public List<GameObject> m_LisTwNumber = new List<GameObject>();

    [HideInInspector]
    public SeeUI m_SeeUi;

    public static int[] m_Propertycount;

    private void Awake()
    {
        for (int i = 0; i < m_nCount.Length; ++i)
        {
            m_nCount[i] = 0;
        }
        m_Propertycount = new int[6];

        //씬 다시 로드해서 초기화
        m_nGold = 20;
        m_DicTwCsGet.Clear();
        m_DicCheck.Clear();
        m_DicSynergyCount.Clear();
        m_DicTowerProperty.Clear();
        m_DicTowerNumber.Clear();

        m_TwMk = gameObject.GetComponent<TowerPick>();
        m_TwUg = gameObject.GetComponent<TowerUpgrade>();

        m_TwMk.Init();
        m_TwUg.Init();

        DataMng.AddTable(TableType.TowerTable);
        DataMng.AddTable(TableType.SynergyTable);

        //시너지를 확인하기위한 타워의 번호 부여
        for (int i = 0; i < m_LisTwNumber.Count; ++i)
        {
           m_DicTowerNumber.Add(m_LisTwNumber[i].name, i);
        }
        m_SeeUi = UIAdd.Get<SeeUI>(UIType.SeeUI);

    }
    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            GameObject a_makeObj = null;          
            if (m_TwMk.MousePicking(ref a_makeObj))
            {
                // 타워의 업그레이드를 처리
                if (a_makeObj.tag == "Tower")
                {
                    m_TwUg.Towerupgrade(a_makeObj);

                    if (m_DicSynergyCount.ContainsKey(m_DicTowerNumber[a_makeObj.name]))
                    {                       
                        switch (m_DicSynergyCount[m_DicTowerNumber[a_makeObj.name]])
                        {
                            case 'A':
                                ++m_Propertycount[1];
                                break;
                            case 'B':
                                ++m_Propertycount[2];
                                break;
                            case 'C':
                                ++m_Propertycount[3];
                                break;
                            case 'D':
                                ++m_Propertycount[4];
                                break;
                            case 'E':
                                ++m_Propertycount[5];
                                break;
                        }
                    }
                }
            }
        }
    }
}
