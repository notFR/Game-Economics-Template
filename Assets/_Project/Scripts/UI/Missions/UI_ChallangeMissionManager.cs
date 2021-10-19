using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.gameeconomics
{
    public class UI_ChallangeMissionManager : MonoBehaviour
    {
        #region Variable
        //Data
        private SO_ChallangeMission data_challangeMission;

        [Header("UI :")] [SerializeField] 
        private List<UIChallangeMission> ui_challangeMission;

		#endregion
    
        #region Function

        private void Start()
        {
	        if (SO_DataController.Singleton != null && SO_DataController.Singleton.ChallangeMissionData != null)
	        {
		        data_challangeMission = SO_DataController.Singleton.ChallangeMissionData;
	        }
        }

        private void OnEnable()
        {
	        
	        
        }
        
        
        
        
        

        #endregion
    
    }

    [System.Serializable]
    public class UIChallangeMission
    {
	    public Image Title_Icon;
	    public TMP_Text Title_text;
	    public TMP_Text Desc_text;
    }
}

