using System;
using System.Collections.Generic;
using FXnRXn;
using UnityEngine;

namespace TD.gameeconomics
{
	public class SO_DataController : MonoBehaviour
	{
		#region Singleton
		public static SO_DataController Singleton;
		
		void Awake()
		{
			OnAwake();
			if (Singleton == null)
			{
				Singleton = this;
			}
			else
			{
				Destroy(this.gameObject);
			}
			DontDestroyOnLoad(this.gameObject);

			//DataGet();
		}
		#endregion
		
		
		#region Variables

		public AuthenticationType _authenticationType;
		
		//Per Level Progeress Reward SO
		
		//Lost Data SO
		
		//WIN Reward Data SO
		private Dictionary<int, Reward> winRewardDict;
		private SO_RewardItem so_rewardItem;

		
		//Player Level Progress SO
		private Dictionary<int, PlayerLevelProgressData> playerProgressDataDictionary;
		private SO_PlayerLevelProgress so_palyerProgress = null;
		
		// Inventory
		private SO_InventorySlotObject inventorySystem = null;
		public SO_InventorySlotObject InventorySystem => inventorySystem;
		
		//Mission
		private SO_ChallangeMission challangeMission_so;
		public SO_ChallangeMission ChallangeMissionData => challangeMission_so;
		
		
		#endregion

		#region Functions

		void OnAwake()
		{
			
		}

		private void Start()
		{
			if (playerProgressDataDictionary == null)
			{
				CreatePlayerProgressDictionary();
			}

			if (winRewardDict == null)
			{
				CreateRewardItemDictionary();
			}
			
		}

		void DataGet()
		{
			so_palyerProgress = Resources.Load<SO_PlayerLevelProgress>("Scriptable Data/so_playerLevelProgress");
			so_rewardItem = Resources.Load<SO_RewardItem>("Scriptable Data/so_rewardItem");
			if (challangeMission_so == null)
			{
				challangeMission_so = Resources.Load<SO_ChallangeMission>("Scriptable Data/Mission/so_ChallangeMission");
			}
			
			if (inventorySystem == null)
			{
				inventorySystem = Resources.Load<SO_InventorySlotObject>("Scriptable Data/Inventory/Inventory System");
			}
			
		}

		private void OnEnable()
		{
			DataGet();
			CreatePlayerProgressDictionary();
			CreateRewardItemDictionary();
		}


		private void Update()
		{
			AddDataForChallangeMission();
		}


		#region Player Progress
		
		private void CreatePlayerProgressDictionary()
		{
			playerProgressDataDictionary = new Dictionary<int, PlayerLevelProgressData>();

			foreach (PlayerLevelProgressData progressData in so_palyerProgress.playerProgressData)
			{
				playerProgressDataDictionary.Add(progressData.Id, progressData);
			}
		}

		public PlayerLevelProgressData GetPlayerProgressData(int dataCode)
		{
			PlayerLevelProgressData progressData;
			if (playerProgressDataDictionary.TryGetValue(dataCode, out progressData))
			{
				return progressData;
			}
			else
			{
				return null;
			}
			
		}

		// public void SetPlayerProgressCurrentXP(int datacode, int valueCurrentXP)
		// {
		// 	foreach (PlayerLevelProgressData progressData in so_palyerProgress.playerProgressData)
		// 	{
		// 		if (progressData.playerLevel == datacode)
		// 		{
		// 			progressData.levelCurrentXP = valueCurrentXP;
		// 		}
		// 	}
		// 	
		// }

		#endregion

		#region WIN Reward Item

		public SO_RewardItem GetRewardItemPrefab()
		{
			if (so_rewardItem != null)
			{
				return so_rewardItem;
			}
			else
			{
				return null;
			}
		}
		
		
		private void CreateRewardItemDictionary()
		{
			winRewardDict = new Dictionary<int, Reward>();

			foreach (Reward progressData in so_rewardItem.winRewardsData[PlayerPrefs.GetInt(GlobalData.playerCurrentLevel, 0)].reward)
			{
				winRewardDict.Add(progressData.Id, progressData);
			}
		}

		public Reward GetRewardData(int dataCode)
		{
			Reward progressData;
			if (winRewardDict.TryGetValue(dataCode, out progressData))
			{
				return progressData;
			}
			else
			{
				return null;
			}
			
		}

		public int GetRewardCount()
		{
			return winRewardDict.Count;
		}
		

		#endregion

		#region Inventory

		public void AddItemInventory(SO_InventoryItemObject item)
		{
			GetSetItem.SetItem(item);
			GetItem();
		}

		public void GetItem()
		{
			var item = GetSetItem.GetItem();

			if (item)
			{
				inventorySystem.AddItem(item, 1);
			}
		}

		#endregion

		#region AddPlayerStatisticsData

		void AddDataForChallangeMission()
		{
			int index = PlayerPrefs.GetInt(GlobalData.challangeMission);
			ChallangeMissionSO chMissionData = challangeMission_so.ChallangeMissionData[index];

			foreach (var chData in chMissionData.missionData)
			{
				switch (chData.missionType)
				{
					case MissionType.PlayerWinCount:
						chData.challangeM_currentCount = PlayerStatistics.GetPlayerWinCount();
						break;
				}
			}
			
		}

		#endregion
		

		#endregion

	}
	
	
	#region Inventory

	public static class GetSetItem
	{
		private static SO_InventoryItemObject item;

		public static void SetItem(SO_InventoryItemObject _item)
		{
			item = _item;
		}

		public static SO_InventoryItemObject GetItem()
		{
			return item;
		}
	}
	
	#endregion

	
}
