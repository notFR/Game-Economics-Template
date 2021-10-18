using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

namespace TD.gameeconomics
{
    public class LeaderBoard_Manager : MonoBehaviour
    {
        #region Variable

        [Header("Player Ranking :")] 
        [SerializeField] private TMP_Text playerRankText;
        [SerializeField] private TMP_Text playerRankNameText;
        [SerializeField] private TMP_Text playerRankPointText;

        [Header("Other Ranking :")] 
        [SerializeField] private List<OtherPlayerRank> _otherPlayerRanks;
		[Space(10)]
        [SerializeField] private Transform rank_parent;
        

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
		        MaxResultsCount = 10
	        }, result => DisplayLeaderboard(result), FailureCallback);
        }

        public void DisplayLeaderboard(GetLeaderboardResult result)
        {
	        List<PlayerLeaderboardEntry> LeaderboardRetrived = result.Leaderboard;
	        
			//Show Player LeaderBoard
			
			foreach (var leaderboard in LeaderboardRetrived)
			{
				if (leaderboard.DisplayName == PlayerPrefs.GetString(GlobalData.playerUsername))
				{
					//Rank
					int pos = leaderboard.Position + 1;
					playerRankText.text = pos.ToString();
					//Display Name
					playerRankNameText.text = leaderboard.DisplayName.ToString();
					//Point
					playerRankPointText.text = leaderboard.StatValue.ToString();
				}
			}
			
			
			//Show Player LeaderBoard

			for (int i = 0; i < 10; i++)
			{

				if (LeaderboardRetrived[i].Position != null && LeaderboardRetrived[i].DisplayName != null && LeaderboardRetrived[i].StatValue != null)
				{
            						
					// 1, 2, 3
					for (int j = 0; j < 3; j++)
					{
						// if (LeaderboardRetrived[j].Position == null || LeaderboardRetrived[j].DisplayName == null || LeaderboardRetrived[j].StatValue == null)
						// {
						// 	break;
						// }
            						
            						
						GameObject tmp = Instantiate(_otherPlayerRanks[j].rankObj) as GameObject;
						//Rank
						tmp.transform.GetChild(1).GetComponent<TMP_Text>().text =
							(LeaderboardRetrived[j].Position + 1).ToString();
            						
						//Display Name
						tmp.transform.GetChild(3).GetComponent<TMP_Text>().text =
							LeaderboardRetrived[j].DisplayName.ToString();
            
						//Point
						tmp.transform.GetChild(5).GetComponent<TMP_Text>().text =
							LeaderboardRetrived[j].StatValue.ToString();
            						
						tmp.transform.SetParent(rank_parent, false);
            
					}
            
					for (int k = 3; k < 10; k++)
					{
						// if (LeaderboardRetrived[k].Position == null || LeaderboardRetrived[k].DisplayName == null || LeaderboardRetrived[k].StatValue == null)
						// {
						// 	break;
						// }
            						
						GameObject tmp = Instantiate(_otherPlayerRanks[k].rankObj) as GameObject;
						//Rank
						tmp.transform.GetChild(0).GetComponent<TMP_Text>().text =
							(LeaderboardRetrived[k].Position + 1).ToString();
            						
						//Display Name
						tmp.transform.GetChild(2).GetComponent<TMP_Text>().text =
							LeaderboardRetrived[k].DisplayName.ToString();
            
						//Point
						tmp.transform.GetChild(4).GetComponent<TMP_Text>().text =
							LeaderboardRetrived[k].StatValue.ToString();
            						
						tmp.transform.SetParent(rank_parent, false);
					} 
				}
            				
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

    [System.Serializable]
    public class OtherPlayerRank
    {
	    public GameObject rankObj;
    }
}

