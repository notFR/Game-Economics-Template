using System;
using System.Collections.Generic;
using FXnRXn.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.gameeconomics
{
	public class UI_ArenaController : MonoBehaviour
	{
		#region Variables

		[Header("Coin to Play")] 
		[SerializeField]private int level1CoinNeed;
		[SerializeField]private int level2CoinNeed;
		[SerializeField]private int level3CoinNeed;
		[SerializeField]private int level4CoinNeed;
		[Space(15)]
		[SerializeField] private List<Arena> arena;

		[Space(15)] 
		[SerializeField] private TMP_Text arena1CoinNeedText;
		[SerializeField] private TMP_Text arena2CoinNeedText;
		[SerializeField] private TMP_Text arena3CoinNeedText;
		[SerializeField] private TMP_Text arena4CoinNeedText;

		#endregion

		#region Functions

		private void Start()
		{
			ArenaButton();
			
		}

		private void OnEnable()
		{
			Unlock();
			CoinPurchaseEligible();
			EventManager.StartListening(GlobalData.setArenaLock,SetArenaLockState);
		}

		private void OnDisable()
		{
			EventManager.StopListening(GlobalData.setArenaLock,SetArenaLockState);
		}

		public void SetArenaLockState(int state)
		{
			switch (state)
			{
				case 2:
					PlayerPrefs.SetInt(GlobalData.level2Lock, 1);
					break;
				case 3:
					PlayerPrefs.SetInt(GlobalData.level3Lock, 1);
					break;
				case 4:
					PlayerPrefs.SetInt(GlobalData.level4Lock, 1);
					break;
			}
		}


		void CoinPurchaseEligible()
		{
			if (EnergyCoinGemController.singleton != null &&
			    EnergyCoinGemController.singleton.IsCoinReady(level1CoinNeed))
			{
				arena1CoinNeedText.color = Color.white;
			}
			else
			{
				arena1CoinNeedText.color = Color.red;
			}
			
			if (EnergyCoinGemController.singleton != null &&
			    EnergyCoinGemController.singleton.IsCoinReady(level2CoinNeed))
			{
				arena2CoinNeedText.color = Color.white;
			}
			else
			{
				arena2CoinNeedText.color = Color.red;
			}
			
			if (EnergyCoinGemController.singleton != null &&
			    EnergyCoinGemController.singleton.IsCoinReady(level3CoinNeed))
			{
				arena3CoinNeedText.color = Color.white;
			}
			else
			{
				arena3CoinNeedText.color = Color.red;
			}
			
			if (EnergyCoinGemController.singleton != null &&
			    EnergyCoinGemController.singleton.IsCoinReady(level4CoinNeed))
			{
				arena4CoinNeedText.color = Color.white;
			}
			else
			{
				arena4CoinNeedText.color = Color.red;
			}
		}

		void Unlock()
		{
			if (PlayerPrefs.GetInt(GlobalData.level2Lock, 0) == 1)
			{
				arena[1].lockImage.gameObject.SetActive(false);
				arena[1].arenaLock = false;
			}
			if (PlayerPrefs.GetInt(GlobalData.level3Lock, 0) == 1)
			{
				arena[2].lockImage.gameObject.SetActive(false);
				arena[2].arenaLock = false;
			}
			if (PlayerPrefs.GetInt(GlobalData.level4Lock, 0) == 1)
			{
				arena[3].lockImage.gameObject.SetActive(false);
				arena[3].arenaLock = false;
			}
		}


		void ArenaButton()
		{
			for (int i = 0; i < arena.Count; i++)
			{
				switch (i)
				{
					case 0:
						arena[0].arenaButton.onClick.RemoveAllListeners();
						arena[0].arenaButton.onClick.AddListener(() =>
						{
							if (!arena[0].arenaLock)
							{
								if (LevelLoadController.Singleton != null)
								{
									if (EnergyCoinGemController.singleton != null && EnergyCoinGemController.singleton.IsCoinReady(level1CoinNeed))
									{
										EventManager.TriggerEvent(GlobalData.coinRemove, level1CoinNeed);
										this.Wait(.5f,()=>
										{
											LevelLoadController.Singleton.LoadTheLevel("Stage");
										});
										
										
									}
									
								}
							}
						});
						break;
					case 1:
						arena[1].arenaButton.onClick.RemoveAllListeners();
						arena[1].arenaButton.onClick.AddListener(() =>
						{
							if (!arena[1].arenaLock)
							{
								if (LevelLoadController.Singleton != null)
								{
									if (EnergyCoinGemController.singleton != null &&
									    EnergyCoinGemController.singleton.IsCoinReady(level2CoinNeed))
									{
										EventManager.TriggerEvent(GlobalData.coinRemove, level2CoinNeed);
										this.Wait(.5f,()=>
										{
											LevelLoadController.Singleton.LoadTheLevel("Stage");
										});
										
									}
									
									
									
								}
							}
						});
						break;
					
					case 2:
						arena[2].arenaButton.onClick.RemoveAllListeners();
						arena[2].arenaButton.onClick.AddListener(() =>
						{
							if (!arena[2].arenaLock)
							{
								if (LevelLoadController.Singleton != null)
								{
									if (EnergyCoinGemController.singleton != null &&
									    EnergyCoinGemController.singleton.IsCoinReady(level3CoinNeed))
									{
										EventManager.TriggerEvent(GlobalData.coinRemove, level3CoinNeed);
										this.Wait(.5f,()=>
										{
											LevelLoadController.Singleton.LoadTheLevel("Stage");
										});
										
									}
									
								}
							}
						});
						break;
					case 3:
						arena[3].arenaButton.onClick.RemoveAllListeners();
						arena[3].arenaButton.onClick.AddListener(() =>
						{
							if (!arena[3].arenaLock)
							{
								if (LevelLoadController.Singleton != null)
								{
									if (EnergyCoinGemController.singleton != null &&
									    EnergyCoinGemController.singleton.IsCoinReady(level4CoinNeed))
									{
										EventManager.TriggerEvent(GlobalData.coinRemove, level4CoinNeed);
										this.Wait(.5f,()=>
										{
											LevelLoadController.Singleton.LoadTheLevel("Stage");
										});
										
									}
								}
							}
						});
						break;
				}
			}
		}
		

		#endregion

	}

	[System.Serializable]
	public class Arena
	{
		public int arenaNo;
		public bool arenaLock;
		public Image lockImage;
		public Button arenaButton;
	}
}
