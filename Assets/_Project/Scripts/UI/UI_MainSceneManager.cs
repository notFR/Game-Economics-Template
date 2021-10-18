using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace TD.gameeconomics
{
	public class UI_MainSceneManager : MonoBehaviour
	{
		#region Variables

		[Header("Panel :")] 
		[SerializeField] private GameObject mainMenuCanvas;
		[SerializeField] private GameObject arenaCanvas;
		[SerializeField] private GameObject inventoryCanvas;

		[Header("Button :")] 
		[SerializeField] private Button battleButton;
		[SerializeField] private Button arenaBackButton;
		[SerializeField] private Button inventoryButton;
		[SerializeField] private Button inventoryCloseButton;
		[Space(10)] 
		[SerializeField] private Button leaderboardButton;
		[SerializeField] private Button leaderboardBackButton;
		[Space(10)] 
		[SerializeField] private Button missionsButton;
		[SerializeField] private Button missionsBackButton;
		
		[Header("Player Info :")] 
		[SerializeField] private TMP_Text playerUserName;
		
		
		[Header("UI Screen :")] 
		[SerializeField]private List<UI_Screen> _UIMenus;


		private const string leaderboardPanel = "LeaderBoard Panel";
		private const string missionsPanel = "Missions";
		

		#endregion

		#region Functions

		private void Start()
		{
			//If Auth is Playfab Server
			if (SO_DataController.Singleton != null && SO_DataController.Singleton._authenticationType == AuthenticationType.PlayfabServer)
			{
				if (String.IsNullOrEmpty(PlayerPrefs.GetString(GlobalData.playerUsername)) )
				{
					playerUserName.text = "Guest";
				}
				else
				{
					string username = PlayerPrefs.GetString(GlobalData.playerUsername);
					playerUserName.text = username.ToString();
				}
			}
			Init();
		}

		void Init()
		{
			mainMenuCanvas.SetActive(true);
			arenaCanvas.SetActive(false);
			inventoryCanvas.SetActive(false);
			
			battleButton.onClick.RemoveAllListeners();
			battleButton.onClick.AddListener(() =>
			{
				mainMenuCanvas.SetActive(false);
				arenaCanvas.SetActive(true);
			});
			
			arenaBackButton.onClick.RemoveAllListeners();
			arenaBackButton.onClick.AddListener(() =>
			{
				mainMenuCanvas.SetActive(true);
				arenaCanvas.SetActive(false);
			});
			
			//Inventory Button
			inventoryButton.onClick.RemoveAllListeners();
			inventoryButton.onClick.AddListener(() =>
			{
				inventoryCanvas.SetActive(true);
			});
			
			inventoryCloseButton.onClick.RemoveAllListeners();
			inventoryCloseButton.onClick.AddListener(() =>
			{
				inventoryCanvas.SetActive(false);
			});
			
			// Leaderboard
			leaderboardButton.onClick.RemoveAllListeners();
			leaderboardButton.onClick.AddListener(() =>
			{
				ShowMenu(leaderboardPanel);
			});
			
			leaderboardBackButton.onClick.RemoveAllListeners();
			leaderboardBackButton.onClick.AddListener(() =>
			{
				CloseMenu(leaderboardPanel);
			});
			
			// Missions
			missionsButton.onClick.RemoveAllListeners();
			missionsButton.onClick.AddListener(() =>
			{
				ShowMenu(missionsPanel);
			});
			missionsBackButton.onClick.RemoveAllListeners();
			missionsBackButton.onClick.AddListener(() =>
			{
				CloseMenu(missionsPanel);
			});
		}
		
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
		
		
		
		
		#endregion

	}
}
