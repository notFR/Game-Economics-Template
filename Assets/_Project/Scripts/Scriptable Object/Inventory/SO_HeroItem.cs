using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_HeroItem", menuName = "Scriptable Objects/Inventory/HeroItem")]
	public class SO_HeroItem : SO_InventoryItemObject
	{
		[Space(10)]
		public int heroHealth;
		public GameObject healthPrefab_UI;
		
		[Space(5)]
		public int defenseHealth;
		public GameObject defensePrefab_UI;
		[Space(5)]
		public int attackDamage;
		public GameObject attackPrefab_UI;
		
		void Awake()
		{
			inventory_ItemType = InventoryItemType.Default;
		}

	}
}
