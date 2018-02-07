using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    //States this state can move towards to

	[SerializeField]
	protected BaseTransition m_endTransition = null;

    [SerializeField]
    protected List<BaseTransition> m_interruptTransitions = new List<BaseTransition>();

    protected StateManager m_stateManager = null;
	protected EnemyBase m_enemyBase = null;

    protected virtual void Start()
    {
        m_stateManager = GetComponent<StateManager>();
		m_enemyBase = GetComponent<EnemyBase> ();
    }

    public virtual void StateInit()
    {


    }

    public void StateUpdate()
    {
		if(!CheckInterruptTransitions())
        StateActions();
    }

    protected virtual void StateActions()
    {

    }

    //Physics actions
    public virtual void StateFixedActions()
    {

    }

    public virtual void StateEnd()
    {
		
    }

	public bool CheckInterruptTransitions()
    {
		if(m_interruptTransitions == null)
			return false;
		
        foreach (BaseTransition transition in m_interruptTransitions)
        {
            if (transition.CheckTransition())
            {
                m_stateManager.SetNextState(transition.m_nextState);
                return true;
            }
        }
		return false;
    }

	public void AddEndTransition(BaseTransition endTransition, BaseState nextState)
	{
        endTransition.SetNextState(nextState);
        m_endTransition = endTransition;
	}

    public void AddInterruptTransition(BaseTransition interruptTransition, BaseState nextState)
    {
        interruptTransition.SetNextState(nextState);
        m_interruptTransitions.Add(interruptTransition);
    }

}
