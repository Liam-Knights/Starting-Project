using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class PlayerMovement : MonoBehaviour {

    private Rigidbody m_rbPlayer = null;
    private BoxCollider m_bcPlayer = null;
    private Player m_scpPlayer = null;
    
    //Player Attributes
    [SerializeField] private float m_forwardsVelocity = 1.0f;
    [SerializeField] private float m_jumpVelocity = 1.0f;

    [SerializeField] private float m_dashVelocity = 1.5f;
    [SerializeField] private float m_dashTime = 0.5f;
    private bool m_dashEnabled = false;
    private bool m_dashOccuring = false;

    private bool m_doubleJumpEnabled = false;
    private bool m_doubleJumpAwaitingKeyUp = false;

    private bool m_doubleJumpDamageEnabled = false;
    [SerializeField] private float m_doubleJumpDamage = 2.0f;
    [SerializeField] private float m_doubleJumpDamageRange = 3.0f;

    [SerializeField] private GameObject m_doubleJumpLandingEffect = null;
    [SerializeField] private Vector3 m_doubleJumpLandingOffset = Vector3.zero;

    private Vector3 m_colliderBounds;

    private CollisionDir m_collisionDir = new CollisionDir(0.1f);

    private void Awake()
    {
        m_rbPlayer = GetComponent<Rigidbody>();
        m_bcPlayer = GetComponent<BoxCollider>();
        m_scpPlayer = GetComponent<Player>();
    }

    // Use this for initialization
    void Start ()
    {
        m_colliderBounds = m_bcPlayer.bounds.extents;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_dashOccuring) //No need to worry about movemnt when dashing
            return;

        UpdateCollisionBox();

        float xAxis = InputManager.m_instance.GetInputFloatRaw("MovementHorizontal");

        //Player rotation
        Vector3 aiming = InputManager.m_instance.GetAimingDirection(transform.position);
        if (aiming.x > 0)
			m_scpPlayer.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else if(aiming.x < 0)
            m_scpPlayer.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        //Left right
        Vector3 currentVelocity = m_rbPlayer.velocity;
        currentVelocity.x = xAxis * m_forwardsVelocity;


        //Jumping
        if(m_collisionDir.m_down && InputManager.m_instance.GetInputBool("Jump"))
        {
            currentVelocity.y = m_jumpVelocity;
            m_doubleJumpAwaitingKeyUp = true;
            m_dashEnabled = true;
        }

        //Enabling double jump
        if (m_doubleJumpAwaitingKeyUp && !InputManager.m_instance.GetInputBool("Jump")) 
        {
            m_doubleJumpAwaitingKeyUp = false;
            m_doubleJumpEnabled = true;
        }

        //Double jump
        if (m_doubleJumpEnabled)//Double jump only when enabled(after jumping), in air and space bar
        {
            if (m_collisionDir.m_down)
                m_doubleJumpEnabled = false;
            else if (InputManager.m_instance.GetInputBool("Jump"))
            {
                currentVelocity.y = m_jumpVelocity;
                m_doubleJumpEnabled = false;
                m_doubleJumpDamageEnabled = true;
            }
        }

        //Double jump damage
        if(m_doubleJumpDamageEnabled && m_collisionDir.m_down)
        {
            m_doubleJumpDamageEnabled = false;

            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, m_doubleJumpDamageRange);
            foreach (Collider collider in objectsInRange)
            {
                if (collider.gameObject.tag == "Enemy")
                    collider.GetComponent<Character>().TakeDamage(m_doubleJumpDamage);
            }

            if (m_doubleJumpLandingEffect != null)
                Instantiate(m_doubleJumpLandingEffect, transform.position + m_doubleJumpLandingOffset, Quaternion.identity);
        }

        //Apply all effects to player
        m_rbPlayer.velocity = currentVelocity;

        //Dashing
        if (m_dashEnabled && xAxis != 0.0f && InputManager.m_instance.GetInputBool("Dash"))
        {
            m_dashEnabled = false;
            m_dashOccuring = true;
            m_rbPlayer.useGravity = false;
			m_rbPlayer.velocity = new Vector3(InputManager.m_instance.GetInputFloatRaw("MovementHorizontal") * m_dashVelocity, 0.0f, 0.0f);
            Invoke("EndDash", m_dashTime);
        }
    }

    private void EndDash()
    {
        m_dashOccuring = false;
        m_rbPlayer.useGravity = true;
    }

    public struct CollisionDir
    {
        public bool m_left;
        public bool m_right;
        public bool m_up;
        public bool m_down;

        public float m_collisionDetectionRange;

        public CollisionDir(float collisionDetectionRange)
        {
            m_right = m_left = m_up = m_down = false;
			m_collisionDetectionRange = collisionDetectionRange;
        }
    }

    private void UpdateCollisionBox()
    {
        m_collisionDir.m_right = RaycastDir(transform.position + new Vector3(0.0f, m_colliderBounds.y * 0.9f, 0.0f), transform.position + new Vector3(0.0f, 0.0f, 0.0f), transform.position + new Vector3(0.0f, -m_colliderBounds.y * 0.9f, 0.0f), Vector3.right, m_colliderBounds.x);
        m_collisionDir.m_left = RaycastDir(transform.position + new Vector3(0.0f, m_colliderBounds.y * 0.9f, 0.0f), transform.position + new Vector3(0.0f, 0.0f, 0.0f), transform.position + new Vector3(0.0f, -m_colliderBounds.y * 0.9f, 0.0f), Vector3.left, m_colliderBounds.x); 
        m_collisionDir.m_up = RaycastDir(transform.position + new Vector3(-m_colliderBounds.x * 0.9f, 0.0f, 0.0f), transform.position + new Vector3(0.0f, 0.0f, 0.0f), transform.position + new Vector3(m_colliderBounds.x * 0.9f, 0.0f, 0.0f), Vector3.up, m_colliderBounds.y);
        m_collisionDir.m_down = RaycastDir(transform.position + new Vector3(-m_colliderBounds.x * 0.9f, 0.0f, 0.0f), transform.position + new Vector3(0.0f, 0.0f, 0.0f), transform.position + new Vector3(m_colliderBounds.x * 0.9f, 0.0f, 0.0f), Vector3.down, m_colliderBounds.y);
    }

    private bool RaycastDir(Vector3 pos1, Vector3 pos2, Vector3 pos3, Vector3 direction, float m_colliderDepth)
    {
        if (Physics.Raycast(pos1, direction, m_colliderDepth + m_collisionDir.m_collisionDetectionRange))
            return true;
        if (Physics.Raycast(pos2, direction, m_colliderDepth + m_collisionDir.m_collisionDetectionRange))
            return true;
        if (Physics.Raycast(pos3, direction, m_colliderDepth + m_collisionDir.m_collisionDetectionRange))
            return true;
        return false;
    }
}
