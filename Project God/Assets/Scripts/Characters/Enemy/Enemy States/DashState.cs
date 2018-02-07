using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : BaseState
{

    private GameObject m_target = null;

    private Rigidbody m_rbCharacter = null;

    [SerializeField] private float m_dashVelocity = 1.5f;
    [SerializeField] private float m_dashTime = 0.5f;

    protected override void Start()
    {
        base.Start();
        m_target = GetComponent<EnemyBase>().GetTarget();
        m_rbCharacter = GetComponent<Rigidbody>();
    }

    public override void StateInit()
    {
        Vector3 dashDir = new Vector3(m_target.transform.position.x - transform.position.x, 0.0f,0.0f).normalized;

        m_rbCharacter.useGravity = false;
        m_rbCharacter.velocity = dashDir * m_dashVelocity;

        if (m_rbCharacter.velocity.x > 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else if (m_rbCharacter.velocity.x < 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        Invoke("EndDash", m_dashTime);
    }

    protected override void StateActions()
    {
    }

    public override void StateEnd()
    {
        m_rbCharacter.velocity = Vector3.zero;
        m_rbCharacter.useGravity = true;
    }

    private void EndDash()
    {
        m_stateManager.SetNextState(m_endTransition.m_nextState);
    }

    public void SetDashVelocity(float velocity)
    {
        m_dashVelocity = velocity;
    }

    public void SetDashTime(float time)
    {
        m_dashTime = time;
    }
}
