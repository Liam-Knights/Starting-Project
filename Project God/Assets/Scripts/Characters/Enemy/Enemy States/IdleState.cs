using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private float m_randomIdleTime = 1.0f;
    private float m_randomIdleTimer = 1.0f;

    protected override void Start()
    {
        base.Start();
    }

    public override void StateInit()
    {
        m_randomIdleTimer = m_randomIdleTime;
    }

    protected override void StateActions()
    {
        m_randomIdleTimer -= Time.deltaTime;

        if (m_randomIdleTimer<0.0f)
            m_stateManager.SetNextState(m_endTransition.m_nextState);
    }

    //Physics actions
    public override void StateFixedActions()
    {
        
    }

    public override void StateEnd()
    {

    }

    public void SetRandomTime(float minTime, float maxTime)
    {
        m_randomIdleTime = Random.Range(minTime, maxTime);
    }
}
