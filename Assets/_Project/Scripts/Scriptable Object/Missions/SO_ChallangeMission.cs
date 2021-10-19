using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_ChallangeMission", menuName = "Scriptable Objects/Mission/Challange")]
    public class SO_ChallangeMission : ScriptableObject
    {
	    [Header("CHALLANGE MISSION :")]
	    public List<ChallangeMissionSO> ChallangeMissionData;

    }

    [System.Serializable]
    public class ChallangeMissionSO
    {
	    public int ChallangeM_Number;
	    public bool allClaim;
	    [Header("DATA :")]
	    public List<MissionData> missionData;
	    //Mission Bonus

    }

    [System.Serializable]
    public class MissionData
    {
	    public bool readyToClaim;
	    public bool claimed;
	    public Sprite challangeM_mainIcon;
	    public string challangeM_Title;
	    [TextArea(5,5)]
	    public string challangeM_Description;

	    [Space(10)] 
	    public int challangeM_targetCount;
	    public int challangeM_currentCount;

	    [Space(10)] 
	    public Sprite challangeM_rewardIcon;
	    public int challangeM_rewardCount;
	    public MissionRewardType missionRewardType;
    }

    public enum MissionRewardType
    {
	    Coin,
	    Gem,
	    Energy,
	    Chest
    }
    
    
}

