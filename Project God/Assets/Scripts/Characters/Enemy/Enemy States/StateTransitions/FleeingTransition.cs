using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingTransition : BaseTransition
{
    private GameObject m_player = null;

    [SerializeField]
    private float m_fleeDistance = 0.0f;
    [SerializeField]
    private float m_fleeMaxTime = 0.0f;
    private float m_fleeTimer = 0.0f;

    //Initialisation
    public static FleeingTransition CreateComponent(GameObject parent, float fleeDistance, float fleeMaxTime)
    {
        FleeingTransition component = parent.AddComponent<FleeingTransition>();
        component.Init(fleeDistance, fleeMaxTime);
        return component;
    }

    private void Init(float fleeDistance, float fleeMaxTime)
    {
        m_fleeDistance = fleeDistance;
        m_fleeMaxTime = fleeMaxTime;
    }
    //End Initialisation

    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<EnemyBase>().GetTarget();
    }

    void Update()
    {
        m_fleeTimer += Time.deltaTime;
    }

    // Update is called once per frame
    public override bool CheckTransition()
    {
        if (Vector3.Distance(m_player.transform.position, transform.position) < m_fleeDistance || m_fleeTimer>m_fleeMaxTime)
            return true;
        return false;
    }
}
