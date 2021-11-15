using System;
using System.Collections;
using System.Collections.Generic;
using FXnRXn.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace TD.gameeconomics
{
    public class RewardWeekManager : MonoBehaviour
    {
	#region Variable

	
	private string url = "www.google.com";
	private string urlDate = "http://worldclockapi.com/api/json/est/now";
	private string sDate = "";
	
	[Space(15)]
	[SerializeField] private Button[] _allDayButton; 
	[SerializeField] private GameObject[] _allDayLock;
	[SerializeField] private GameObject[] _allDayClear;
	[SerializeField] private Image[] _Icon;
	[SerializeField] private TMP_Text[] _Amount;
	
	
	[Header("UI Button :")] 
	[SerializeField] private Button rewardWeekCancelButton;
	
	[Header("UI Screen :")] 
	[SerializeField] private List<UI_Screen> _UIMenus;
	
	
	
	
	private const string rewardWeekPanel = "Reward Week";
	private SO_RewardWeek _rewardWeek = null;
	
	#endregion

	#region Function

	private void Awake()
	{
	}

	public void StartRewardWeek()
	{
		StartCoroutine(CheckInternet());
	}

	private void Start()
	{
		if (_rewardWeek == null)
		{
			_rewardWeek = SO_DataController.Singleton.rewardWeek;
		}

		if (!PlayerPrefs.HasKey(GlobalData.RewardWeek))
		{
			PlayerPrefs.SetInt(GlobalData.RewardWeek, 0);
		}
		
		//  --------------------------------------------------
		ResetUI();

		UpgradeRewardDaily();

		ButtonPress();
		
		if (PlayerPrefs.GetInt(GlobalData.firstTimeSignInAward) == 1)
		{
			StartCoroutine(CheckInternet());
		}
	}

	private void OnEnable()
	{
		EventManager.StartListening("OpenWeekRewardPanel", ShowPanel);
	}

	private void OnDisable()
	{
		EventManager.StopListening("OpenWeekRewardPanel", ShowPanel);
	}










	private IEnumerator CheckInternet()
	{
		WWW www = new WWW(url);

		yield return www;

		if (string.IsNullOrEmpty(www.text))
		{
			Debug.Log("Not Connect Internet");
		}
		else
		{
			Debug.Log("Success Internet");
			StartCoroutine(CheckDate());
		}
	}

	private IEnumerator CheckDate()
	{
		WWW www = new WWW(urlDate);
		yield return www;

		string[] splitDate = www.text.Split(new string[] {"currentDateTime\":\"" }, StringSplitOptions.None);

		sDate = splitDate[1].Substring(0, 10);

		Debug.Log(sDate);
		DailyCheck();
	}
	
	void DailyCheck()
	{
		int index = PlayerPrefs.GetInt(GlobalData.RewardWeek, 0);
		string dateOld = PlayerPrefs.GetString("PlayDateOld");

		if (string.IsNullOrEmpty(dateOld) && !_rewardWeek.rewardWeekDatas[index].weekDatas[0].isclaimed) // Day 1
		{
			ResetUI();
			// DONE : First Game
			// DONE : First Reward

			RewardButtonReady(0);
			PlayerPrefs.SetString("PlayDateOld", sDate);
			EventManager.TriggerEvent("OpenWeekRewardPanel");
			
			
		}
		else
		{
			
			DateTime _dateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
			DateTime _dateOld = Convert.ToDateTime(dateOld);

			TimeSpan diff = _dateNow.Subtract(_dateOld);
			
			//Debug.Log("other day" + _dateOld + " == " + _dateNow + " diff = "+ diff.Days);
			if (diff.Days >= 1 && !_rewardWeek.rewardWeekDatas[index].weekDatas[1].isclaimed)// Day 2
			{
				RewardButtonReady(1);
				//PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
				EventManager.TriggerEvent("OpenWeekRewardPanel");
			}
			if (diff.Days >= 2 && !_rewardWeek.rewardWeekDatas[index].weekDatas[2].isclaimed)// Day 3
			{
				RewardButtonReady(2);
				//PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
				EventManager.TriggerEvent("OpenWeekRewardPanel");
			}
			if (diff.Days >= 3 && !_rewardWeek.rewardWeekDatas[index].weekDatas[3].isclaimed)// Day 4
			{
				RewardButtonReady(3);
				//PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
				EventManager.TriggerEvent("OpenWeekRewardPanel");
			}
			if (diff.Days >= 4 && !_rewardWeek.rewardWeekDatas[index].weekDatas[4].isclaimed)// Day 5
			{
				RewardButtonReady(4);
				//PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
				EventManager.TriggerEvent("OpenWeekRewardPanel");
			}
			
			if (diff.Days >= 5 && !_rewardWeek.rewardWeekDatas[index].weekDatas[5].isclaimed)// Day 6
			{
				RewardButtonReady(5);
				//PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
				EventManager.TriggerEvent("OpenWeekRewardPanel");
			}
			if (diff.Days >= 6 && !_rewardWeek.rewardWeekDatas[index].weekDatas[6].isclaimed)// Day 7
			{
				RewardButtonReady(6);
				//PlayerPrefs.SetString("PlayDateOld", _dateNow.ToString());
				EventManager.TriggerEvent("OpenWeekRewardPanel");
			}
			if (diff.Days >= 7)
			{
				int temp = PlayerPrefs.GetInt(GlobalData.RewardWeek, 0) + 1;
				PlayerPrefs.SetInt(GlobalData.RewardWeek, temp);
				
				int tempspinWheel = PlayerPrefs.GetInt(GlobalData.spinWheel, 0) + 1;
				PlayerPrefs.SetInt(GlobalData.spinWheel, tempspinWheel);
				
				ResetUI();
				// TODO : Delete date Playerprefs
				PlayerPrefs.DeleteKey("PlayDateOld");
				
				StartCoroutine(CheckInternet());
			}

		}
	}
	


	void ShowPanel()
	{
		ShowMenu(rewardWeekPanel);
	}

	void HidePanel()
	{
		CloseMenu(rewardWeekPanel);
	}

	void UpgradeRewardDaily()
	{
		int index = PlayerPrefs.GetInt(GlobalData.RewardWeek, 0);
		
		
		// TODO : DAY 1-6

		for (int i = 0; i < 6; i++)
		{
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].type == WeekRewardType.Coin)
			{
				_Icon[i].sprite = _rewardWeek.rewardWeekDatas[index].weekDatas[i].rewardSprite;
				_Amount[i].text = _rewardWeek.rewardWeekDatas[index].weekDatas[i].amount.ToString();
			}
			
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].type == WeekRewardType.Energy)
			{
				_Icon[i].sprite = _rewardWeek.rewardWeekDatas[index].weekDatas[i].rewardSprite;
				_Amount[i].text = _rewardWeek.rewardWeekDatas[index].weekDatas[i].amount.ToString();
			}
			
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].type == WeekRewardType.Gem)
			{
				_Icon[i].sprite = _rewardWeek.rewardWeekDatas[index].weekDatas[i].rewardSprite;
				_Amount[i].text = _rewardWeek.rewardWeekDatas[index].weekDatas[i].amount.ToString();
			}
			
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].type == WeekRewardType.NormalChest)
			{
				_Icon[i].sprite = _rewardWeek.rewardWeekDatas[index].weekDatas[i].rewardSprite;
				_Amount[i].text = _rewardWeek.rewardWeekDatas[index].weekDatas[i].amount.ToString();
			}
		}
		
		// TODO : DAY 7
		_Icon[6].sprite = _rewardWeek.rewardWeekDatas[index].rewardSprite;
		_Amount[6].text = "Special Chest x" + _rewardWeek.rewardWeekDatas[index].amount.ToString();
		
		for (int i = 0; i < 6; i++)
		{
			// Lock
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].isLocked)
			{
				_allDayLock[i].SetActive(true);
			}
			else
			{
				_allDayLock[i].SetActive(false);
			}
			
			
			// interactable
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].readytoclaim)
			{
				_allDayButton[i].interactable = true;
			}
			else
			{
				_allDayButton[i].interactable = false;
			}
			
			// clear obj
			
			if (_rewardWeek.rewardWeekDatas[index].weekDatas[i].isclaimed)
			{
				_allDayClear[i].SetActive(true);
			}
			else
			{
				_allDayClear[i].SetActive(false);
			}
		}
		
		// Lock 7
		if (_rewardWeek.rewardWeekDatas[index].isLocked)
		{
			_allDayLock[6].SetActive(true);
		}
		else
		{
			_allDayLock[6].SetActive(false);
		}
			
			
		// interactable 7
		if (_rewardWeek.rewardWeekDatas[index].readytoclaim)
		{
			_allDayButton[6].interactable = true;
		}
		else
		{
			_allDayButton[6].interactable = false;
		}
			
		// clear obj 7
			
		if (_rewardWeek.rewardWeekDatas[index].isclaimed)
		{
			_allDayClear[6].SetActive(true);
		}
		else
		{
			_allDayClear[6].SetActive(false);
		}
		
		
		

	}

	void ButtonPress()
	{
		int index = PlayerPrefs.GetInt(GlobalData.RewardWeek, 0);
		
		rewardWeekCancelButton.onClick.RemoveAllListeners();
		rewardWeekCancelButton.onClick.AddListener(() =>
		{
			HidePanel();
			if (_rewardWeek.rewardWeekDatas[index].allClaimed)
			{
				int temp = PlayerPrefs.GetInt(GlobalData.RewardWeek, 0) + 1;
				PlayerPrefs.SetInt(GlobalData.RewardWeek, temp);
				
				int tempspinWheel = PlayerPrefs.GetInt(GlobalData.spinWheel, 0) + 1;
				PlayerPrefs.SetInt(GlobalData.spinWheel, tempspinWheel);
				
				ResetUI();
				// TODO : Delete date Playerprefs
			}
		});
		
		// TODO : Day 1
		_allDayButton[0].onClick.RemoveAllListeners();
		_allDayButton[0].onClick.AddListener(() =>
		{
			ButtonTapWork(_rewardWeek.rewardWeekDatas[index].weekDatas[0].type, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[0].amount, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[0].chest);
			
			
			_rewardWeek.rewardWeekDatas[index].weekDatas[0].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[0].interactable = false;
		});
		
		// TODO : Day 2
		_allDayButton[1].onClick.RemoveAllListeners();
		_allDayButton[1].onClick.AddListener(() =>
		{
			ButtonTapWork(_rewardWeek.rewardWeekDatas[index].weekDatas[1].type, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[1].amount, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[1].chest);
			
			_rewardWeek.rewardWeekDatas[index].weekDatas[1].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[1].interactable = false;
		});
		
		// TODO : Day 3
		_allDayButton[2].onClick.RemoveAllListeners();
		_allDayButton[2].onClick.AddListener(() =>
		{
			ButtonTapWork(_rewardWeek.rewardWeekDatas[index].weekDatas[2].type, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[2].amount, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[2].chest);
			
			_rewardWeek.rewardWeekDatas[index].weekDatas[2].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[2].interactable = false;
		});
		
		// TODO : Day 4
		_allDayButton[3].onClick.RemoveAllListeners();
		_allDayButton[3].onClick.AddListener(() =>
		{
			ButtonTapWork(_rewardWeek.rewardWeekDatas[index].weekDatas[3].type, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[3].amount, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[3].chest);
			
			_rewardWeek.rewardWeekDatas[index].weekDatas[3].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[3].interactable = false;
		});
		
		// TODO : Day 5
		_allDayButton[4].onClick.RemoveAllListeners();
		_allDayButton[4].onClick.AddListener(() =>
		{
			ButtonTapWork(_rewardWeek.rewardWeekDatas[index].weekDatas[4].type, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[4].amount, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[4].chest);
			
			_rewardWeek.rewardWeekDatas[index].weekDatas[4].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[4].interactable = false;
		});
		
		// TODO : Day 6
		_allDayButton[5].onClick.RemoveAllListeners();
		_allDayButton[5].onClick.AddListener(() =>
		{
			ButtonTapWork(_rewardWeek.rewardWeekDatas[index].weekDatas[5].type, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[5].amount, 
				_rewardWeek.rewardWeekDatas[index].weekDatas[5].chest);
			
			_rewardWeek.rewardWeekDatas[index].weekDatas[5].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[5].interactable = false;
		});
		
		// TODO : Day 7
		_allDayButton[6].onClick.RemoveAllListeners();
		_allDayButton[6].onClick.AddListener(() =>
		{
			if (_rewardWeek.rewardWeekDatas[index].chest != null)
			{
				if (SO_DataController.Singleton != null)
				{
					SO_DataController.Singleton.AddItemInventory(_rewardWeek.rewardWeekDatas[index].chest);
				}
			}
			
			_rewardWeek.rewardWeekDatas[index].isclaimed = true;
			UpgradeRewardDaily();
			_allDayButton[6].interactable = false;

			_rewardWeek.rewardWeekDatas[index].allClaimed = true;
		});
	}


	void ButtonTapWork(WeekRewardType type , int amount, SO_InventoryItemObject item)
	{
		switch (type)
		{
			case WeekRewardType.Coin:
				GameCoinGemEnergyCount.CoinAddCount(amount);
				break;
			
			case WeekRewardType.Energy:
				GameCoinGemEnergyCount.EnergyAddCount(amount);
				break;
			
			case WeekRewardType.Gem:
				GameCoinGemEnergyCount.GemAddCount(amount);
				break;
			
			case WeekRewardType.NormalChest:
				if (item != null )
				{
					if (SO_DataController.Singleton != null)
					{
						SO_DataController.Singleton.AddItemInventory(item);
					}
				}
				break;
		}
	}




	void RewardButtonReady(int buttonIndex)
	{
		int index = PlayerPrefs.GetInt(GlobalData.RewardWeek, 0);
		
		switch (buttonIndex)
		{
			case 0:
				_rewardWeek.rewardWeekDatas[index].weekDatas[0].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].weekDatas[0].isLocked = false;
				break;
			case 1:
				_rewardWeek.rewardWeekDatas[index].weekDatas[1].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].weekDatas[1].isLocked = false;
				break;
			case 2:
				_rewardWeek.rewardWeekDatas[index].weekDatas[2].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].weekDatas[2].isLocked = false;
				break;
			case 3:
				_rewardWeek.rewardWeekDatas[index].weekDatas[3].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].weekDatas[3].isLocked = false;
				break;
			case 4:
				_rewardWeek.rewardWeekDatas[index].weekDatas[4].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].weekDatas[4].isLocked = false;
				break;
			case 5:
				_rewardWeek.rewardWeekDatas[index].weekDatas[5].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].weekDatas[5].isLocked = false;
				break;
			case 6:
				_rewardWeek.rewardWeekDatas[index].readytoclaim = true;
				_rewardWeek.rewardWeekDatas[index].isLocked = false;
				break;
		}

		UpgradeRewardDaily();
	}
	

	void ResetUI()
	{
		//button Lock
		foreach (var button in _allDayLock)
		{
			button.SetActive(true);
		}
		
		// botton interactable
		foreach (var button in _allDayButton)
		{
			button.interactable = false;
		}
		
		// Clear Obj
		foreach (var button in _allDayClear)
		{
			button.SetActive(false);
		}
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


