using System;
using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
    public class WeaponDataManager : MonoBehaviour
    {

	    #region Singleton

	    public static WeaponDataManager singleton;

	    private void Awake()
	    {
		    OnAwake();
		    if (singleton == null)
		    {
			    singleton = this;
		    }
	    }

	    #endregion
	    
	    
	#region Variable
	
	

	public List<WeaponData> weaponData = new List<WeaponData>();

	#endregion

	#region Function

	void OnAwake()
	{
		
	}

	#endregion

    }

    public static class WeaponSetup
    {
	    public static void AddWeaponItem(string name)
	    {
		    foreach (var data in WeaponDataManager.singleton.weaponData)
		    {
			    if (data.weapon_name == name)
			    {
				    if (SO_DataController.Singleton != null)
				    {
					    SO_DataController.Singleton.weaponC.AddWeapon(data.weapon_so);
				    }
				    
			    }
		    }
		    
	    }

	    public static SO_Weapon GetWeaponItem()
	    {
		    foreach (var weaponcontainer in SO_DataController.Singleton.weaponC.Container)
		    {
			    return weaponcontainer.weapon;
		    }

		    return null;
	    }

	    public static void RemoveWeaponItem()
	    {
		    
	    }
    }

    [System.Serializable]
    public class WeaponData
    {
	    public string weapon_name;
	    public SO_Weapon weapon_so;
    }
    
}


