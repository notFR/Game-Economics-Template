using System;
using System.Collections.Generic;
using FXnRXn.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.gameeconomics
{
	public class UI_InventoryManager : MonoBehaviour
	{

		#region Singleton

		public static UI_InventoryManager singleton;

		private void Awake()
		{
			if (singleton == null)
			{
				singleton = this;
			}
			else
			{
				Destroy(this.gameObject);
			}
		}

		#endregion
		
		
		
		
		#region Variables

		[SerializeField] private Transform slotParent;
		
		
		//[Header("Setting :")]
		private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
		private SO_InventorySlotObject inventory;


		[Header("Info Data UI :")] 
		[SerializeField] private TMP_Text rankText;
		[SerializeField] private TMP_Text itemNameText;
		[SerializeField] private TMP_Text infoText;
		[SerializeField] private Image itemIcon;
		[SerializeField] private Sprite itemIconNull;
		[Space(10)]
		[SerializeField] private Transform statParent;
		[Space(10)] 
		[SerializeField] private Button sellButton;
		[SerializeField] private Button selectButton;
		[Space(10)] 
		[SerializeField] private TMP_Text sellAmountText;
		
		[HideInInspector]public int itemID;
		[HideInInspector] public int sellAmount;
		#endregion

		#region Functions

		private void Start()
		{
			sellButton.onClick.RemoveAllListeners();
			 sellButton.onClick.AddListener(() =>
			 {
				 //From Scriptable
				 RemoveFromContainer(itemID);
				 //For UI
				 for (int i = 0; i < slotParent.childCount; i++)
			 	{
			 		slotParent.GetChild(i).GetComponent<UI_ItemSelection>().GetID(itemID);
			 	}

				 if (EnergyCoinGemController.singleton != null)
				 {
					 EnergyCoinGemController.singleton.CoinAdd(sellAmount);
				 }
				 SellAmountShow(false);
			 });
			 
			 selectButton.onClick.RemoveAllListeners();
			 selectButton.onClick.AddListener(() =>
			 {
				 RemoveFromContainer(itemID);
				 for (int i = 0; i < slotParent.childCount; i++)
				 {
					 slotParent.GetChild(i).GetComponent<UI_ItemSelection>().GetID(itemID);
				 }
				 SellAmountShow(false);
			 });
			 
			 CreateItemSlot();
			 
		}

		private void OnEnable()
		{
			if (SO_DataController.Singleton != null && SO_DataController.Singleton.InventorySystem)
			{
				inventory = SO_DataController.Singleton.InventorySystem;
			}
			
			ResetDataByDefault();
			SellAmountShow(false);
		}

		private void Update()
		{
			UpdateDisplay();
		}

		void CreateItemSlot()
		{
			if (inventory.Container.Count == 0)
			{
				itemsDisplayed.Clear();
			}
			for (int i = 0; i < inventory.Container.Count; i++)
			{
				GameObject slotPrefab = Instantiate(inventory.Container[i].item.itemPrefab) as GameObject;
				slotPrefab.transform.SetParent(slotParent, false);
				slotPrefab.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString();
				itemsDisplayed.Add(inventory.Container[i], slotPrefab);
			}
		}

		void UpdateDisplay()
		{
			for (int i = 0; i < inventory.Container.Count; i++)
			{
				if (itemsDisplayed.ContainsKey(inventory.Container[i]))
				{
					if (itemsDisplayed[inventory.Container[i]] != null)
					{
						itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString();
					}
					
				}
				else
				{
					GameObject slotPrefab = Instantiate(inventory.Container[i].item.itemPrefab) as GameObject;
					slotPrefab.transform.SetParent(slotParent, false);
					slotPrefab.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString();
					itemsDisplayed.Add(inventory.Container[i], slotPrefab);
				}
				
			}
		}

		public void RemoveFromContainer(int _id)
		{
			for (int i = 0; i < inventory.Container.Count; i++)
			{
				if (inventory.Container[i].item.ID == _id)
				{
					inventory.Container.RemoveAt(i);
				}
			}
		}

		public void AddItemDataUI(string _rankText, string _itemNameText, string _infoText, Sprite _itemIcon, List<GameObject> prefab)
		{
			rankText.text = _rankText.ToString();
			itemNameText.text = _itemNameText.ToString();
			infoText.text = _infoText.ToString();
			itemIcon.sprite = _itemIcon;

			foreach (var obj in prefab)
			{
				GameObject go = Instantiate(obj) as GameObject;
				go.transform.SetParent(statParent, false);
			}
		}


		public void ResetDataByDefault()
		{
			rankText.text = String.Empty;
			itemNameText.text = String.Empty;
			infoText.text = String.Empty;
			itemIcon.sprite = itemIconNull;
			if (statParent.childCount != 0)
			{
				for (int i = 0; i < statParent.childCount; i++)
				{
					Destroy(statParent.GetChild(i).gameObject);
				}
			}
			
		}
		
		
		
		// 
		public void SellAmountShow(bool t)
		{
			if (t)
			{
				sellAmountText.text = sellAmount.ToString();
			}
			else
			{
				sellAmountText.text = string.Empty;
			}
		}
		
		
		
		
		
		
		#endregion

	}
	
	
}
