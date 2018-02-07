using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingState : BaseState
{
    private GameObject m_target = null;

    private Rigidbody m_rbCharacter = null;
    private float m_forwardVelocity = 0.0f;

    private float m_fleeingMaxTime = 0.0f;
    private float m_fleeingTimer = 0.0f;

    protected override void Start()
    {
        base.Start();
        m_target = GetComponent<EnemyBase>().GetTarget();
        m_rbCharacter = GetComponent<Rigidbody>();
    }

    public override void StateInit()
    {
        m_fleeingTimer = 0.0f;
    }

    protected override void StateActions()
    {
        m_fleeingTimer += Time.deltaTime;

        if (m_fleeingTimer > m_fleeingMaxTime)
            m_stateManager.SetNextState(m_endTransition.m_nextState);
    }

    //Physics actions
    public override void StateFixedActions()
    {
        VectorMaths.RigidbodyXVelocity(m_rbCharacter, transform.position - m_target.transform.position, m_forwardVelocity);

        if (m_rbCharacter.velocity.x > 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else if (m_rbCharacter.velocity.x < 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    public override void StateEnd()
    {
        m_rbCharacter.velocity = Vector3.zero;
    }

    public void SetMovementVelocity(float velocity)
    {
        m_forwardVelocity = velocity;
    }

    public void SetFleeingMaxTime(float time)
    {
        m_fleeingMaxTime = time;
    }
}
