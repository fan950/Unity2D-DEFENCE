using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField]
    private Transform m_TopPos;
    [SerializeField]
    private Transform m_BottomPos;
    [SerializeField]
    private Transform m_CenterPos;
    [HideInInspector]
    public bool m_bUpdate=false;
    [HideInInspector]
    public bool m_dir;

    private Vector3 BaseTopPos;
    private Vector3 BaseBottomPos;

    private float m_elapsedTime = 0;
    private float m_speed = 1.0f;

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        SceneMng.m_uIScreen = this;
        BaseTopPos = m_TopPos.position;
        BaseBottomPos = m_BottomPos.position;
    }

    public void Execute(bool dir)
    {
        m_bUpdate = true;
        m_dir = dir;
        m_elapsedTime = 0;
    }


    public void Update()
    {
        if (m_bUpdate == false)
        {
            return;
        }

        if (m_dir)
        {
            m_elapsedTime += Time.deltaTime / m_speed;
            m_elapsedTime = Mathf.Clamp01(m_elapsedTime);

            m_TopPos.position = Vector3.Lerp(m_TopPos.position, m_CenterPos.position, m_elapsedTime);
            m_BottomPos.position = Vector3.Lerp(m_BottomPos.position, m_CenterPos.position, m_elapsedTime);
            if (m_elapsedTime >= 1.0f)
            {
                m_elapsedTime = 0;
                m_bUpdate = false;
            }
        }
        else
        {
            m_elapsedTime += Time.deltaTime / m_speed;
            m_elapsedTime = Mathf.Clamp01(m_elapsedTime);

            m_TopPos.position = Vector3.Lerp(m_CenterPos.position, BaseTopPos, m_elapsedTime);
            m_BottomPos.position = Vector3.Lerp(m_CenterPos.position, BaseBottomPos, m_elapsedTime);
            if (m_elapsedTime >= 1.0f)
            {
                m_elapsedTime = 0;
                m_bUpdate = false;
            }
        }
    }
}
