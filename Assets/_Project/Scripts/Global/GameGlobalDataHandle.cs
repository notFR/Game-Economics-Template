using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TD.gameeconomics
{
	public class GameGlobalDataHandle : MonoBehaviour
	{
		#region Variables

		#endregion

		#region Functions

		private void Start()
		{
			if (!PlayerPrefs.HasKey(GlobalData.firstTimeSignInAward))
			{
				PlayerPrefs.SetInt(GlobalData.firstTimeSignInAward, 0);
				PlayerPrefs.SetInt(GlobalData.playerCurrentLevel, 0);
				PlayerPrefs.SetInt(GlobalData.playerCurrentXP, 0);
			}

			// Data Create and Write
			//InitializeGameData();
		}


		void InitializeGameData()
		{
			CheckAndInitCharacterData();
		}
		
		

		#region Character Level Data

		void CheckAndInitCharacterData()
		{
			if (!FileTool.IsFileExists(GlobalData.charLevelProgressData))
			{
				string initStr = "Id,LevelNumber,LevelTargetXP,LevelCurrentXP,LevelBonusCount";
				FileTool.createORwriteFile(GlobalData.charLevelProgressData, initStr);
			}

			//WriteCharacterProgressData();
		}

		// void WriteCharacterProgressData()
		// {
		// 	CSVFileTool csv = new CSVFileTool(FileTool.RootPath + GlobalData.charLevelProgressData);
		//
		// 	int  index = -1;
		// 	for (int i=1; i<=csv.rowCount; ++i) {
		// 		if (levelID.CompareTo(csv[i, "Id"]) == 0)
		// 		{
		// 			index = i;
		// 			break;
		// 		}
		// 	}
		// 	
		// 	if ( index>=0) {
		// 		csv[index, 2]=1.ToString();
		// 		csv[index, 3]=1.ToString();
		// 		csv[index, 4]=1.ToString();
		// 		csv[index, 5]=1.ToString();
		// 	} else {
		// 		//if ID not exist , new a row
		// 		//string[] newRow = {levelID,LevelNumber,LevelTargetXP,LevelCurrentXP,LevelBonusCount};
		// 		//csv.AddNewRow (newRow);
		//
		// 	}
		// 	csv.SaveCSV ();
		// }

		#endregion
		
		

		#endregion

	}
}
