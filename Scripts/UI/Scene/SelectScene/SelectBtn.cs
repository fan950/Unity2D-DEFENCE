using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBtn : MonoBehaviour
{
    public Image m_MapImg;
    public Sprite[] m_MapSprite = new Sprite[4];
    public AudioClip[] m_Click = new AudioClip[3];
    public GameObject LockObj;
    public Text Rank;

    private Dictionary<int, char> DicRank = new Dictionary<int, char>();

    public bool m_bOneTouch;

    public void Start()
    {
        if (SceneMng.m_uIScreen != null)
        {
            SceneMng.m_uIScreen.Execute(false);
        }
        LockObj.SetActive(false);

        m_bOneTouch = false;
    }

    SceneChange sceneChange = SceneChange.None;
    public int m_nSence = 2;

    public void NextBtn()
    {
        if (m_bOneTouch == false)
        {
            int _nIndex = 0;
            AudioManager.Instance.PlayEffect(m_Click[2]);
            switch (m_nSence)
            {
                case (int)SceneChange.Level1:
                    _nIndex = 0;
                    sceneChange = SceneChange.Level1;
                    break;
                case (int)SceneChange.Level2:
                    _nIndex = 1;
                    sceneChange = SceneChange.Level2;
                    break;
                case (int)SceneChange.Level3:
                    _nIndex = 2;
                    sceneChange = SceneChange.Level3;
                    break;
                case (int)SceneChange.Level4:
                    _nIndex = 3;
                    sceneChange = SceneChange.Level4;
                    break;
            }

            if (SceneMng.Instance.m_nClearScene[_nIndex] != 0)
            {
                if (SceneMng.m_uIScreen != null)
                {
                    SceneMng.m_uIScreen.Execute(true);
                }
                SceneMng.Instance.NextScene(sceneChange, 1.5f);
            }

            SeeUI.m_nPause = false;
        }
    }

    public void OpeningBtn()
    {
        if (SceneMng.m_uIScreen != null)
        {
            SceneMng.m_uIScreen.Execute(true);
        }

        SceneMng.Instance.NextScene(SceneChange.OpeningScene, 1.5f);
        AudioManager.Instance.PlayEffect(m_Click[0]);
    }

    public void NextMap()
    {
        AudioManager.Instance.PlayEffect(m_Click[1]);
        ++m_nSence;
        if (m_nSence > 5)
        {
            m_nSence = 5;
        }

        RankList(m_nSence);
        m_MapImg.sprite = m_MapSprite[m_nSence - 2];
    }
    public void BackMap()
    {
        AudioManager.Instance.PlayEffect(m_Click[1]);
        --m_nSence;
        if (m_nSence < 2)
        {
            m_nSence = 2;
        }
        RankList(m_nSence);
        m_MapImg.sprite = m_MapSprite[m_nSence - 2];
    }

    public void RankList(int Index)
    {
        switch (m_nSence)
        {
            case (int)SceneChange.Level1:
                sceneChange = SceneChange.Level1;
                LockObj.SetActive(false);
                Rank.text = "C";
                break;
            case (int)SceneChange.Level2:
                if (SceneMng.Instance.m_nClearScene[1] == 0)
                {
                    LockObj.SetActive(true);
                }
                else
                {
                    LockObj.SetActive(false);
                }
                sceneChange = SceneChange.Level2;
                Rank.text = "B";
                break;
            case (int)SceneChange.Level3:
                if (SceneMng.Instance.m_nClearScene[2] == 0)
                {
                    LockObj.SetActive(true);
                }
                else
                {
                    LockObj.SetActive(false);
                }
                sceneChange = SceneChange.Level3;
                Rank.text = "A";
                break;
            case (int)SceneChange.Level4:
                if (SceneMng.Instance.m_nClearScene[3] == 0)
                {
                    LockObj.SetActive(true);
                }
                else
                {
                    LockObj.SetActive(false);
                }
                sceneChange = SceneChange.Level4;
                Rank.text = "S";
                break;
        }
    }
}
