using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedStaff : MonoBehaviour {

    [SerializeField] private GameObject m_projectile = null;
    [SerializeField] private float m_damage = 0.0f;
    [SerializeField] private Vector3 m_projectileSpawnPos = Vector3.zero;

    public float m_projectleSpeed = 1.0f;

    private Animator m_animator = null;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.m_instance.GetInputBool("LightAttack") || InputManager.m_instance.GetInputBool("HeavyAttack"))
            m_animator.SetBool("FireStaff", true);
        else
            m_animator.SetBool("FireStaff", false);
    }

    public void RangedAttack()
    {
        Vector3 fireDir = InputManager.m_instance.GetAimingDirection(transform.TransformPoint(m_projectileSpawnPos));
        GameObject newProjectile = Instantiate(m_projectile, transform.TransformPoint(m_projectileSpawnPos), Quaternion.identity);
        newProjectile.tag = transform.parent.tag;
        newProjectile.GetComponent<Projectile>().SetDamage(m_damage);
        newProjectile.GetComponent<Rigidbody>().velocity = fireDir.normalized * m_projectleSpeed;
    }
}
