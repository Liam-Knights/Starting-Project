using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField] private List<GameObject> m_weaponList = new List<GameObject>();
    private int m_currentWeaponIndex = 0;

    [SerializeField] private float m_swappingTime = 0.3f;
    private bool m_swappingWeapon = false;

    // Use this for initialization
    void Start ()
    {
		GameObject m_characterModel = GetComponent<Character>().m_characterModel;

        //Set all weapons to inactive, enable selected
        for (int i = 0; i < m_weaponList.Count; i++)
        {
			m_weaponList[i] = Instantiate(m_weaponList[i], m_characterModel.transform);
            m_weaponList[i].tag = gameObject.tag;
			m_weaponList [i].GetComponentInChildren<BaseWeapon> ().SetParent (gameObject);
			m_weaponList[i].SetActive(false);
        }
    
        m_weaponList[0].SetActive(true);
    }

    public void SwapWeapon()
    {
        //Move to next weapon slot
		if (!m_swappingWeapon) {
			m_weaponList[m_currentWeaponIndex].SetActive(false);
			m_currentWeaponIndex++;
			if (m_currentWeaponIndex > m_weaponList.Count -1)
				m_currentWeaponIndex = 0;
			m_weaponList[m_currentWeaponIndex].SetActive(true);

			//Disable swapping for a period of time
			m_swappingWeapon = true;
			Invoke("EnableSwapping", m_swappingTime);
		}
    }

    void EnableSwapping()
    {
        m_swappingWeapon = false;
    }

	public GameObject GetCurrentWeapon()
	{
		return(m_weaponList [m_currentWeaponIndex]);
	}
}
