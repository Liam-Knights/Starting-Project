using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWithinRangeTransition : BaseTransition
{
    private GameObject m_target = null;

    private float m_checkDistance = 5.0f;

    //Initialisation
    public static TargetWithinRangeTransition CreateComponent(GameObject parent, float checkDistance)
    {
        TargetWithinRangeTransition component = parent.AddComponent<TargetWithinRangeTransition>();
        component.Init(checkDistance);
        return component;
    }

    private void Init(float checkDistance)
    {
        m_checkDistance = checkDistance;
    }
    //End Initialisation

    // Use this for initialization
    void Start ()
    {
        m_target = GetComponent<EnemyBase>().GetTarget();
    }

    // Update is called once per frame
    public override bool CheckTransition()
    {
        if(Vector3.Distance(m_target.transform.position, transform.position) < m_checkDistance)
            return true;
        return false;
    }
}
