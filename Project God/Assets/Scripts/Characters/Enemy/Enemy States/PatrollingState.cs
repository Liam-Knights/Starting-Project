using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : BaseState
{
    [SerializeField]
    protected List<GameObject> m_patrollingNodes = new List<GameObject>();

	private float m_nodeClosingDistance = 1.0f;

    private int m_currentNode = 0;

	private Rigidbody m_rbCharacter = null;
	private float m_forwardVelocity = 0.0f;

	protected override void Start()
	{
		base.Start();
		m_rbCharacter = GetComponent<Rigidbody> ();
	}

    public override void StateInit()
    {
		
    }

    protected override void StateActions()
	{
        if (m_patrollingNodes.Count > 1) { // Move bewteen nodes
            if (Mathf.Abs(m_patrollingNodes[m_currentNode].transform.position.x - transform.position.x) < m_nodeClosingDistance || (m_rbCharacter.velocity.x > -0.1f && m_rbCharacter.velocity.x < 0.1f)) //Havent reached node yet on x axis, or velocity isnt changing(above or under target pos)
				m_currentNode = GetRandomNode();//Get random int excluding current pos
		}
        else if (m_patrollingNodes.Count == 1) //only one node to move towards
        {
            //if ((m_patrollingNodes [m_currentNode].transform.position - transform.position).magnitude < m_nodeClosingDistance)//Havent reached node yet TODO, Add idle state
        }

        //Set rotation
        if (m_rbCharacter.velocity.x > 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        else if (m_rbCharacter.velocity.x < 0)
            m_enemyBase.m_characterModel.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    //Physics actions
    public override void StateFixedActions()
    {
        if(m_patrollingNodes.Count != 0)
            VectorMaths.RigidbodyXVelocity(m_rbCharacter, m_patrollingNodes[m_currentNode].transform.position - transform.position, m_forwardVelocity);
    }

    public override void StateEnd()
    {
		m_rbCharacter.velocity = Vector3.zero;
    }

	private int GetRandomNode()
	{
		List<int> allPos = new List<int> ();
		for (int i = 0; i < m_patrollingNodes.Count; i++) 
		{
			if (i != m_currentNode)
				allPos.Add (i);
		}
		return(allPos[Random.Range(0, allPos.Count -1)]);
	}

	public void SetMovementVelocity(float speed)
	{
		m_forwardVelocity = speed;
	}

	public void SetNodes(List<GameObject> patrollingNodes)
	{
		m_patrollingNodes = patrollingNodes;
	}
}
