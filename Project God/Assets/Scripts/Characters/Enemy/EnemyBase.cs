using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Character
{
    private GameObject m_player = null;

    // Use this for initialization
    protected override void Awake()
    {
		base.Awake ();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
  
    }

    public override Vector3 GetAimingDir(Vector3 projectileSpawnPos)
    {
        Vector3 aimingDir = Vector3.zero;
        if (m_player != null)
            aimingDir = m_player.transform.position - transform.TransformPoint(projectileSpawnPos);
        //As this is all side on , remove z component in vector
        aimingDir.z = 0;
        return aimingDir;
    }

    public GameObject GetTarget()
    {
        return m_player;
    }
}
