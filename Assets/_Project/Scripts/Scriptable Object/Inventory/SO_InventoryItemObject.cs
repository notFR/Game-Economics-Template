using UnityEngine;

namespace TD.gameeconomics
{
	
	public abstract class SO_InventoryItemObject : ScriptableObject
	{
		public int ID;
		public InventoryItemType inventory_ItemType; // Item type
		public string name; //Item name
		public string typeName;
		public GameObject itemPrefab; // UI prefab
		public Sprite itemIcon;// Item Icon
		public int sellAmount;
		
		[TextArea(15, 20)]
		public string description;
	}//END CLASS
}

public enum InventoryItemType
{
	NormalChest, // 101
	GoldenChest, // 102
	XPChest, // 103
	XP, //104
	Coin, //105
	Energy, //106
	Weapon, //107
	Shield, //108
	Rune,// 109
	UpgradePoint,//110
	Hero,//111
	Default
}