using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedStaff : BaseWeapon {

    [SerializeField] private GameObject m_projectile = null;
    [SerializeField] private float m_damage = 0.0f;
    [SerializeField] private Vector3 m_projectileSpawnPos = Vector3.zero;

    public float m_projectleSpeed = 1.0f;

    private Character m_character = null;

    private void Start()
    {
        m_character = m_parent.GetComponent<Character>();
    }

    public void RangedAttack()
    {
        //TODO make this look nicer
        Vector3 fireDir = m_character.GetAimingDir(m_projectileSpawnPos);
        GameObject newProjectile = Instantiate(m_projectile, transform.TransformPoint(m_projectileSpawnPos), Quaternion.identity);
        newProjectile.tag = transform.parent.tag;
        newProjectile.GetComponent<Projectile>().SetDamage(m_damage);
        newProjectile.GetComponent<Rigidbody>().velocity = fireDir.normalized * m_projectleSpeed;
    }
}
