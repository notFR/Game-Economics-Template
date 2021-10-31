using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace TD.gameeconomics
{
    public class UserInfoManager : MonoBehaviour
    {
	#region Variable

	[SerializeField] private TMP_Text tagID;
	[Header("Player Stat UI :")] 
	[SerializeField] private TMP_Text highestScoreText;
	[SerializeField] private TMP_Text highestStageText;
	[SerializeField] private TMP_Text mostWinText;
	[SerializeField] private TMP_Text soloVictoriesText;
	[SerializeField] private TMP_Text duoVictoriesText;
	[SerializeField] private TMP_Text threeV3VictoriesText;
	[SerializeField] private TMP_Text bossVictoriesText;
	[SerializeField] private TMP_Text totalPlayText;
	[SerializeField] private TMP_Text destroyDamageText;
	
	
	private int _highestScore;
	private int _highestStage;
	private int _mostWins;
	private int _soloVictories;
	private int _duoVictories;
	private int _3V3Victories;
	private int _bossVictories;
	private int _totalPlay;
	private int _destroyDamage;

	#endregion

	#region Function

	private void Start()
	{
		
	}

	private void OnEnable()
	{
		
		_highestStage = PlayerStatistics.GetHighestStage();
		_mostWins = PlayerStatistics.GetMostWin();
		_soloVictories = PlayerStatistics.GetSoloVictories();
		_duoVictories = PlayerStatistics.GetDuoVictories();
		_3V3Victories = PlayerStatistics.Get3V3Victories();
		_bossVictories = PlayerStatistics.GetBossVictories();
		_totalPlay = PlayerStatistics.GetTotalPlay();
		_destroyDamage = PlayerStatistics.GetDestroyDamage();
		
		
		//
		
		PlayFabClientAPI.GetAccountInfo
		(
			new GetAccountInfoRequest(),
			result =>
			{
				tagID.text = result.AccountInfo.PlayFabId;
			},
			error =>
			{
				Debug.Log(error.GenerateErrorReport());
			}
		);
		
		
		// Player Highest Score
            
		if (SO_DataController.Singleton != null)
		{
			PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
				{
					StatisticName = "PlayerScore",
				},
				result =>
				{
					List<PlayerLeaderboardEntry> LeaderboardRetrived = result.Leaderboard;
					foreach (var leaderboard in LeaderboardRetrived)
					{
						if (leaderboard.DisplayName == PlayerPrefs.GetString(GlobalData.playerUsername))
						{
							SO_DataController.Singleton.playerStat._highestScore = leaderboard.StatValue;
							
							if (highestScoreText != null)
							{
								highestScoreText.text = leaderboard.StatValue.ToString();
							}
						}
					}
                        
                        
				},
				error =>
				{
					Debug.Log(error.GenerateErrorReport());
				});
			
		}
		
		
		
		
		this.Wait(.1f, () =>
		{
			PlayerStatUIUpdate();

		});
	}


	void PlayerStatUIUpdate()
	{
		
		if (highestStageText != null)
		{
			highestStageText.text = _highestStage.ToString();
		}
		if (mostWinText != null)
		{
			mostWinText.text = _mostWins.ToString();
		}
		if (soloVictoriesText != null)
		{
			soloVictoriesText.text = _soloVictories.ToString();
		}
		if (duoVictoriesText != null)
		{
			duoVictoriesText.text = _duoVictories.ToString();
		}
		if (threeV3VictoriesText != null)
		{
			threeV3VictoriesText.text = _3V3Victories.ToString();
		}
		if (bossVictoriesText != null)
		{
			bossVictoriesText.text = _bossVictories.ToString();
		}
		if (totalPlayText != null)
		{
			totalPlayText.text = _totalPlay.ToString();
		}
		if (destroyDamageText != null)
		{
			destroyDamageText.text = _destroyDamage.ToString();
		}
		
	}
	

	#endregion
        
    }
}


