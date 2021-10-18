using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TD.gameeconomics
{
	public class UI_GamePlayManager : MonoBehaviour
	{
		#region Variables

		[Header("Button :")] 
		[SerializeField] private Button winButton;
		[SerializeField] private Button winCloseButton;
		[SerializeField] private Button lostButton;
		[SerializeField] private Button lostCloseButton;

		[Space(15)] 
		[SerializeField] private Button win_claimButton;
		[SerializeField] private Button win_claimDoubleButton;
		[SerializeField] private Button lost_gemButton;
		[SerializeField] private Button lost_adsButton;

		[Header("UI Screen :")] 
		[SerializeField]private List<UI_Screen> _UIMenus;


		private int xp;
		public int XPVal
		{
			get => xp;
			set => xp = value;
		}
		
		private int coin;
		public int CoinVal
		{
			get => coin;
			set => coin = value;
		}
		
		private int gem;
		public int GemVal
		{
			get => gem;
			set => gem = value;
		}

		public SO_InventoryItemObject so_inventoryObj;
		
		
		
		
		
		#endregion

		#region Functions

		private void OnEnable()
		{
			InitUI();
		}

		void InitUI()
		{
			CloseMenu(GlobalData.winScene);
			CloseMenu(GlobalData.lostScene);
		}


		private void Start()
		{
			winButton.onClick.RemoveAllListeners();
			winButton.onClick.AddListener(() =>
			{
				ShowMenu(GlobalData.winScene);
				
				//Send Leaderboard Value
				if (SO_DataController.Singleton._authenticationType == AuthenticationType.PlayfabServer)
				{
					int s = (int) Random.Range(1000, 32000);
					SendLeaderBoard(s);
				}
				


			});
			
			lostButton.onClick.RemoveAllListeners();
			lostButton.onClick.AddListener(() =>
			{
				ShowMenu(GlobalData.lostScene);
			});
			
			winCloseButton.onClick.RemoveAllListeners();
			winCloseButton.onClick.AddListener(() =>
			{
				//MDisableAllScreens();
				if (LevelLoadController.Singleton != null)
				{
					LevelLoadController.Singleton.LoadTheLevel("Main");
				}
			});
			
			lostCloseButton.onClick.RemoveAllListeners();
			lostCloseButton.onClick.AddListener(() =>
			{
				//MDisableAllScreens();
				if (LevelLoadController.Singleton != null)
				{
					LevelLoadController.Singleton.LoadTheLevel("Main");
				}
			});
			
			//WIN BUTTON
			win_claimButton.onClick.RemoveAllListeners();
			win_claimButton.onClick.AddListener(() =>
			{
				GamePlayerLevelProgress.AddPlayerXPLevel(XPVal);
				GameCoinGemEnergyCount.CoinAddCount(CoinVal);
				
				//Chest Type
				if (SO_DataController.Singleton != null)
				{
					SO_DataController.Singleton.AddItemInventory(so_inventoryObj);
				}
				
				this.Wait(0.5f, () =>
				{
					if (LevelLoadController.Singleton != null)
					{
						LevelLoadController.Singleton.LoadTheLevel("Main");
					}
				});
			});
			
			win_claimDoubleButton.onClick.RemoveAllListeners();
			win_claimDoubleButton.onClick.AddListener(() =>
			{
				// GamePlayerLevelProgress.AddPlayerXPLevel(XPVal);
				// GamePlayerLevelProgress.AddPlayerXPLevel(XPVal);
				// GameCoinGemEnergyCount.CoinAddCount(CoinVal);
				// GameCoinGemEnergyCount.CoinAddCount(CoinVal);
				// //Chest Type
				// if (SO_DataController.Singleton != null)
				// {
				// 	SO_DataController.Singleton.AddItemInventory(so_inventoryObj);
				// 	SO_DataController.Singleton.AddItemInventory(so_inventoryObj);
				// }
				//
				// this.Wait(0.5f, () =>
				// {
				// 	if (LevelLoadController.Singleton != null)
				// 	{
				// 		LevelLoadController.Singleton.LoadTheLevel("Main");
				// 	}
				// });
			});
			
			
			//Lost Button
			
			lost_gemButton.onClick.RemoveAllListeners();
			lost_gemButton.onClick.AddListener(() =>
			{
				if (GameCoinGemEnergyCount.IsGemReadyToReduce(GemVal))
				{
					GameCoinGemEnergyCount.GemRemoveCount(GemVal);
					
					this.Wait(0.5f, () =>
					{
						if (LevelLoadController.Singleton != null)
						{
							LevelLoadController.Singleton.LoadTheLevel("Main");
						}
					});
				}
				
				
			});
			
			
			
		}



		private void SendLeaderBoard(int val)
		{
			PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
				{
					Statistics = new List<StatisticUpdate> {
						new StatisticUpdate {
							StatisticName = "PlayerScore",
							Value = val
						}
					}
				}, result => OnStatisticUpdate(result), FailureCallback);
		}
		
		
		
		private void OnStatisticUpdate(UpdatePlayerStatisticsResult updateResult)
		{
			Debug.Log("Successfully submitted high score");
		}

		private void FailureCallback(PlayFabError error)
		{
			Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
			Debug.LogError(error.GenerateErrorReport());
		}
		
		

		#endregion
		
		#region UI MANAGEMENT

		public void ShowMenu(string name)
		{
			ShowMenuFunc(name, true);
		}
		
		public void ShowMenu(string name , bool isAllUIDisable)
		{
			ShowMenuFunc(name, isAllUIDisable);
		}
		
		private void ShowMenuFunc(string name, bool disableAllScreens){
			if(disableAllScreens) MDisableAllScreens();

			foreach (UI_Screen UI in _UIMenus){
				if (UI.UI_name == name) {
                
					if (UI.UI_Object != null) {
						UI.UI_Object.SetActive(true);
                    

					} else {
						Debug.Log ("no menu found with name: " + name);
					}
				}
			}
		}
    
		public void CloseMenu(string name){
			foreach (UI_Screen UI in _UIMenus){
				if (UI.UI_name == name)	UI.UI_Object.SetActive (false);
			}
		}
    
		public void MDisableAllScreens(){
			foreach (UI_Screen UI in _UIMenus){ 
				if(UI.UI_Object != null) 
					UI.UI_Object.SetActive(false);
				else 
					Debug.Log("Null ref found in UI with name: " + UI.UI_name);
			}
		}
		
		
		
		

		#endregion

	}
}
