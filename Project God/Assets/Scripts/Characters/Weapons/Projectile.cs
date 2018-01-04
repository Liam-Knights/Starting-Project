using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float m_damage = 0.0f;

    [SerializeField] private GameObject m_explosionEffect = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") // Only get charaters who arent on same side
        {
            if(other.gameObject.tag != gameObject.tag)
            {
                other.gameObject.GetComponent<Character>().TakeDamage(m_damage);
                DestroyProjectile();
            }
        }
        else
            DestroyProjectile();
    }

    public void SetDamage(float damage)
    {
        m_damage = damage;
    }

    private void DestroyProjectile()
    {
        if (m_explosionEffect != null)
            Instantiate(m_explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
