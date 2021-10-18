using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TD.gameeconomics
{
	public class UI_LostManager : MonoBehaviour
	{
		#region Variables

		[Header("UI Reff :")] 
		[SerializeField] private TMP_Text gemCountText;
		
		
		
		private UI_GamePlayManager ui_gameplayManager;
		private int gemVal = 3;

		#endregion

		#region Functions

		private void OnEnable()
		{
			ui_gameplayManager = Object.FindObjectOfType<UI_GamePlayManager>().GetComponent<UI_GamePlayManager>();
			GemState();
		}

		void GemState()
		{
			if (GameCoinGemEnergyCount.IsGemReadyToReduce(gemVal))
			{
				gemCountText.color = Color.white;
				gemCountText.text = gemVal.ToString();
				ui_gameplayManager.GemVal = gemVal;
			}
			else
			{
				gemCountText.text = gemVal.ToString();
				ui_gameplayManager.GemVal = gemVal;
				gemCountText.color = Color.red;
			}
		}

		#endregion

	}
}
