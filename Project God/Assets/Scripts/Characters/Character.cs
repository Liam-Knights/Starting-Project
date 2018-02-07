using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] protected float m_healthMax = 1;
    protected float m_health = 1;

    [SerializeField] protected GameObject m_healthBar = null;

    [SerializeField] protected GameObject m_deathEffect = null;

    public GameObject m_characterModel = null;

	protected Inventory m_inventory = null;

    // Use this for initialization
	protected virtual void Awake ()
    {
        m_health = m_healthMax;
    }

	// Use this for initialization
	void Start ()
	{
		m_inventory = GetComponent<Inventory> ();
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
        //Check if dead
        if (IsDead())
            CharacterDeath();
    }

    protected bool IsDead()
    {
        return (m_health <= 0);
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;

        //Update health bar

        if (m_healthBar != null)
        {
            if(!IsDead())
            {
                Vector3 healthBarScale = m_healthBar.transform.localScale;
                healthBarScale.x = m_health / m_healthMax;

                m_healthBar.transform.localScale = healthBarScale;
            }
        }
    }

    public virtual Vector3 GetAimingDir(Vector3 projectileSpawnPos)
    {
        return Vector3.zero;
    }

    public void CharacterDeath()
    {
        if (m_deathEffect != null)
            Destroy(Instantiate(m_deathEffect, transform.position, Quaternion.identity),5.0f);
        Destroy(gameObject);
    }
}
