using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_rewardItem", menuName = "Scriptable Objects/Reward/WinReward")]
	public class SO_RewardItem : ScriptableObject
	{
		public GameObject XPItem;
		public GameObject coinItem;
		public GameObject chestItem;
		[SerializeField] public List<WinReward> winRewardsData;
	}
	
	[System.Serializable]
	public class WinReward
	{
		public int level;
		public int rewardCount;
		public List<Reward> reward;
	}
	
	[System.Serializable]
	public class Reward
	{
		public int Id;
		public bool isClaimed;
		public int XPVal;
		public int coinVal;
		public ChestType chestType;
		public SO_InventoryItemObject so_inventoryObj;

	}
}
