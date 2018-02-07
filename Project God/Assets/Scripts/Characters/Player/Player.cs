using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
		//Player Weapons 
		BaseWeapon weapon = m_inventory.GetCurrentWeapon().GetComponentInChildren<BaseWeapon>();
		if(weapon!=null)
			weapon.AttackInput (InputManager.m_instance.GetInputBool ("LightAttack"), InputManager.m_instance.GetInputBool ("HeavyAttack"));

		if (InputManager.m_instance.GetInputBool("SwapWeapon"))
			m_inventory.SwapWeapon();
    }

    public override Vector3 GetAimingDir(Vector3 projectileSpawnPos)
    {
        return(InputManager.m_instance.GetAimingDirection(transform.TransformPoint(projectileSpawnPos)));
    }
}
