using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float m_baseDamage = 1;
    [SerializeField] private float m_heavyDamageIncrease = 1.2f;

    private float m_actualDamage = 1;

    private bool m_doingDamage = false;

    private List<GameObject> m_hitCharacters;

    private Animator m_animator = null;

	// Use this for initialization
	void Start ()
    {
        m_hitCharacters = new List<GameObject>();
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.m_instance.GetInputBool("LightAttack"))
            m_animator.SetBool("LightAttack", true);
        else
            m_animator.SetBool("LightAttack", false);

        if (InputManager.m_instance.GetInputBool("HeavyAttack"))
            m_animator.SetBool("HeavyAttack", true);
        else
            m_animator.SetBool("HeavyAttack", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_doingDamage)
        {
            if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") && other.gameObject.tag != gameObject.tag) // Only get charaters who arent on same side
            {
                if (!m_hitCharacters.Contains(other.gameObject))
                {
                    m_hitCharacters.Add(other.gameObject);
                    other.gameObject.GetComponent<Character>().TakeDamage(m_actualDamage);
                }
            }
        }
    }


    public void EnableDamage(int heavyAttack)
    {
        m_doingDamage = true;
        if (heavyAttack == 0)
            m_actualDamage = m_baseDamage;
        else
            m_actualDamage = m_baseDamage * m_heavyDamageIncrease;
    }

    public void DisableDamage()
    {
        m_doingDamage = false;
        m_hitCharacters.Clear();
    }
}
