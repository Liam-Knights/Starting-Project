using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : BaseState
{
    private Inventory m_inventory = null;
	private BaseWeapon m_weapon = null;

	private bool previousAttackingState = false; //Used to determine if attaking has just ended

	private GameObject m_target = null;

    protected override void Start()
    {
        base.Start();
        m_inventory = GetComponent<Inventory>();
        m_target = GetComponent<EnemyBase>().GetTarget();
    }

    public override void StateInit()
    {
		m_weapon = m_inventory.GetCurrentWeapon().GetComponentInChildren<BaseWeapon>();
		previousAttackingState = false;

		//Face towards player
		float targetXDir = m_target.transform.position.x - transform.position.x;
		if (targetXDir > 0)
			m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		else if (targetXDir < 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    protected override void StateActions()
    {
		if (m_weapon != null) 
		{
			m_weapon.AttackInput(true, false);
			if(!m_weapon.m_attacking && previousAttackingState) // end of weapon attack
				m_stateManager.SetNextState(m_endTransition.m_nextState);
			previousAttackingState = m_weapon.m_attacking;
		}
    }

    public override void StateEnd()
    {
		m_weapon.AttackInput(false, false);
    }
}
