using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackFlee : EnemyBase
{
    [SerializeField]
    private float m_forwardVelocity = 1.0f;
    [SerializeField]
    private float m_detectionRange = 5.0f;
    [SerializeField]
    private float m_attackingRange = 5.0f;
    [SerializeField]
    private float m_fleeingDistance = 6.0f;
    [SerializeField]
    private float m_fleeingMaxTime = 2.0f;

    [SerializeField]
    private float m_idleMinTime = 1.0f;
    [SerializeField]
    private float m_idleMaxTime = 2.0f;

    [SerializeField]
    private List<GameObject> m_patrollingNodes = new List<GameObject>();

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //Setup states
        PatrollingState patrollingState = gameObject.AddComponent<PatrollingState>();
        patrollingState.SetNodes(m_patrollingNodes);
        patrollingState.SetMovementVelocity(m_forwardVelocity);

        MoveTowardsState moveTowardsState = gameObject.AddComponent<MoveTowardsState>();
        moveTowardsState.SetMovementVelocity(m_forwardVelocity);

        InvestigateState investigateState = gameObject.AddComponent<InvestigateState>();
        investigateState.SetMovementVelocity(m_forwardVelocity);

        IdleState idleState = gameObject.AddComponent<IdleState>();
        idleState.SetRandomTime(m_idleMinTime, m_idleMaxTime);

        AttackingState attackingState = gameObject.AddComponent<AttackingState>();

        FleeingState fleeingState = gameObject.AddComponent<FleeingState>();
        fleeingState.SetMovementVelocity(m_forwardVelocity);
        fleeingState.SetFleeingMaxTime(m_fleeingMaxTime);

        //Assigning transitons
        patrollingState.AddInterruptTransition(TargetSightTransition.CreateComponent(gameObject, m_detectionRange), moveTowardsState);

        moveTowardsState.AddInterruptTransition(NotTransition.CreateComponent(gameObject, TargetSightTransition.CreateComponent(gameObject, m_detectionRange)), investigateState);
        moveTowardsState.AddInterruptTransition(TargetWithinRangeTransition.CreateComponent(gameObject, m_attackingRange), attackingState);

        investigateState.AddEndTransition(BaseTransition.CreateComponent(gameObject), idleState);
        investigateState.AddInterruptTransition(TargetSightTransition.CreateComponent(gameObject, m_detectionRange), moveTowardsState);

        idleState.AddEndTransition(BaseTransition.CreateComponent(gameObject), patrollingState);
        idleState.AddInterruptTransition(TargetSightTransition.CreateComponent(gameObject, m_detectionRange), moveTowardsState);

        attackingState.AddEndTransition(BaseTransition.CreateComponent(gameObject), fleeingState);

        fleeingState.AddEndTransition(BaseTransition.CreateComponent(gameObject), moveTowardsState);
        fleeingState.AddInterruptTransition(NotTransition.CreateComponent(gameObject, TargetWithinRangeTransition.CreateComponent(gameObject, m_fleeingDistance)), moveTowardsState);

        //Setup intial state
        GetComponent<StateManager>().SetInitialState(patrollingState);
    }
}
