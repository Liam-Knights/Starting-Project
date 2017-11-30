using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour {

    private InputManager m_inputManager = null;
    private Rigidbody m_rbPlayer = null;
    private CapsuleCollider m_ccPlayer = null;

    //Player Attributes
    [SerializeField] private float m_forwardsVelocity = 1.0f;
    [SerializeField] private float m_jumpVelocity = 1.0f;

    //Grounded setup
    private Vector3 m_ColliderBounds;

    private void Awake()
    {
        m_inputManager = GetComponent<Player>().m_InputManager;
        m_rbPlayer = GetComponent<Rigidbody>();
        m_ccPlayer = GetComponent<CapsuleCollider>();
    }

    // Use this for initialization
    void Start ()
    {
        m_ColliderBounds = m_ccPlayer.bounds.extents;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Movement
        Vector3 currentVelocity = m_rbPlayer.velocity;
        currentVelocity.x = (m_inputManager.GetInputFloat("Horizontal") * m_forwardsVelocity);
        if(IsGrounded() && m_inputManager.GetInputBool("Jump"))
            currentVelocity.y = m_jumpVelocity;
        m_rbPlayer.velocity = currentVelocity;

        //Attacking
    }

    private bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, -transform.up, m_ColliderBounds.y + 0.01f));
    }
}
