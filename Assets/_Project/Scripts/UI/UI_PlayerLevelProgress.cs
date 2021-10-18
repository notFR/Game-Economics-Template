using System;
using System.Collections;
using System.Text;
using FXnRXn.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.gameeconomics
{
	public class UI_PlayerLevelProgress : MonoBehaviour
	{
		#region Variables

		[Header(("UI :"))] 
		[SerializeField] private TMP_Text playerLevelNumberText;
		[SerializeField] private TMP_Text playerLevelXP;
		[SerializeField] private Slider xpSlider;

		

		#endregion

		#region Functions

		private void Start()
		{
			//ShowPlayerProgress();

			if (!PlayerPrefs.HasKey(GlobalData.playerXPSliderVal))
			{
				PlayerPrefs.SetFloat(GlobalData.playerXPSliderVal, 0);
			}
			
		}

		private void OnEnable()
		{
			ShowPlayerProgress();
			//XPSlider(PlayerPrefs.GetFloat(GlobalData.playerXPSliderVal, 0));
			EventManager.StartListening(GlobalData.SetPlayerCurrentXP, AddPlayerCurrentXP);
		}

		private void OnDisable()
		{
			EventManager.StopListening(GlobalData.SetPlayerCurrentXP, AddPlayerCurrentXP);
		}

		public void ShowPlayerProgress()
		{
			// Show Player Level
			int currentLvl = PlayerPrefs.GetInt(GlobalData.playerCurrentLevel, 0);
			playerLevelNumberText.text = currentLvl.ToString();
			
			//Show Player XP
			if (SO_DataController.Singleton != null)
			{
				int val = PlayerPrefs.GetInt(GlobalData.playerCurrentXP, 0);
				playerLevelXP.text =
					val.ToString() + "/" +
					SO_DataController.Singleton.GetPlayerProgressData(currentLvl+1).levelTargetXP.ToString();
			}

			
		}

		void XPSlider(float val)
		{
			PlayerPrefs.SetFloat(GlobalData.playerXPSliderVal, val);
			xpSlider.value = val;
			// if (!PlayerPrefs.HasKey("cacheSliderVal"))
			// {
			// 	PlayerPrefs.SetFloat("cacheSliderVal", 0);
			// }
			//
			// float startVal = PlayerPrefs.GetFloat("cacheSliderVal", 0);
			// StartCoroutine(LerpSlider(2f, startVal, val));
		}
		
		
		// IEnumerator LerpSlider(float lerpDuration, float startValue, float endValue)
		// {
		// 	float timeElapsed = 0;
		//
		// 	while (timeElapsed < lerpDuration)
		// 	{
		// 		int valueToLerp =(int) Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
		// 		timeElapsed += Time.deltaTime;
		// 		xpSlider.value = valueToLerp;
		// 		yield return null;
		// 	}
		// 	PlayerPrefs.SetFloat("cacheSliderVal", endValue);
		// }


		public void AddPlayerCurrentXP(int val)
		{
			
			GamePlayerLevelProgress.AddPlayerXPLevel(val);
			ShowPlayerProgress();

		}


		public void TEST_AddXP()
		{
			AddPlayerCurrentXP(40);
		}


		#endregion

	}


	public static class GamePlayerLevelProgress
	{
		public static void AddPlayerXPLevel(int val)
		{
			int l = PlayerPrefs.GetInt(GlobalData.playerCurrentXP, 0);// get current xp
			l += val;// increment it
			int currentLvl = PlayerPrefs.GetInt(GlobalData.playerCurrentLevel, 0); // get current player level
			
			if (l<= SO_DataController.Singleton.GetPlayerProgressData(currentLvl+1).levelTargetXP) // If current Xp not cross target XP
			{
				PlayerPrefs.SetInt(GlobalData.playerCurrentXP, l);
				
				//SLider
				// int ll = SO_DataController.Singleton.GetPlayerProgressData(currentLvl + 1).levelTargetXP;
				// float temp = ((1/ ll) * PlayerPrefs.GetInt(GlobalData.playerCurrentXP, 0));
				// XPSlider(temp);
			}
			else
			{
				int extraVal = l - SO_DataController.Singleton.GetPlayerProgressData(currentLvl + 1).levelTargetXP;
				PlayerPrefs.SetInt(GlobalData.playerCurrentXP, extraVal);
				PlayerPrefs.SetInt(GlobalData.playerCurrentLevel, currentLvl + 1);
				//Slider
				// int ll = SO_DataController.Singleton.GetPlayerProgressData(PlayerPrefs.GetInt(GlobalData.playerCurrentLevel, 0)).levelTargetXP;
				// float temp = ((1/ ll) * PlayerPrefs.GetInt(GlobalData.playerCurrentXP, 0));
				// XPSlider(temp);
				
				//EventManager.TriggerEvent(GlobalData.playerLevelChange, PlayerPrefs.GetInt(GlobalData.playerCurrentLevel, 0));
			}
		}
	}
}
