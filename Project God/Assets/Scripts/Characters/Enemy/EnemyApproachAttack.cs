using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApproachAttack : EnemyBase 
{
	[SerializeField]
	private float m_forwardVelocity = 1.0f;
	[SerializeField]
	private float m_detectionRange = 5.0f;
    [SerializeField]
    private float m_attackingRange = 5.0f;

    [SerializeField]
    private float m_idleMinTime = 1.0f;
    [SerializeField]
    private float m_idleMaxTime = 2.0f;

    [SerializeField] 
	private List<GameObject> m_patrollingNodes = new List<GameObject> ();

	// Use this for initialization
	protected override void Awake()
	{
		base.Awake ();

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

        //Assigning transitons
        patrollingState.AddInterruptTransition(TargetSightTransition.CreateComponent(gameObject, m_detectionRange), moveTowardsState);

        moveTowardsState.AddInterruptTransition(NotTransition.CreateComponent(gameObject, TargetSightTransition.CreateComponent(gameObject, m_detectionRange)), investigateState);
        moveTowardsState.AddInterruptTransition(TargetWithinRangeTransition.CreateComponent(gameObject, m_attackingRange), attackingState);

        investigateState.AddEndTransition(BaseTransition.CreateComponent(gameObject), idleState);
        investigateState.AddInterruptTransition(TargetSightTransition.CreateComponent(gameObject, m_detectionRange), moveTowardsState);

        idleState.AddEndTransition(BaseTransition.CreateComponent(gameObject), patrollingState);
        idleState.AddInterruptTransition(TargetSightTransition.CreateComponent(gameObject, m_detectionRange), moveTowardsState);

        attackingState.AddEndTransition(NotTransition.CreateComponent(gameObject, TargetWithinRangeTransition.CreateComponent(gameObject, m_attackingRange)), moveTowardsState);

        //Setup intial state
        GetComponent<StateManager>().SetInitialState(patrollingState);
	}
}
