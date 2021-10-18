using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "Inventory System", menuName = "Scriptable Objects/Inventory/InventorySystem")]
	public class SO_InventorySlotObject : ScriptableObject
	{
		public List<InventorySlot> Container = new List<InventorySlot>();

		public void AddItem(SO_InventoryItemObject _item, int _amount)
		{
			bool hasItem = false;
			for (int i = 0; i < Container.Count; i++)
			{
				if (Container[i].item == _item)
				{
					Container[i].AddAmount(_amount);
					hasItem = true;
					break;
				}
			}

			if (!hasItem)
			{
				Container.Add(new InventorySlot(_item, _amount));
			}
		}

	}


	[System.Serializable]
	public class InventorySlot
	{
		public SO_InventoryItemObject item;
		public int amount;

		public InventorySlot(SO_InventoryItemObject _item, int _amount)
		{
			item = _item;
			amount = _amount;
		}

		public void AddAmount(int value)
		{
			amount += value;
		}
	}
}
