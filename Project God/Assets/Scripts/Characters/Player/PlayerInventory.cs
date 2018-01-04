using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [SerializeField] private List<GameObject> m_weaponList = new List<GameObject>();
    private int m_currentWeaponIndex = 0;

    [SerializeField] private float m_swappingTime = 0.3f;
    private bool m_swappingWeapon = false;

    private GameObject m_playerModel = null;

    // Use this for initialization
    void Start ()
    {
        m_playerModel = GetComponent<Player>().m_playerModel;

        //Set all weapons to inactive, enable selected
        for (int i = 0; i < m_weaponList.Count; i++)
        {
            m_weaponList[i] = Instantiate(m_weaponList[i], m_playerModel.transform);
            m_weaponList[i].SetActive(false);
            m_weaponList[i].tag = gameObject.tag;
        }
    
        m_weaponList[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.m_instance.GetInputBool("SwapWeapon") && !m_swappingWeapon)
            SwapWeapon();
    }

    void SwapWeapon()
    {
        //Move to next weapon slot
        m_weaponList[m_currentWeaponIndex].SetActive(false);
        m_currentWeaponIndex++;
        if (m_currentWeaponIndex > m_weaponList.Count -1)
        m_currentWeaponIndex = 0;
        m_weaponList[m_currentWeaponIndex].SetActive(true);

        //Disable swapping for a period of time
        m_swappingWeapon = true;
        Invoke("EnableSwapping", m_swappingTime);
    }

    void EnableSwapping()
    {
        m_swappingWeapon = false;
    }
}
