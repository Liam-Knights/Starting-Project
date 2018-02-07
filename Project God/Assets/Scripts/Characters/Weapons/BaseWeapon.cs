using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {

	public Animator m_animator = null;

	protected GameObject m_parent = null;

	public bool m_attacking = false;

	// Use this for initialization
	protected void OnEnable ()
	{
		m_animator = GetComponent<Animator>();
	}
	
	public void SetParent(GameObject parent)
	{
		m_parent = parent;
	}

	public void AttackInput(bool LightAttack, bool HeavyAttack)
	{
        if (m_animator != null)
        {
            if (LightAttack)
                m_animator.SetBool("LightAttack", true);
            else
                m_animator.SetBool("LightAttack", false);

            if (HeavyAttack)
                m_animator.SetBool("HeavyAttack", true);
            else
                m_animator.SetBool("HeavyAttack", false);
        }
	}

	public void StartOfAttack()
	{
		m_attacking = true;
	}

	public void EndOfAttack()
	{
		m_attacking = false;
	}
}
