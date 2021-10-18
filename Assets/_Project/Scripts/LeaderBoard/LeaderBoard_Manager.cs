using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace TD.gameeconomics
{
    public class LeaderBoard_Manager : MonoBehaviour
    {
        #region Variable

		#endregion
    
        #region Function

        private void Start()
        {
	        RequestLeaderboard();
        }

        public void RequestLeaderboard()
        {
	        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
	        {
		        StatisticName = "PlayerScore",
		        StartPosition = 0,
		        MaxResultsCount = 2
	        }, result => DisplayLeaderboard(result), FailureCallback);
        }

        public void DisplayLeaderboard(GetLeaderboardResult result)
        {
	        List<PlayerLeaderboardEntry> LeaderboardRetrived = result.Leaderboard;
			//Show Player LeaderBoard
			Debug.Log(LeaderboardRetrived[0].DisplayName);
			foreach (var leaderboard in LeaderboardRetrived)
			{
				if (leaderboard.DisplayName == PlayerPrefs.GetString(GlobalData.playerUsername))
				{
					Debug.Log("Player Name : " + leaderboard.DisplayName + " == " + "Player Rank : " + leaderboard.Position);
				}
			}
	        for (int i = 0; i < LeaderboardRetrived.Count; i++)
	        {
		        
	        }
        }
        
        
        private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult)
        {
	        Debug.Log("Successfully submitted high score");
        }

        private void FailureCallback(PlayFabError error)
        {
	        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
	        Debug.LogError(error.GenerateErrorReport());
        }

		#endregion
    
    }
}

