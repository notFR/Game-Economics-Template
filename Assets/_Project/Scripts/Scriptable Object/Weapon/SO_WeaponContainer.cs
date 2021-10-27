using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "weapon_container", menuName = "Scriptable Objects/Weapon/Container")]
    public class SO_WeaponContainer : ScriptableObject
    {
	    public List<WeaponSlot> Container = new List<WeaponSlot>();

	    public void AddWeapon(SO_Weapon _weapon)
	    {
		    bool hasItem = false;
		    for (int i = 0; i < Container.Count; i++)
		    {
			    if (Container[i].weapon == _weapon)
			    {
				    hasItem = true;
				    break;
			    }
		    }

		    if (!hasItem)
		    {
			    Container.Add(new WeaponSlot(_weapon));
		    }
		    
	    }

	    public void RemoveWeapon(int index)
	    {
		    for (int i = 0; i < Container.Count; i++)
		    {
			    if (Container[i].weapon.ID == index)
			    {
				    Container.RemoveAt(i);
			    }
		    }
	    }
	    
    }

    [System.Serializable]
    public class WeaponSlot
    {
	    public SO_Weapon weapon;
	    
	    public WeaponSlot(SO_Weapon _weapon)
	    {
		    weapon = _weapon;
	    }
    }
}


