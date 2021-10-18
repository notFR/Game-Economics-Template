using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TD.gameeconomics
{
	public class UI_WinItem : MonoBehaviour
	{
		#region Variables
		[Header("Reff :")]
		[SerializeField] private Transform parent;

		[SerializeField] private Sprite normalChestIcon;
		[SerializeField] private Sprite goldenChestIcon;
		[SerializeField] private Sprite xpChestIcon;
		

		private SO_RewardItem rewardDATA;
		private UI_GamePlayManager ui_gameplayManager;

		#endregion

		#region Functions

		private void OnEnable()
		{
			ui_gameplayManager = Object.FindObjectOfType<UI_GamePlayManager>().GetComponent<UI_GamePlayManager>();
			rewardDATA = SO_DataController.Singleton.GetRewardItemPrefab();
			AddItemPrefab();
			
		}

		void AddItemPrefab()
		{
			if (SO_DataController.Singleton == null) return;


			int itemCount = rewardDATA.winRewardsData[PlayerPrefs.GetInt(GlobalData.playerCurrentLevel, 0)].rewardCount;
			int c = Random.Range(0, SO_DataController.Singleton.GetRewardCount());	
			
			switch (itemCount)
			{
				case 3:
					GameObject xpObj = Instantiate(rewardDATA.XPItem) as GameObject;
					AddValueToRewardItem(xpObj, c);
					xpObj.transform.SetParent(parent, false);
					GameObject coinObj = Instantiate(rewardDATA.coinItem) as GameObject;
					AddValueToRewardItem(coinObj, c);
					coinObj.transform.SetParent(parent, false);
					GameObject chestObj = Instantiate(rewardDATA.chestItem) as GameObject;
					AddValueToRewardItem(chestObj, c);
					chestObj.transform.SetParent(parent, false);
					break;
			}
		}

		void AddValueToRewardItem(GameObject obj , int c)
		{
			if(obj.GetComponent<UI_Item>() == null) return;

			
			
			RewardType _rType = obj.GetComponent<UI_Item>()._rewardType;

			switch (_rType)
			{
				case RewardType.XP:
					int valxp = SO_DataController.Singleton.GetRewardData(c).XPVal;
					ui_gameplayManager.XPVal = valxp;
					obj.GetComponent<UI_Item>()._xpText.text = valxp.ToString();
					break;
				
				case RewardType.Coin:
					int valc = SO_DataController.Singleton.GetRewardData(c).coinVal;
					ui_gameplayManager.CoinVal = valc;
					obj.GetComponent<UI_Item>()._coinText.text = valc.ToString();
					break;
				
				case RewardType.Chest:
					ChestType _chestT = SO_DataController.Singleton.GetRewardData(c).chestType;
					ui_gameplayManager.so_inventoryObj = SO_DataController.Singleton.GetRewardData(c).so_inventoryObj;
					switch (_chestT)
					{
						case ChestType.NormalChest:
							obj.GetComponent<UI_Item>()._chestIcon.sprite = normalChestIcon;
							obj.GetComponent<UI_Item>()._chestCountText.text = 1.ToString();
							break;
					}
					
					break;
			}
		}


		#endregion

	}
}
