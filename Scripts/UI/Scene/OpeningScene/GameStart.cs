using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameStart : MonoBehaviour
{
    private static UIScreen m_UIScreen;
    public AudioClip m_WorldSound;
    public AudioClip m_Click;

    private bool m_nOneTouch;

    public void Start()
    {
        if (m_UIScreen == null)
        {
            GameObject uiScreenObj = Instantiate(Resources.Load("UI/UIScreen")) as GameObject;
            m_UIScreen = uiScreenObj.GetComponent<UIScreen>();
            m_UIScreen.Init();
        }

        if (m_UIScreen.m_dir == true)
        {
            m_UIScreen.Execute(false);
        }

        AudioManager.Instance.Init();
        AudioManager.Instance.WorldSound(m_WorldSound);

        SceneMng.Instance.DateLode();
        m_nOneTouch = false;
    }

    //Start버튼
    public void NextScene()
    {
        if (m_nOneTouch == false)
        {
            AudioManager.Instance.PlayEffect(m_Click);
            m_UIScreen.Execute(true);
            SceneMng.Instance.NextScene(SceneChange.SelectScene, 1.5f);
            m_nOneTouch = true;
        }
    }

    //게임종료
    public void EndGame()
    {
        Application.Quit();
    }

}
