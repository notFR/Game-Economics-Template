using UnityEngine;

namespace TD.gameeconomics
{
	public static class GlobalData
	{


		public static string playerUsername;
		//PlayerPrefs
		public static string firstTimeSignInAward = "First Time Sign In Award"; // 0 = true, 1 = false 
		public static string playerCurrentLevel = "Player Current Level";
		public static string playerCurrentXP = "Player Current XP";
		public static string playerXPSliderVal = "PlayerXPSliderValue";
		
		public static string level2Lock = "Level 2 Lock";
		public static string level3Lock = "Level 3 Lock";
		public static string level4Lock = "Level 4 Lock";
		
		//Game Event
		public static string firstRewardClaimComplete = "First Reward Claim Complete";
		public static string setArenaLock = "Set Arena Lock";
		public static string SetPlayerCurrentXP = "Set Player Current XP";
		public static string playerLevelChange = "Player Level Change";
		
		
		// UI
		public static string energyUI = "EnergyUI";
		public static string coinUI = "CoinUI";
		public static string gemUI = "GemUI";
		public static string winScene = "WinUI";
		public static string lostScene = "LostUI";
		
		// Energy - Coin - Gem
		public static string energy = "ENERGY";
		public static string energyAdd = "ENERGY ADD"; //Event
		public static string energyRemove = "ENERGY REMOVE";//Event

		public static string coin = "COIN";//Playerprefs
		public static string coinAdd = "COIN ADD";//Event
		public static string coinRemove = "COIN REMOVE";//Event

		public static string gem = "GEM";//Playerprefs
		public static string gemAdd = "GEM ADD";//Event
		public static string gemRemove = "GEM REMOVE";//Event
		
		// CSV & Json Data Name
		public static string charLevelProgressData = "CharacterLevelData.csv";
		
		// Inventory Button event
		public static string sellButtonPress = "Sell Button Press";
		public static string selectButtonPress = "Select Button Press";
		public static string addItemInventory = "Add Item Inventory";
		
		// Add Inventory Name
		public static string normalChestName = "ChestNormal";
		
		//MISSION
		public static string challangeMission = "ChallangeMission";
		
		//Weapon
		public static string arrowLevel = "Arrow Level";
		public static string axeLevel = "axe Level";
		public static string hammerLevel = "hammer Level";
		public static string bombLevel = "bomb Level";
		
		// Authentication
		public static string AuthType = "AuthType"; // 0 = guest, 1 = playfab, 2 = facebook
		
	}
}
