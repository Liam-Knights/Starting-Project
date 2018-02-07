using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotTransition : BaseTransition 
{
	BaseTransition m_transition = null;

    //Initialisation
    public static NotTransition CreateComponent(GameObject parent, BaseTransition transition)
    {
        NotTransition component = parent.AddComponent<NotTransition>();
        component.Init(transition);
        return component;
    }

    private void Init(BaseTransition transition)
    {
        m_transition = transition;
    }
    //End Initialisation

    public override bool CheckTransition()
	{
		return(!m_transition.CheckTransition ());
	}
}
