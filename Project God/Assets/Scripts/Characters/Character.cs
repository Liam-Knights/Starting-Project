using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] protected float m_healthMax = 1;
    protected float m_health = 1;

    [SerializeField] protected GameObject m_healthBar = null;

    // Use this for initialization
    void Awake ()
    {
        m_health = m_healthMax;
    }

    // Update is called once per frame
    protected void Update ()
    {
        //Check if dead
        if (IsDead())
            Destroy(gameObject);
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
}
