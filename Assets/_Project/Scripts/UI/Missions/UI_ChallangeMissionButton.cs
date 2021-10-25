using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TD.gameeconomics
{
	public class UI_ChallangeMissionButton : MonoBehaviour
	{
		#region Variable

		[SerializeField] private int index;
		[SerializeField] private Button claimButton;
		[SerializeField] private GameObject claimedBanner;
		[SerializeField] private MissionRewardType _missionRewardType;
		[SerializeField] private MissionType _rewardType;

		private UI_ChallangeMissionManager ui_challangeMissionManager;

		#endregion

		#region Function

		private void Start()
		{
			ui_challangeMissionManager = Object.FindObjectOfType<UI_ChallangeMissionManager>()
				.GetComponent<UI_ChallangeMissionManager>();
			
			this.Wait(.5f, () =>
			{
				_missionRewardType = ui_challangeMissionManager.data_challangeMission.ChallangeMissionData[PlayerPrefs.GetInt(GlobalData.challangeMission, 0)].missionData[index]
					.missionRewardType;
			
				_rewardType = ui_challangeMissionManager.data_challangeMission.ChallangeMissionData[PlayerPrefs.GetInt(GlobalData.challangeMission, 0)].missionData[index]
					.missionType;
				
				if (ui_challangeMissionManager.data_challangeMission.
					ChallangeMissionData[PlayerPrefs.GetInt(GlobalData.challangeMission, 0)].missionData[index].claimed)
				{
					claimedBanner.SetActive(true);
					claimButton.gameObject.SetActive(false);
				}
				else
				{
					claimedBanner.SetActive(false);
					claimButton.gameObject.SetActive(true);
				}
				
				
			});

			claimButton.onClick.AddListener(() =>
			{
				if (!ui_challangeMissionManager.data_challangeMission.
					ChallangeMissionData[PlayerPrefs.GetInt(GlobalData.challangeMission, 0)].missionData[index].claimed)
				{
					//AddReward

					ClaimedReward();
					
					//UI
					claimedBanner.SetActive(true);
					ui_challangeMissionManager.data_challangeMission
						.ChallangeMissionData[PlayerPrefs.GetInt(GlobalData.challangeMission, 0)].missionData[index]
						.claimed = true;
					claimButton.gameObject.SetActive(false);
				}
				
			});

			

		}

		void ClaimedReward()
		{
			if (_rewardType == MissionType.PlayerWinCount)
			{
				if (_missionRewardType == MissionRewardType.Coin)
				{
					int rewardVal = ui_challangeMissionManager.data_challangeMission
						.ChallangeMissionData[PlayerPrefs.GetInt(GlobalData.challangeMission, 0)].missionData[index]
						.challangeM_rewardCount;
					
					GameCoinGemEnergyCount.CoinAddCount(rewardVal);
				}
			}
			
		}
		

		#endregion

	}
}

