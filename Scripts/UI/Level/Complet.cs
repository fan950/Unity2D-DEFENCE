using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complet : MonoBehaviour
{
    private Map m_Map;
    public AudioClip audioClip;

    private bool m_bOneTouch;

    public void Init()
    {
         m_Map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        m_bOneTouch = false;
    }
    public void CompletedBtn()
    {
        if (m_bOneTouch == false)
        {
            AudioManager.Instance.PlayEffect(audioClip);

            if (SceneMng.m_uIScreen != null)
            {
                SceneMng.m_uIScreen.Execute(true);
            }

            SceneMng.Instance.m_nClearScene[m_Map.m_nMapIndex] = 1;
            SceneMng.Instance.DateSave();
            SceneMng.Instance.NextScene(SceneChange.SelectScene, 1.5f);
            m_bOneTouch = true;
        }
    }
}
