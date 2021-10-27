using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.gameeconomics
{
    public class UI_WeaponManager : MonoBehaviour
    {
	#region Variable

	[SerializeField] private List<GameObject> _weaponContainer;
	[SerializeField] private int totalWeapon;
	
	[Header("UI Button:")] 
	[SerializeField] private Button _leftSwapButton;
	[SerializeField] private Button _rightSwapButton;

	[Header("UI :")] 
	[SerializeField] private TMP_Text weapon_Name;
	[SerializeField] private TMP_Text weapon_Desc;
	[Space(10)]
	[SerializeField] private TMP_Text weapon_Damage;
	[SerializeField] private TMP_Text weapon_attackRange;
	[Space(10)] 
	[SerializeField] private TMP_Text weapon_level;

	[Space(10)] 
	[SerializeField] private Image[] specialityIcon;

	[Space(10)] 
	[SerializeField] private TMP_Text ButtonLevelUpgradeText;

	[Header("UI Button:")] 
	[SerializeField] private Button upgradeButton;
	[SerializeField] private TMP_Text upgradeCoin;

	[Header("Setting :")] 
	[SerializeField] private Transform weaponParent;
	
	
	private float tweenDuration = 0.5f;

	private Vector3 leftSwapPos = new Vector3(-1400f, 0f, 0f);
	private Vector3 midPos = new Vector3(0f, 0f, 0f);
	private Vector3 rightSwapPos = new Vector3(1400f, 0f, 0f);

	private int weaponIndex;
	public Dictionary<int, SO_Weapon> _weapons = new Dictionary<int, SO_Weapon>();

	private int activeWeaponId;

	
	

	#endregion

	#region Function

	private void Awake()
	{
		if (!PlayerPrefs.HasKey("WeaponIndex"))
		{
			PlayerPrefs.SetInt("WeaponIndex", 1);
		}
		PlayerPrefs.SetInt(GlobalData.coin, 5000);
	}


	private void Start()
	{
		// Add item to list
		foreach (var weaponcontainer in SO_DataController.Singleton.weaponC.Container)
		{
			GameObject obj = Instantiate(weaponcontainer.weapon.uiObject) as GameObject;
			obj.transform.SetParent(weaponParent, false);
			_weaponContainer.Add(obj);
			
		}

		for (int i = 0; i < SO_DataController.Singleton.weaponC.Container.Count; i++)
		{
			_weapons.Add(i, SO_DataController.Singleton.weaponC.Container[i].weapon);
		}
		
		totalWeapon = _weaponContainer.Count;
		weaponIndex = PlayerPrefs.GetInt("WeaponIndex", 0);
		ButtonEligibleForPurchase();
		DefaultWeapon();
		
	}

	private void OnEnable()
	{
		//BUG Solved : Right press 1, 3,4  left 4, 2, 1

		
		_rightSwapButton.onClick.RemoveAllListeners();
		_rightSwapButton.onClick.AddListener(() =>
		{
			//ButtonEligibleForPurchase();
			NextWeapon();
			
		});
		_leftSwapButton.onClick.RemoveAllListeners();
		_leftSwapButton.onClick.AddListener(() =>
		{
			//ButtonEligibleForPurchase();
			PreviousWeapon();
			
		});
		
		upgradeButton.onClick.RemoveAllListeners();
		upgradeButton.onClick.AddListener(() =>
		{
			
			//ButtonEligibleForPurchase();
			UpgradeButton();
			// UI upgrade
			Upgrade();
		});
		
		
		weaponIndex = PlayerPrefs.GetInt("WeaponIndex", 0);
		DefaultWeapon();
		//ButtonEligibleForPurchase();
	}

	private void Update()
	{
		ButtonEligibleForPurchase();
	}


	private void OnDisable()
	{
		PlayerPrefs.SetInt("WeaponIndex", 1);
	}

	private void ButtonEligibleForPurchase()
	{
		SO_Weapon weaponUp;
		for (int i = 0; i < _weapons.Count; i++)
		{
			if (_weapons.TryGetValue(i, out weaponUp))
			{
				if (weaponUp.ID == activeWeaponId)
				{

					switch (weaponUp.weaponType)
					{
						case WeaponType.Arrow:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.arrowLevel, 0)].CoinToUpgrade))
							{
								upgradeCoin.color = Color.white;
								upgradeButton.interactable = true;
							}
							else
							{
								upgradeCoin.color = Color.red;
								upgradeButton.interactable = false;
							}
							break;
						
						case WeaponType.Axe:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.axeLevel, 0)].CoinToUpgrade))
							{
								upgradeCoin.color = Color.white;
								upgradeButton.interactable = true;
							}
							else
							{
								upgradeCoin.color = Color.red;
								upgradeButton.interactable = false;
							}
							
							break;
						
						case WeaponType.Hammer:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.hammerLevel, 0)].CoinToUpgrade))
							{
								upgradeCoin.color = Color.white;
								upgradeButton.interactable = true;
							}
							else
							{
								upgradeCoin.color = Color.red;
								upgradeButton.interactable = false;
								
							}
							
							break;
						
						case WeaponType.Bomb:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.bombLevel, 0)].CoinToUpgrade))
							{
								upgradeCoin.color = Color.white;
								upgradeButton.interactable = true;
							}
							else
							{
								upgradeCoin.color = Color.red;
								upgradeButton.interactable = false;
							}
							break;
					}
					
					
					
					
				}
			}
		}
	}


	private void DefaultWeapon()
	{
		_rightSwapButton.interactable = true;
		_leftSwapButton.interactable = false;
		
		foreach (var obj in _weaponContainer)
		{
			obj.transform.localPosition = leftSwapPos;
		}
		_weaponContainer[0].transform.DOLocalMove(midPos, tweenDuration);
		

		if (_weapons.TryGetValue(0, out weaponVal))
		{
			activeWeaponId = weaponVal.ID;
		}
		
			
			
		InitializeWeaponLevel();
		UpdateUIData(0);
		Upgrade();
	}

	private void InitializeWeaponLevel()
	{
		if (!PlayerPrefs.HasKey(GlobalData.arrowLevel))
		{
			PlayerPrefs.SetInt(GlobalData.arrowLevel, 0);
		}
		if (!PlayerPrefs.HasKey(GlobalData.axeLevel))
		{
			PlayerPrefs.SetInt(GlobalData.axeLevel, 0);
		}
		if (!PlayerPrefs.HasKey(GlobalData.hammerLevel))
		{
			PlayerPrefs.SetInt(GlobalData.hammerLevel, 0);
		}
		if (!PlayerPrefs.HasKey(GlobalData.bombLevel))
		{
			PlayerPrefs.SetInt(GlobalData.bombLevel, 0);
		}
		
		
	}
	
	
	private void NextWeapon()
	{
		weaponIndex++;
		switch (weaponIndex)
		{
			case 2:
				_leftSwapButton.interactable = true;
				_weaponContainer[0].transform.DOLocalMove(rightSwapPos, tweenDuration);
				_weaponContainer[1].transform.DOLocalMove(midPos, tweenDuration);
				
				if (_weapons.TryGetValue(1, out weaponVal))
				{
					activeWeaponId = weaponVal.ID;
				}

				ButtonEligibleForPurchase();
				UpdateUIData(1);
				Upgrade();
				break;
			case 3:
				_weaponContainer[1].transform.DOLocalMove(rightSwapPos, tweenDuration);
				_weaponContainer[2].transform.DOLocalMove(midPos, tweenDuration);
				
				if (_weapons.TryGetValue(2, out weaponVal))
				{
					activeWeaponId = weaponVal.ID;
				}

				ButtonEligibleForPurchase();
				UpdateUIData(2);
				Upgrade();
				break;
			case 4:
				_weaponContainer[2].transform.DOLocalMove(rightSwapPos, tweenDuration);
				_weaponContainer[3].transform.DOLocalMove(midPos, tweenDuration);
				
				if (_weapons.TryGetValue(3, out weaponVal))
				{
					activeWeaponId = weaponVal.ID;
				}

				ButtonEligibleForPurchase();
				UpdateUIData(3);
				Upgrade();
				_rightSwapButton.interactable = false;
				break;
		}
		
		
		if (weaponIndex >= totalWeapon)
		{
			weaponIndex = totalWeapon;
		}
	}
	
	private void PreviousWeapon()
	{
		weaponIndex--;
		switch (weaponIndex)
		{
			case 1:
				_weaponContainer[0].transform.DOLocalMove(midPos, tweenDuration);
				_weaponContainer[1].transform.DOLocalMove(leftSwapPos, tweenDuration);
				
				if (_weapons.TryGetValue(0, out weaponVal))
				{
					activeWeaponId = weaponVal.ID;
				}

				ButtonEligibleForPurchase();
				UpdateUIData(0);
				Upgrade();
				_leftSwapButton.interactable = false;
				break;
			case 2:
				_weaponContainer[1].transform.DOLocalMove(midPos, tweenDuration);
				_weaponContainer[2].transform.DOLocalMove(leftSwapPos, tweenDuration);
				
				if (_weapons.TryGetValue(1, out weaponVal))
				{
					activeWeaponId = weaponVal.ID;
				}

				ButtonEligibleForPurchase();
				UpdateUIData(1);
				Upgrade();
				break;
			case 3:
				_weaponContainer[2].transform.DOLocalMove(midPos, tweenDuration);
				_weaponContainer[3].transform.DOLocalMove(leftSwapPos, tweenDuration);
				
				if (_weapons.TryGetValue(2, out weaponVal))
				{
					activeWeaponId = weaponVal.ID;
				}

				ButtonEligibleForPurchase();
				UpdateUIData(2);
				Upgrade();
				_rightSwapButton.interactable = true;
				break;
		}

		if (weaponIndex <= 1)
		{
			weaponIndex = 1;
		}
	}


	private SO_Weapon weaponVal;
	private void UpdateUIData(int val)
	{
		if (_weapons.TryGetValue(val, out weaponVal))
		{
			weapon_Name.text = weaponVal.weaponName.ToString();
			weapon_Desc.text = weaponVal.weaponDesc.ToString();
			weapon_Damage.text = weaponVal.damage.ToString();
			weapon_attackRange.text = weaponVal.attackRange.ToString();
			
			//Player Level
			switch (weaponVal.weaponType)
			{
				case WeaponType.Arrow:
					weapon_level.text = PlayerPrefs.GetInt(GlobalData.arrowLevel,0).ToString();
					//Speciality Icon
					for (int i = 0; i < specialityIcon.Length; i++)
					{
						specialityIcon[i].sprite = weaponVal.specialityIcon[i];
					}

					break;
				case WeaponType.Axe:
					weapon_level.text = PlayerPrefs.GetInt(GlobalData.axeLevel,0).ToString();
					//Speciality Icon
					for (int i = 0; i < specialityIcon.Length; i++)
					{
						specialityIcon[i].sprite = weaponVal.specialityIcon[i];
					}
					break;
				case WeaponType.Hammer:
					weapon_level.text = PlayerPrefs.GetInt(GlobalData.hammerLevel,0).ToString();
					//Speciality Icon
					for (int i = 0; i < specialityIcon.Length; i++)
					{
						specialityIcon[i].sprite = weaponVal.specialityIcon[i];
					}
					
					break;
				case WeaponType.Bomb:
					weapon_level.text = PlayerPrefs.GetInt(GlobalData.bombLevel,0).ToString();
					//Speciality Icon
					for (int i = 0; i < specialityIcon.Length; i++)
					{
						specialityIcon[i].sprite = weaponVal.specialityIcon[i];
					}
					
					break;
			}
			
		}
		
	}
	
	
	private void Upgrade()
	{
		SO_Weapon weaponUp;

		for (int i = 0; i < _weapons.Count; i++)
		{
			if (_weapons.TryGetValue(i, out weaponUp))
			{
				
				weapon_Damage.text = weaponVal.damage.ToString();
				weapon_attackRange.text = weaponVal.attackRange.ToString();
				
				string buttonText;
				switch (weaponVal.weaponType)
				{
					case WeaponType.Arrow:
						 buttonText = "Lv " + PlayerPrefs.GetInt(GlobalData.arrowLevel, 0).ToString() + " Upgrade";
						 ButtonLevelUpgradeText.text = buttonText;
						 upgradeCoin.text = weaponVal.weaponLevel[PlayerPrefs.GetInt(GlobalData.arrowLevel, 0)]
							 .CoinToUpgrade.ToString();
						break;
					case WeaponType.Axe:
						 buttonText = "Lv " + PlayerPrefs.GetInt(GlobalData.axeLevel, 0).ToString() + " Upgrade";
						 ButtonLevelUpgradeText.text = buttonText;
						 upgradeCoin.text = weaponVal.weaponLevel[PlayerPrefs.GetInt(GlobalData.axeLevel, 0)]
							 .CoinToUpgrade.ToString();
						break;
					case WeaponType.Hammer:
						buttonText = "Lv " + PlayerPrefs.GetInt(GlobalData.hammerLevel, 0).ToString() + " Upgrade";
						ButtonLevelUpgradeText.text = buttonText;
						upgradeCoin.text = weaponVal.weaponLevel[PlayerPrefs.GetInt(GlobalData.hammerLevel, 0)]
							.CoinToUpgrade.ToString();
						break;
					case WeaponType.Bomb:
						buttonText = "Lv " + PlayerPrefs.GetInt(GlobalData.bombLevel, 0).ToString() + " Upgrade";
						ButtonLevelUpgradeText.text = buttonText;
						upgradeCoin.text = weaponVal.weaponLevel[PlayerPrefs.GetInt(GlobalData.bombLevel, 0)]
							.CoinToUpgrade.ToString();
						break;
				}
			}
		}
		
	}

	private void UpgradeButton()
	{
		//DONE : Measure which weapon to upgrade
		//DONE : Check if coin ready to spent
		//DONE : Level Upgrade
		//DONE : Button Level text upgrade
		//DONE : Add damage
		//DONE : Add attack
		//DONE : Coin Loose
		//TODO : If exist coin is not enough for upgrade
		
		SO_Weapon weaponUp;
		for (int i = 0; i < _weapons.Count; i++)
		{
			if (_weapons.TryGetValue(i, out weaponUp))
			{
				if (weaponUp.ID == activeWeaponId)
				{

					switch (weaponUp.weaponType)
					{
						case WeaponType.Arrow:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.arrowLevel, 0)].CoinToUpgrade))
							{
								
								
								//Add damage
								weaponUp.damage = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.arrowLevel, 0)]
									           .damagePoint +
								           weaponUp.damage;
								

								//Add attack
								weaponUp.attackRange = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.arrowLevel, 0)]
									                  .attackPoint +
								                  weaponUp.attackRange;
								
								//Coin Loose
								GameCoinGemEnergyCount.CoinRemoveCount(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.arrowLevel, 0)].CoinToUpgrade);
								
								
								// Level Upgrade
								WeaponLevelProgress.AddWeaponLevel(1, WeaponType.Arrow);
							}
							break;
						
						case WeaponType.Axe:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.axeLevel, 0)].CoinToUpgrade))
							{
								
								
								//Add damage
								weaponUp.damage = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.axeLevel, 0)]
									                  .damagePoint +
								                  weaponUp.damage;
								

								//Add attack
								weaponUp.attackRange = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.axeLevel, 0)]
									                       .attackPoint +
								                       weaponUp.attackRange;
								
								//Coin Loose
								GameCoinGemEnergyCount.CoinRemoveCount(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.axeLevel, 0)].CoinToUpgrade);
								
								// Level Upgrade
								WeaponLevelProgress.AddWeaponLevel(1, WeaponType.Axe);
							}
							
							break;
						
						case WeaponType.Hammer:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.hammerLevel, 0)].CoinToUpgrade))
							{
								
								
								//Add damage
								weaponUp.damage = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.hammerLevel, 0)]
									                  .damagePoint +
								                  weaponUp.damage;
								

								//Add attack
								weaponUp.attackRange = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.hammerLevel, 0)]
									                       .attackPoint +
								                       weaponUp.attackRange;
								
								
								//Coin Loose
								GameCoinGemEnergyCount.CoinRemoveCount(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.hammerLevel, 0)].CoinToUpgrade);
								
								
								// Level Upgrade
								WeaponLevelProgress.AddWeaponLevel(1, WeaponType.Hammer);
								
							}
							
							
							break;
						
						case WeaponType.Bomb:
							if (EnergyCoinGemController.singleton.IsCoinReady
								(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.bombLevel, 0)].CoinToUpgrade))
							{
								
								
								
								//Add damage
								weaponUp.damage = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.bombLevel, 0)]
									                  .damagePoint +
								                  weaponUp.damage;
								

								//Add attack
								weaponUp.attackRange = weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.bombLevel, 0)]
									                       .attackPoint +
								                       weaponUp.attackRange;
								
								//Coin Loose
								GameCoinGemEnergyCount.CoinRemoveCount(weaponUp.weaponLevel[PlayerPrefs.GetInt(GlobalData.bombLevel, 0)].CoinToUpgrade);
								
								
								// Level Upgrade
								WeaponLevelProgress.AddWeaponLevel(1, WeaponType.Bomb);
								
							}
							
							break;
					}

				}
			}
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	#endregion

    }


    [System.Serializable]
    public class WeaponContainer
    {
	    public GameObject _weaponContainer;
    }

    public static class WeaponLevelProgress
    {
	    public static void AddWeaponLevel(int val , WeaponType weaponType)
	    {
		    int wLevel;

		    switch (weaponType)
		    {
			    case WeaponType.Arrow:
				    wLevel = PlayerPrefs.GetInt(GlobalData.arrowLevel, 0);
				    foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				    {
					    if (wc.weapon.weaponType == WeaponType.Arrow)
					    {
						    int temp = val + wLevel;
						    PlayerPrefs.SetInt(GlobalData.arrowLevel, temp);
					    }
				    }
				    break;
			    
			    case WeaponType.Axe:
				    wLevel = PlayerPrefs.GetInt(GlobalData.axeLevel, 0);
				    
				    foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				    {
					    if (wc.weapon.weaponType == WeaponType.Axe)
					    {
						    int temp = val + wLevel;
						    PlayerPrefs.SetInt(GlobalData.axeLevel, temp);
					    }
				    }
				    break;
			    
			    case WeaponType.Bomb:
				    wLevel = PlayerPrefs.GetInt(GlobalData.bombLevel, 0);
				    foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				    {
					    if (wc.weapon.weaponType == WeaponType.Bomb)
					    {
						    int temp = val + wLevel;
						    PlayerPrefs.SetInt(GlobalData.bombLevel, temp);
					    }
				    }
				    break;
			    
			    case WeaponType.Hammer:
				    wLevel = PlayerPrefs.GetInt(GlobalData.hammerLevel, 0);
				    
				    foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				    {
					    if (wc.weapon.weaponType == WeaponType.Hammer)
					    {
						    int temp = val + wLevel;
						    PlayerPrefs.SetInt(GlobalData.hammerLevel, temp);
					    }
				    }
				    break;
			    
		    }
		    
		    
		    
		    
		    
		    
	    }
    }

    // public static class WeaponAttackDamage
    // {
	   //  public static void AddAttack(int val , WeaponType weaponType)
	   //  {
		  //   int wLevel;
		  //   int attack;
    //
		  //   switch (weaponType)
		  //   {
			 //    case WeaponType.Arrow:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.arrowLevel, 0);
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Arrow)
				// 	    {
				// 		    attack = wc.weapon.weaponLevel[wLevel].attackPoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].attackPoint = attack;
				// 	    }
				//     }
				//     break;
			 //    
			 //    case WeaponType.Axe:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.axeLevel, 0);
				//     
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Axe)
				// 	    {
				// 		    attack = wc.weapon.weaponLevel[wLevel].attackPoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].attackPoint = attack;
				// 	    }
				//     }
				//     break;
			 //    
			 //    case WeaponType.Bomb:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.bombLevel, 0);
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Bomb)
				// 	    {
				// 		    attack = wc.weapon.weaponLevel[wLevel].attackPoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].attackPoint = attack;
				// 	    }
				//     }
				//     break;
			 //    
			 //    case WeaponType.Hammer:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.hammerLevel, 0);
				//     
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Hammer)
				// 	    {
				// 		    attack = wc.weapon.weaponLevel[wLevel].attackPoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].attackPoint = attack;
				// 	    }
				//     }
				//     break;
			 //    
		  //   }
	   //  }
	   //  
	   //  public static void AddDamage(int val , WeaponType weaponType)
	   //  {
		  //   int wLevel;
		  //   int damage;
    //
		  //   switch (weaponType)
		  //   {
			 //    case WeaponType.Arrow:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.arrowLevel, 0);
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Arrow)
				// 	    {
				// 		    damage = wc.weapon.weaponLevel[wLevel].damagePoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].damagePoint = damage;
				// 	    }
				//     }
				//     break;
			 //    
			 //    case WeaponType.Axe:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.axeLevel, 0);
				//     
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Axe)
				// 	    {
				// 		    damage = wc.weapon.weaponLevel[wLevel].damagePoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].damagePoint = damage;
				// 	    }
				//     }
				//     break;
			 //    
			 //    case WeaponType.Bomb:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.bombLevel, 0);
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Bomb)
				// 	    {
				// 		    damage = wc.weapon.weaponLevel[wLevel].damagePoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].damagePoint = damage;
				// 	    }
				//     }
				//     break;
			 //    
			 //    case WeaponType.Hammer:
				//     wLevel = PlayerPrefs.GetInt(GlobalData.hammerLevel, 0);
				//     
				//     foreach (var wc in SO_DataController.Singleton.weaponC.Container)
				//     {
				// 	    if (wc.weapon.weaponType == WeaponType.Hammer)
				// 	    {
				// 		    damage = wc.weapon.weaponLevel[wLevel].damagePoint + val;
				// 		    wc.weapon.weaponLevel[wLevel].damagePoint = damage;
				// 	    }
				//     }
				//     break;
			 //    
		  //   }
	   //  }
    // }
    
    
}// NameSpace


