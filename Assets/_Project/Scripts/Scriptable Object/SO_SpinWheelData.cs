using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_spinWheelData", menuName = "Scriptable Objects/Spin Wheel/Data")]
    public class SO_SpinWheelData : ScriptableObject
    {

	    [Header("Spin Wheel SO Data :")] 
	    public int dailySpinCountTo;
	    public int dailySpinCountToMax;
	    public List<SpinWheelSO> spinWheelDatas;
    }


    [System.Serializable]
    public class SpinWheelSO
    {
	    public List<SpinWheel> spinWheelData;
    }

    [System.Serializable]
    public class SpinWheel
    {
	    public LuckySpinType _spinType;
	    public Sprite Icon;
	    public int count;
	    public SO_InventoryItemObject chest;
    }
    
    public enum LuckySpinType
    {
	    Coin,
	    Energy,
	    Gem,
	    NormalChest,
	    Rune
    }
}


