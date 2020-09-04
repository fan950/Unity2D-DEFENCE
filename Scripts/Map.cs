using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int m_nMapIndex;
    public int m_nLevelIndex;
    public int m_nStartCount;

    private void Start()
    {
        if (AudioManager.Instance.m_MusicSource == null)
        {
            AudioManager.Instance.Init();
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
