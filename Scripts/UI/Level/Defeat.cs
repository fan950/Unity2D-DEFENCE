using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Defeat : MonoBehaviour
{
    private bool m_bOneTouch;
    public AudioClip audioClip;

    public void Init()
    {
        m_bOneTouch = false;
    }

    public void ReLode()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DefeatBtn()
    {
        if (m_bOneTouch == false)
        {
            AudioManager.Instance.PlayEffect(audioClip);

            if (SceneMng.m_uIScreen != null)
            {
                SceneMng.m_uIScreen.Execute(true);
            }
           
            SceneMng.Instance.NextScene(SceneChange.SelectScene, 1.5f);
            m_bOneTouch = true;
        }
    }
}
