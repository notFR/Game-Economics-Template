using System;
using System.Collections.Generic;
using FXnRXn.Manager;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TD.gameeconomics
{
	public class MainSceneUIManager : MonoBehaviour
	{
		#region Variables

		[Header("First Time Sign In Reward")] 
		[SerializeField]private GameObject _energyRewardUI;
		[SerializeField]private GameObject _coinRewardUI;
		[SerializeField]private GameObject _gemRewardUI;
		
		[SerializeField]private Button _energyRewardOkBtn;
		[SerializeField]private Button _coinRewardOkBtn;
		[SerializeField]private Button _gemRewardOkBtn;

		[Header("UI Screen :")] 
		[SerializeField]private List<UI_Screen> _UIMenus;

		private RewardWeekManager _rewardWeekManager;

		#endregion

		#region Functions

		private void Start()
		{
			Init();
			FirstTimeRewardButton();
		}

		void Init()
		{
			_rewardWeekManager = Object.FindObjectOfType<RewardWeekManager>().GetComponent<RewardWeekManager>();
		}

		private void OnEnable()
		{
			
		}

		private void OnDisable()
		{
			
		}


		#region First Time Reward Claim

		void FirstTimeRewardButton()
		{
			if (PlayerPrefs.GetInt(GlobalData.firstTimeSignInAward, 0) == 0 || !PlayerPrefs.HasKey(GlobalData.firstTimeSignInAward))
			{
				ShowMenu(GlobalData.energyUI);
			}
			
			
			
			
			if (_energyRewardOkBtn == null || _coinRewardOkBtn == null || _gemRewardOkBtn == null) return;
			
			_energyRewardOkBtn.onClick.RemoveAllListeners();
			_energyRewardOkBtn.onClick.AddListener(() =>
			{
				CloseMenu(GlobalData.energyUI);
				EventManager.TriggerEvent(GlobalData.energyAdd, 20);
				ShowMenu(GlobalData.coinUI);
			});
			
			_coinRewardOkBtn.onClick.RemoveAllListeners();
			_coinRewardOkBtn.onClick.AddListener(() =>
			{
				CloseMenu(GlobalData.coinUI);
				EventManager.TriggerEvent(GlobalData.coinAdd, 1000);
				ShowMenu(GlobalData.gemUI);
			});
			
			_gemRewardOkBtn.onClick.RemoveAllListeners();
			_gemRewardOkBtn.onClick.AddListener(() =>
			{
				CloseMenu(GlobalData.gemUI);
				EventManager.TriggerEvent(GlobalData.gemAdd, 5);
				EventManager.TriggerEvent(GlobalData.firstRewardClaimComplete);
				PlayerPrefs.SetInt(GlobalData.firstTimeSignInAward, 1);
				this.Wait(0.5f, () =>
				{
					_rewardWeekManager.StartRewardWeek();
				});
				
			});

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
		

		#endregion

	}


	
}
