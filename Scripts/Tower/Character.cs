using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public AttackRange playAttack;
    private SpriteRenderer charobj;

    private float m_fPos_x;
    private bool m_bSwich = false;

    private void Start()
    {
        m_fPos_x = transform.localScale.x;
    }
    void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (playAttack.mobObj != null)
            {
                Vector3 dir = playAttack.mobObj.transform.position - transform.position;
                dir.Normalize();

                float _fAngle = Vector3.Angle(Vector3.up, dir);


                if (playAttack.mobObj.transform.position.x <= transform.position.x)
                {
                    if (m_bSwich == false)
                    {
                        transform.localScale = new Vector3(-m_fPos_x, transform.localScale.y, transform.localScale.z);
                        m_bSwich = true;
                    }
                    //시작각이 약 144도 약 8도부터 -10까지
                    _fAngle -= 141;

                    if (_fAngle < -10)
                    {
                        _fAngle = -10;
                    }
                    else if (_fAngle > 8)
                    {
                        _fAngle = 8;
                    }

                    transform.rotation = Quaternion.AngleAxis(_fAngle, transform.forward);
                }
                else
                {
                    if (m_bSwich == true)
                    {
                        transform.localScale = new Vector3(m_fPos_x, transform.localScale.y, transform.localScale.z);
                        m_bSwich = false;
                    }
                    _fAngle = 110 - _fAngle;
                    if (_fAngle < -10)
                    {
                        _fAngle = -10;
                    }
                    else if (_fAngle > 14)
                    {
                        _fAngle = 14;
                    }
                    //점점 각이 커지기때문에 마이너스 각으로 바꾸고 각 계산
                    //시작각이 약 177도 이고 우리가 원하는 각은 약 14~-10이기떄문에 계산      
                    transform.rotation = Quaternion.AngleAxis(_fAngle, transform.forward);
                }
            }
            else
                return;
        }
    }
}
