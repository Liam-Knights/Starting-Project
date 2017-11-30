using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    private InputManager m_inputManager = null;
    [SerializeField] private GameObject m_weapon = null;

    // Use this for initialization
    void Awake ()
    {
        m_inputManager = GetComponent<Player>().m_InputManager;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(m_inputManager.GetInputBool("LightAttack"))
            m_weapon.GetComponent<Animator>().SetBool("LightAttack", true);
        else
            m_weapon.GetComponent<Animator>().SetBool("LightAttack", false);

        if (m_inputManager.GetInputBool("HeavyAttack"))
            m_weapon.GetComponent<Animator>().SetBool("HeavyAttack", true);
        else
            m_weapon.GetComponent<Animator>().SetBool("HeavyAttack", false);
    }
}
