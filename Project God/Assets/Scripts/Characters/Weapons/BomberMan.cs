using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : BaseWeapon
{
    [SerializeField] private float m_detonationDamage = 1.0f;
    [SerializeField] private float m_detonationRange = 3.0f;

    [SerializeField] protected GameObject m_explosionEffect = null;

    public void Detonation()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, m_detonationRange);
        foreach (Collider col in objectsInRange)
        {
            if(col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<Character>().TakeDamage(m_detonationDamage);
            }
        }

        if(m_explosionEffect!= null)
            Destroy(Instantiate(m_explosionEffect, transform.position, Quaternion.identity), 5.0f);

        m_parent.GetComponent<Character>().CharacterDeath();
    }

    public void SetVaribles(float damage, float range)
    {
        m_detonationDamage = damage;
        m_detonationRange = range;
    }
}
