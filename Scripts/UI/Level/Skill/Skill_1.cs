using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : SkillDamage
{
    [HideInInspector]
    public List<Mob> mob = new List<Mob>();

    private float m_nfSkillTime = 0;

    public void Update()
    {
        m_nfSkillTime += Time.deltaTime;

        if (m_nfSkillTime > 0.5f)
        {
            m_nfSkillTime = 0;
            AudioManager.Instance.PlayEffect(audioClip);
        }
    }

    public override void Init()
    {

        if (SeeUI.m_nPause == false)
        {
            Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0, 1 << LayerMask.NameToLayer("Monster"));
            if (coll != null)
            {
                for (int i = 0; i < coll.Length; ++i)
                {
                    mob.Add(coll[i].gameObject.GetComponent<Mob>());

                    if (mob[i].m_Hp > 0)
                    {
                        mob[i].DecreaseHp(Damage);
                    }
                }
            }
        }
    }
}
