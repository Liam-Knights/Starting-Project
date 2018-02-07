using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSightTransition : BaseTransition
{
    private GameObject m_target = null;
    private Character m_character = null;

    private float m_sightDistance = 0.0f;

    //Initialisation
    public static TargetSightTransition CreateComponent(GameObject parent, float sightDistance)
    {
        TargetSightTransition component = parent.AddComponent<TargetSightTransition>();
        component.Init(sightDistance);
        return component;
    }

    private void Init(float sightDistance)
    {
        m_sightDistance = sightDistance;
    }
    //End Initialisation

    // Use this for initialization
    void Start()
    {
        m_target = GetComponent<EnemyBase>().GetTarget();
        m_character = GetComponent<Character>();
    }

    // Update is called once per frame
    public override bool CheckTransition()
    {
        Vector3 lookDir = m_character.GetAimingDir(Vector3.zero);

        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(transform.position, lookDir, out hit, m_sightDistance))
        {
            if(hit.collider.gameObject.tag == "Player")
                return true;
        }
        return false;
    }
}
