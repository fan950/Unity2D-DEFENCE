using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum SceneChange
{
    None = -1,
    OpeningScene = 0,
    SelectScene,

    Level1,
    Level2,
    Level3,
    Level4,
}
public class SceneMng : MonoBehaviour
{
    public int[] m_nClearScene = new int[4];

    public static UIScreen m_uIScreen;

    private static SceneMng m_Instance;

    public static SceneMng Instance
    {
        get
        {
            if (m_Instance == null)
            {
                GameObject obj = new GameObject("Scene Manager", typeof(SceneMng));
                m_Instance = obj.GetComponent<SceneMng>();
                DontDestroyOnLoad(obj);
            }
            return m_Instance;
        }
    }
    public void NextScene(SceneChange sceneChange, float time)
    {
        StartCoroutine(IELoadAsyncScene(sceneChange, time));
    }

    private IEnumerator IELoadAsyncScene(SceneChange sceneChange, float time)
    {
        yield return new WaitForSeconds(time);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneChange.ToString());

        while (operation.isDone == false)
        {
            yield return null;
        }

    }

    public void DateSave()
    {
        m_nClearScene[0] = 1;


        string _sTmpStr = string.Empty;
        
        for (int i = 0; i < m_nClearScene.Length; i++)
        {
            _sTmpStr = _sTmpStr + m_nClearScene[i];
            if (i < m_nClearScene.Length)
            {
                _sTmpStr = _sTmpStr + ",";
            }
        }
     
        PlayerPrefs.SetString("Data", _sTmpStr);
    }

    public void DateLode()
    {
        if (!PlayerPrefs.HasKey("Data"))
        {
            return;
        }

        string[] _sDataArr = PlayerPrefs.GetString("Data").Split(',');

        for (int i = 0; i < m_nClearScene.Length; i++)
        {
            m_nClearScene[i] = Convert.ToInt32(_sDataArr[i]);
        }
    }
}
