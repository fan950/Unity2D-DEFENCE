using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : SkillDamage
{
    [HideInInspector]
    public Mob mob;

    private float m_nfSkillTime = 0;
    private bool m_bSound = true;

    public void Update()
    {
        m_nfSkillTime += Time.deltaTime;

        if (m_nfSkillTime > 0.5f&& m_bSound==true)
        {
            m_bSound = false;
            AudioManager.Instance.PlayEffect(audioClip);
        }
    }

    public override void Init()
    {
        if (SeeUI.m_nPause == false)
        {
            Collider2D coll = Physics2D.OverlapBox(transform.position, new Vector2(1f, 1f), 0, 1 << LayerMask.NameToLayer("Monster"));
            if (coll != null)
            {
                mob = coll.gameObject.GetComponent<Mob>();
                if (mob.m_Hp > 0)
                {
                    mob.DecreaseHp(Damage);
                }
            }
        }
    }
}
