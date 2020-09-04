using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraCs : MonoBehaviour
{
    private Vector3 mousePos;
    private float m_fSpeed = 5.5f;
    public float m_fMaxPos;
    public float m_fMinPos;

    public float m_fClicking = 0;
    public static bool m_bCameraDreg = true;

    private void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (m_bCameraDreg == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_fClicking = 0;
                    mousePos = Input.mousePosition;
                }
    
                if (Input.GetMouseButton(0))
                {
                    m_fClicking += Time.deltaTime;
                    //민감한 터치를 방지하기 위한 부분
                    if (m_fClicking > 0.2f)
                    {
                        if (mousePos.y < Input.mousePosition.y)
                        {
                            transform.Translate(0, Time.deltaTime * m_fSpeed, 0);
                        }
                        else if (mousePos.y > Input.mousePosition.y)
                        {
                            transform.Translate(0, Time.deltaTime * -m_fSpeed, 0);
                        }

                        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, m_fMinPos, m_fMaxPos), transform.position.z);
                    }
                }

            }
        }
    }
}
