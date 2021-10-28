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
        public SO_ChallangeMission data_challangeMission;

        [Header("UI :")] [SerializeField] 
        private List<UIChallangeMission> ui_challangeMission;
        

		#endregion
    
        #region Function

        private void Start()
        {
	        if (!PlayerPrefs.HasKey(GlobalData.challangeMission))
	        {
		        PlayerPrefs.GetInt(GlobalData.challangeMission, 0);
	        }
	        
	        
	        if (SO_DataController.Singleton != null && SO_DataController.Singleton.ChallangeMissionData != null)
	        {
		        data_challangeMission = SO_DataController.Singleton.ChallangeMissionData;
	        }
	        
	        
        }

        private void OnEnable()
        {
	        this.Wait(0.1f, () =>
	        {
		        int index = GetCurrentChallangeMissionNumber();
		        ChallangeMissionSO chMissionData = data_challangeMission.ChallangeMissionData[index];
	        
		        UpdateChallangeData(chMissionData);
	        });
	        
	        
        }


        int GetCurrentChallangeMissionNumber()
        {
	        
	        if (data_challangeMission != null)
	        {
		        foreach (var dataNumber in data_challangeMission.ChallangeMissionData)
		        {
			        if (dataNumber.allClaim)
			        {
				        int temp = PlayerPrefs.GetInt(GlobalData.challangeMission);
				        temp += 1;
				        PlayerPrefs.SetInt(GlobalData.challangeMission, temp);
			        }
		        }
		        
	        }
	        int index = PlayerPrefs.GetInt(GlobalData.challangeMission);
	        return index;
        }

        void UpdateChallangeData(ChallangeMissionSO chMissionData)
        {
	        if (data_challangeMission != null)
	        {
		        
		        //Debug.Log(" == " + chMissionData.missionData[0].challangeM_Title.ToString());
		        // UI data Add
		        for (int i = 0; i < chMissionData.missionData.Count; i++)
		        {
			        // Main
			        ui_challangeMission[i].Title_Icon.sprite = chMissionData.missionData[i].challangeM_mainIcon;
			        ui_challangeMission[i].Title_text.text = chMissionData.missionData[i].challangeM_Title;
			        ui_challangeMission[i].Desc_text.text = chMissionData.missionData[i].challangeM_Description;
			        
			        //Slider
			        string sliderText = chMissionData.missionData[i].challangeM_currentCount.ToString() + "/"
				        + chMissionData.missionData[i].challangeM_targetCount.ToString();
			        ui_challangeMission[i].sliderText.text = sliderText;

			        ui_challangeMission[i].slider.value = chMissionData.missionData[i].challangeM_currentCount;
			        ui_challangeMission[i].slider.maxValue = chMissionData.missionData[i].challangeM_targetCount;

			        ui_challangeMission[i].Reward_Icon.sprite = chMissionData.missionData[i].challangeM_rewardIcon;
			        ui_challangeMission[i].Reward_text.text = chMissionData.missionData[i].challangeM_rewardCount.ToString();

			        

					//Decision
			        if (chMissionData.missionData[i].challangeM_currentCount >= chMissionData.missionData[i].challangeM_targetCount)
			        {
				        chMissionData.missionData[i].readyToClaim = true;
			        }
			        
			        //TODO : Problem with claim button 

			        // if (ui_challangeMission[i].claimButton.interactable)
			        // {
				       //  ui_challangeMission[i].claimButton.onClick.AddListener(() =>
				       //  {
					      //   chMissionData.missionData[i].claimed = true;
					      //   ui_challangeMission[i].claimButton.gameObject.SetActive(false);
					      //   ui_challangeMission[i].claimedBanner.gameObject.SetActive(true);
				       //  });
			        // }
			        
			        
			        
			        
			        if (chMissionData.missionData[i].readyToClaim && !chMissionData.missionData[i].claimed)
			        {
				        ui_challangeMission[i].claimButton.interactable = true;
			        }

			        if (chMissionData.missionData[i].readyToClaim && chMissionData.missionData[i].claimed)
			        {
				        ui_challangeMission[i].claimButton.gameObject.SetActive(false);
				        ui_challangeMission[i].claimedBanner.gameObject.SetActive(true);
			        }

		        }
	        }
	        
        }
        
        
        

        #endregion
    
    }
    
    
    
    
    
    
    
    

    [System.Serializable]
    public class UIChallangeMission
    {
	    public Image Title_Icon;
	    public TMP_Text Title_text;
	    public TMP_Text Desc_text;
	    [Space(10)] 
	    public Slider slider;
	    public TMP_Text sliderText;
	    [Space(10)]
	    public Image Reward_Icon;
	    public TMP_Text Reward_text;
	    [Space(10)] 
	    public Button claimButton;
	    public GameObject claimedBanner;
    }
}

