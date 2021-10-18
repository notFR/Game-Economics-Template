using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TD.gameeconomics
{
	public class UI_ItemSelection : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
	{
		#region Singleton

		public static UI_ItemSelection singleton;

		private void Awake()
		{
			if (singleton == null)
			{
				singleton = this;
			}
		}

		#endregion
		
		
		#region Variables
		[Header("Image Border :")]
		[SerializeField] private Image Border;
		[SerializeField] private Color BorderColorDefault;
		[SerializeField] private Color BorderColorOver;
		[SerializeField] private Color BorderColorHighlight;

		[Header("SO Data Name")] 
		[SerializeField] private string normalChestSOName;
		[SerializeField] private string heroSOName;
		
		[SerializeField] private InventoryItemType _itemType;
		
		[HideInInspector]
		public bool selected;
		
		private SO_ChestItem chestInfo;
		private SO_HeroItem heroInfo;
		
		List<GameObject> prefab = new List<GameObject>();


		#endregion

		#region Functions

		private void Start()
		{
			ResetAllButtons();
		}

		private void OnEnable()
		{
			chestInfo = Resources.Load<SO_ChestItem>("Scriptable Data/Inventory/" + normalChestSOName);
			heroInfo = Resources.Load<SO_HeroItem>("Scriptable Data/Inventory/" + heroSOName);
			
		}

		public void GetID(int id)
		{
			if (_itemType == InventoryItemType.NormalChest)
			{
				if (chestInfo.ID == id)
				{
					UI_InventoryManager.singleton.ResetDataByDefault();
					this.Wait(.5f, () =>
					{
						Destroy(this.gameObject);
					});
					
					
				}
			}
			if (_itemType == InventoryItemType.Hero)
			{
				if (heroInfo.ID == id)
				{
					UI_InventoryManager.singleton.ResetDataByDefault();
					this.Wait(.5f, () =>
					{
						Destroy(this.gameObject);
					});
					
					
				}
			}
				
		}


		//on mouse enter
		public void OnPointerEnter(PointerEventData eventData){
			Select();
		}
		
		//on mouse exit
		public void OnPointerExit(PointerEventData eventData){
			Deselect();
		}

		//on click
		public void OnPointerClick(PointerEventData eventData){
			OnClick();
		}
		
		//select
		public void Select()
		{
			if(Border && !selected) Border.color = BorderColorOver;
			
		}

		//deselect
		public void Deselect()
		{
			if(Border && !selected) Border.color = BorderColorDefault;
		}

		//On Click
		public void OnClick(){
			
			
			ResetAllButtons();
			selected = true;
			if(Border) Border.color = BorderColorHighlight;

			if (UI_InventoryManager.singleton != null)
			{
				//RESET
				UI_InventoryManager.singleton.ResetDataByDefault();
				
				prefab.Clear();
				
				//ADD
				switch (_itemType)
				{
					case InventoryItemType.NormalChest:

						UI_InventoryManager.singleton.itemID = chestInfo.ID;
						UI_InventoryManager.singleton.sellAmount = chestInfo.sellAmount;
						UI_InventoryManager.singleton.SellAmountShow(true);
						
						//Coin
						if (chestInfo.coinPrefab_UI != null)
						{
							prefab.Add(chestInfo.coinPrefab_UI);
							GameObject t = chestInfo.coinPrefab_UI;
							t.transform.GetChild(0).GetComponent<TMP_Text>().text = chestInfo.coin.ToString();
						}
						
						//Hero
						if (chestInfo.heroPrefab_UI != null)
						{
							prefab.Add(chestInfo.heroPrefab_UI);
							GameObject t = chestInfo.heroPrefab_UI;
							t.transform.GetChild(0).GetComponent<Image>().sprite = chestInfo.heroIcon;
							t.transform.GetChild(2).GetComponent<TMP_Text>().text = "+1".ToString();
						}
						
						//Hero Upgrade
						if (chestInfo.heroUpgradePoint != 0)
						{
							
						}
						//Energy
						if (chestInfo.energyPrefab_UI != null)
						{
							prefab.Add(chestInfo.energyPrefab_UI);
						}
						//Gem
						if (chestInfo.gemPrefab_UI != null)
						{
							prefab.Add(chestInfo.gemPrefab_UI);
							GameObject t = chestInfo.gemPrefab_UI;
							t.transform.GetChild(0).GetComponent<TMP_Text>().text = chestInfo.gem.ToString();
						}
						
						UI_InventoryManager.singleton.AddItemDataUI(chestInfo.typeName, chestInfo.name, chestInfo.description, chestInfo.itemIcon, prefab);
						
						break;
					
					case InventoryItemType.Hero:
						UI_InventoryManager.singleton.itemID = heroInfo.ID;
						UI_InventoryManager.singleton.sellAmount = heroInfo.sellAmount;
						UI_InventoryManager.singleton.SellAmountShow(true);
						//Health
						if (heroInfo.healthPrefab_UI != null)
						{
							prefab.Add(heroInfo.healthPrefab_UI);
							GameObject t = heroInfo.healthPrefab_UI;
							t.transform.GetChild(2).GetComponent<TMP_Text>().text = heroInfo.heroHealth.ToString();
						}
						
						//Defense
						if (heroInfo.defensePrefab_UI != null)
						{
							prefab.Add(heroInfo.defensePrefab_UI);
							GameObject t = heroInfo.defensePrefab_UI;
							t.transform.GetChild(2).GetComponent<TMP_Text>().text = heroInfo.defenseHealth.ToString();
						}
						
						//Attack
						if (heroInfo.attackPrefab_UI != null)
						{
							prefab.Add(heroInfo.attackPrefab_UI);
							GameObject t = heroInfo.attackPrefab_UI;
							t.transform.GetChild(2).GetComponent<TMP_Text>().text = heroInfo.attackDamage.ToString();
						}
						
						
						UI_InventoryManager.singleton.AddItemDataUI(heroInfo.typeName, heroInfo.name, heroInfo.description, heroInfo.itemIcon, prefab);
						break;
				}
			}
		}
		
		
		public void ResetAllButtons(){
			UI_ItemSelection[] allButtons = GameObject.FindObjectsOfType<UI_ItemSelection>();
			foreach(UI_ItemSelection button in allButtons) { 
				button.Border.color = button.BorderColorDefault;
				button.selected = false;
			}
		}

		#endregion

	}
}
