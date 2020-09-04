using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Sprite SkillReady;
    public Sprite SkillOn;

    public Image SkillImg;
    public Image SkillCool;
    public Text sillTex;

    private Vector3 mousePosition;
    private GameObject skillDrag;
    public GameObject SkillDrag;
    public GameObject skillObj;

    private float m_fTime;
    public float m_fCool;
    private float m_fCooltime;

    private void Start()
    {
        m_fCooltime = m_fCool;
    }
    private void Update()
    {
        if (SeeUI.m_nPause == false)
        {
            if (SkillCool.fillAmount != 0)
            {
                m_fTime += Time.deltaTime;
                m_fCooltime -= Time.deltaTime;
                sillTex.text = ((int)m_fCooltime).ToString();

                SkillCool.fillAmount = Mathf.Clamp01(1 - m_fTime / m_fCool);
                if (m_fCooltime < 0)
                {
                    StartCoroutine("Light");

                }
            }
        }
    }
    IEnumerator Light()
    {
        SkillImg.sprite = SkillOn;
        sillTex.text = " ";

        yield return new WaitForSeconds(1.0f);
        SkillImg.sprite = SkillReady;
        m_fCooltime = m_fCool;
        m_fTime = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (SkillCool.fillAmount == 0)
        {
            CameraCs.m_bCameraDreg = false;
            skillDrag = Instantiate(SkillDrag);
            skillDrag.transform.SetParent(transform.parent);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (SkillCool.fillAmount == 0)
        {
            skillDrag.transform.position = Input.mousePosition;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (skillDrag != null)
        {
            CameraCs.m_bCameraDreg = true;
            Destroy(skillDrag);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            GameObject skilClon = Instantiate(skillObj, mousePos, Quaternion.identity);
            StartCoroutine(SkillDamage(skilClon));
            Destroy(skilClon, 2.0f);

            SkillCool.fillAmount = 1;
        }
    }

    IEnumerator SkillDamage(GameObject skill)
    {
        yield return new WaitForSeconds(1.0f);
        skill.GetComponent<SkillDamage>().Init();
    }
}
