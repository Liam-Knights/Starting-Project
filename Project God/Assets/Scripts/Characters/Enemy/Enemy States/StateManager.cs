using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    private BaseState m_currentState = null;
	
	// Update is called once per frame
	void Update ()
    {
        m_currentState.StateUpdate();
        Debug.Log("Current State: " + m_currentState);
    }

    private void FixedUpdate()
    {
        m_currentState.StateFixedActions();
    }

    public void SetInitialState(BaseState initialState)
	{
		m_currentState = initialState;
		m_currentState.StateInit();
	}

    public void SetNextState(BaseState nextState)
    {
		if(m_currentState!=null)
        	m_currentState.StateEnd();
        m_currentState = nextState;
        m_currentState.StateInit();
    }
}
