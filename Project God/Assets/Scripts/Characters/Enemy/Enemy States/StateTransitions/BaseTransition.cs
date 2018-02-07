using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTransition : MonoBehaviour
{
    public BaseState m_nextState = null;

    public static BaseTransition CreateComponent(GameObject parent)
    {
        BaseTransition component = parent.AddComponent<BaseTransition>();
        component.Init();
        return component;
    }

    private void Init()
    {

    }

    public virtual bool CheckTransition()
    {
        return true;
    }

	public void SetNextState(BaseState nextState)
	{
		m_nextState = nextState;
	}
}
