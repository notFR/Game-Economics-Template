using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_rewardWeekData", menuName = "Scriptable Objects/Reward Week/Data")]
    public class SO_RewardWeek : ScriptableObject
    {
		[Header("Reward SO Data :")]
	    public List<RewardWeekData> rewardWeekDatas;
    }

    [System.Serializable]
    public class RewardWeekData
    {
	    public bool allClaimed;
	    [Header("Day 7 Data:")]
	    public bool isLocked;
	    public bool readytoclaim;
	    public bool isclaimed;
	    public Sprite rewardSprite;
	    public int amount;
	    public SO_InventoryItemObject chest;
	    
	    [Header("1-6 Days :")]
	    public List<WeekData> weekDatas;
    }
    
    [System.Serializable]
    public class WeekData
    {
	    public bool isLocked;
	    public bool readytoclaim;
	    public bool isclaimed;
	    public WeekRewardType type;
	    public Sprite rewardSprite;
	    public int amount;
	    public SO_InventoryItemObject chest;
    }

    public enum WeekRewardType
    {
	    Coin,
	    Energy,
	    Gem,
	    NormalChest
    }
}


