using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    private float m_fTimeTw = 0;

    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            m_fTimeTw += Time.deltaTime;
            if (m_fTimeTw >= 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
