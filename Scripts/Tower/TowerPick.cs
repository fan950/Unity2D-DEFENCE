using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum SelectPoint
{
    Nono,
    TwClon,
    BTw,
    GR,
    GRM,
}

public class TowerPick : MonoBehaviour
{
    [HideInInspector]
    public Guard m_gm;

    public AudioClip ClickClip;
    public AudioClip ConstructClip;
    public GameObject[] TowerObj = new GameObject[16];
    public Tower[] TowerCs = new Tower[15];
    public GameObject[] Towers;

    private GameObject TowerUI;

    public Bounds bs;

    public static int m_nObjCount = 0;

    public static bool m_bSwich = true;
    public static string selectPoint = SelectPoint.Nono.ToString();

    public void Init()
    {
        Towers = GameObject.FindGameObjectsWithTag("Tower");
    }

    public GameObject MousePicking(ref GameObject _Obj)
    {
        _Obj = null;
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlayEffect(ClickClip);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, 1 << LayerMask.NameToLayer("Select"));

            if (hit.collider != null)
            {
                bs.center = hit.collider.transform.position;
                TowerBtn towerBtn = UIAdd.Get<TowerBtn>(UIType.TowerBtn);
                if (hit.transform.gameObject.tag == "TowerClon" && selectPoint != SelectPoint.GR.ToString())
                {
                    AudioManager.Instance.PlayEffect(ConstructClip);
                    _Obj = TwClon(_Obj, towerBtn);
                }
                else if (hit.transform.gameObject.tag == "BuildTower" && m_bSwich == true && selectPoint != SelectPoint.GR.ToString())
                {
                    if (towerBtn.transform.GetChild(1).gameObject.activeSelf == false)
                    {
                        BTw(towerBtn, hit, hit.collider.gameObject);
                    }
                }
                else if (hit.transform.gameObject.tag == "Guard" && m_bSwich == true)
                {
                    selectPoint = SelectPoint.GR.ToString();
                    GR(towerBtn, hit);
                }
                else if (towerBtn.gameObject.transform.GetChild(2).gameObject.activeSelf == true &&
                        towerBtn.gameObject.transform.GetChild(3).gameObject.activeSelf == true && hit.transform.gameObject.tag == "Range")
                {
                    selectPoint = SelectPoint.Nono.ToString();
                    GRM(towerBtn, hit);
                }
            }
        }
        return _Obj;
    }
    //타워 클론
    public GameObject TwClon(GameObject obj, TowerBtn towerBtn)
    {
        for (int i = 0; i < Towers.Length; ++i)
        {
            if (Towers[i].transform.GetChild(0).name == TowerObj[15].name)
            {
                //겹치는 부분만 생성시키기 위해서이다.
                if (bs.Intersects(Towers[i].transform.GetChild(0).GetComponent<BuildTowerBounds>().bs))
                {
                    GameObject tower = Instantiate(TowerObj[TowerManager.m_nTwindex], Towers[i].transform.position, Quaternion.identity);

                    obj = Towers[i];

                    tower.transform.SetParent(Towers[i].transform);
                    tower.name = TowerObj[TowerManager.m_nTwindex].name;

                    TowerManager.m_DicSynergyCount.Add(TowerManager.m_DicTowerNumber[Towers[i].name], TowerManager.m_DicTowerProperty[tower.name]);

                    if (!TowerManager.m_DicCheck.ContainsKey(tower.name))
                    {
                        TowerManager.m_DicCheck.Add(tower.name, TowerManager.m_nTwindex);
                        TowerManager.m_DicTwCsGet.Add(TowerManager.m_nTwindex, tower.GetComponent<Tower>());
                        SeeUI.m_TowerObj = tower.GetComponent<Tower>();
                    }

                    if (Towers[i].transform.childCount >= 2)
                    {
                        Destroy(Towers[i].transform.GetChild(0).gameObject);
                    }


                    if (m_bSwich == true)
                    {
                        ++TowerManager.m_nCount[TowerManager.m_nTwindex];
                    }

                    else
                    {
                        towerBtn.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true;
                        towerBtn.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Button>().enabled = true;

                        towerBtn.m_Sellimage.color = new Color(towerBtn.m_Sellimage.color.r, towerBtn.m_Sellimage.color.g, towerBtn.m_Sellimage.color.b, 1.0f);
                        towerBtn.m_MoveText.color = new Color(towerBtn.m_MoveText.color.r, towerBtn.m_MoveText.color.g, towerBtn.m_MoveText.color.b, 1.0f);

                        towerBtn.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        m_bSwich = true;
                    }
                    if (TowerManager.m_nCount[TowerManager.m_nTwindex] == 3)
                    {
                        Destroy(tower);
                    }
                }
                else
                {
                    Destroy(Towers[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
                }
            }
            if (i == Towers.Length - 1)
            {
                towerBtn.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        return obj;
    }
    //타워
    public void BTw(TowerBtn towerBtn, RaycastHit2D hit, GameObject hitobj)
    {
        TowerManager.m_nTwindex = TowerManager.m_DicCheck[hitobj.name];
        SeeUI.m_TowerObj = hitobj.GetComponent<Tower>();
        m_bSwich = false;

        towerBtn.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        towerBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        towerBtn.gameObject.transform.GetChild(2).gameObject.SetActive(true);

        if (TowerManager.m_Propertycount[2] == 3 || TowerManager.m_Propertycount[2] == 2)
        {
            TowerCs[TowerManager.m_nTwindex] = hit.collider.gameObject.GetComponent<Tower>();
        }
        towerBtn.gameObject.transform.GetChild(2).transform.localScale = TowerCs[TowerManager.m_nTwindex].AttackRange;

        towerBtn.gameObject.transform.GetChild(2).transform.transform.position = hit.transform.gameObject.transform.position;

        towerBtn.m_SelectObject = hit.transform.gameObject;
    }
    //가드
    public void GR(TowerBtn towerBtn, RaycastHit2D hit)
    {
        towerBtn.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        towerBtn.gameObject.transform.GetChild(3).gameObject.SetActive(true);

        towerBtn.gameObject.transform.GetChild(2).transform.localScale = new Vector3(1.8f, 1.8f, 1);

        towerBtn.gameObject.transform.GetChild(2).transform.transform.position = new Vector3(hit.transform.parent.transform.parent.gameObject.transform.position.x,
          hit.transform.parent.transform.parent.gameObject.transform.position.y,
            hit.transform.parent.transform.parent.gameObject.transform.position.z);

        m_gm = hit.transform.gameObject.GetComponent<Guard>();
    }
    //가드 무브
    public void GRM(TowerBtn towerBtn, RaycastHit2D hit)
    {
        towerBtn.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        towerBtn.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        //가드 스크립트에 직접 사용하여서 움직였다.
        m_gm.SetObj(hit.point);
    }
}