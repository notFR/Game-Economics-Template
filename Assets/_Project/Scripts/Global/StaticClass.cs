using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	
	[System.Serializable]
	public class UI_Screen
	{
		public string UI_name;
		public GameObject UI_Object;
	}
	
	
	[System.Serializable]
	public class PlayerLevelProgressData
	{
		public int Id;
		public int playerLevel;
		public int levelTargetXP;

	}

	
	
	
	
	// [System.Serializable]
	// public class StoreData
	// {
	// 	public int _Id;
	// 	public string _name;
	// 	public GameObject _obj;
	//
	// 	public void Setvalues(int id, string name, GameObject obj)
	// 	{
	// 		this._Id = id;
	// 		this._name = name;
	// 		this._obj = obj;
	// 	}
	// 	
	// 	public void RemoveValues()
	// 	{
	// 		this._Id = 0;
	// 		this._name = null;
	// 		this._obj = null;
	// 	}
	// }
}

