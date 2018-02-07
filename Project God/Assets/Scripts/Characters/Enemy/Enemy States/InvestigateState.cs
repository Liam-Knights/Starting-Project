using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : BaseState
{
    private Vector3 m_lastKnowPos = Vector3.zero;

    private Rigidbody m_rbCharacter = null;
    private GameObject m_target = null;
    private float m_forwardVelocity = 0.0f;

    private float m_closingDistance = 1.0f;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        m_target = GetComponent<EnemyBase>().GetTarget();
        m_rbCharacter = GetComponent<Rigidbody>();
    }

    public override void StateInit()
    {
        m_lastKnowPos = m_target.transform.position;
    }

    protected override void StateActions()
    {
        if (Mathf.Abs(m_lastKnowPos.x - transform.position.x) < m_closingDistance || (m_rbCharacter.velocity.x > -0.1f && m_rbCharacter.velocity.x < 0.1f)) //Havent reached node yet on x axis, or velocity isnt changing(above or under target pos)
            m_stateManager.SetNextState(m_endTransition.m_nextState);
    }

    //Physics actions
    public override void StateFixedActions()
    {
        VectorMaths.RigidbodyXVelocity(m_rbCharacter, m_lastKnowPos - transform.position, m_forwardVelocity);

        if (m_rbCharacter.velocity.x > 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else if (m_rbCharacter.velocity.x < 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    public override void StateEnd()
    {
        m_rbCharacter.velocity = Vector3.zero;
    }

    public void SetMovementVelocity(float speed)
    {
        m_forwardVelocity = speed;
    }
}
