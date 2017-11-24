using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private float m_damage = 1;
    private bool m_doingDamage = false;

    private List<GameObject> m_HitCharacters;


	// Use this for initialization
	void Start ()
    {
        m_HitCharacters = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (m_doingDamage)
        {
            if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") && other.gameObject.tag != gameObject.tag) // Only get charaters who arent on same side
            {
                if (!m_HitCharacters.Contains(other.gameObject))
                {
                    m_HitCharacters.Add(other.gameObject);
                    other.gameObject.GetComponent<Character>().TakeDamage(m_damage);
                }
            }
        }
    }


    public void EnableDamage()
    {
        m_doingDamage = true;
    }

    public void DisableDamage()
    {
        m_doingDamage = false;
        m_HitCharacters.Clear();
    }
}
