using System;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_ChestItem", menuName = "Scriptable Objects/Inventory/ChestItem")]
	public class SO_ChestItem : SO_InventoryItemObject
	{
		[Space(10)]
		public int coin;
		public GameObject coinPrefab_UI;
		[Space(5)]
		public int heroUpgradePoint;
		public HeroType heroType;
		public Sprite heroIcon;
		public GameObject heroPrefab_UI;
		[Space(5)]
		public int energy;
		public GameObject energyPrefab_UI;
		[Space(5)]
		public int gem;
		public GameObject gemPrefab_UI;
		
		void Awake()
		{
			inventory_ItemType = InventoryItemType.Default;
		}
	}
}

public enum HeroType
{
	None,
	Hero1,
	Hero2,
	Hero3
}
