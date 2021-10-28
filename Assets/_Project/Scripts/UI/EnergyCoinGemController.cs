using System;
using FXnRXn;
using FXnRXn.Manager;
using TMPro;
using UnityEngine;

namespace TD.gameeconomics
{
	public class EnergyCoinGemController : MonoBehaviour
	{
		#region Singleton

		public static EnergyCoinGemController singleton;

		private void Awake()
		{
			if (singleton == null)
			{
				singleton = this;
			}
		}

		#endregion
		
		
		#region Variables

		[Header("UI TEXT :")] 
		[SerializeField] private TMP_Text _energyText;
		[SerializeField] private TMP_Text _coinText;
		[SerializeField] private TMP_Text _gemText;

		#endregion

		#region Functions

		private void Start()
		{
			
		}

		private void OnDestroy()
		{
			
		}

		public void ValueInit()
		{
			EnergyInit();
			CoinInit();
			GemInit();
		}

		public void EnergyInit()
		{
			_energyText.text = GameCoinGemEnergyCount.GetEnergyCount().ToString();
		}
		
		public void CoinInit()
		{
			if (!PlayerPrefs.HasKey(GlobalData.coin))
			{
				PlayerPrefs.SetInt(GlobalData.coin, 0);
			}

			int c = PlayerPrefs.GetInt(GlobalData.coin, 0);
			_coinText.text = c.ToString();
		}
		
		public void GemInit()
		{
			if (!PlayerPrefs.HasKey(GlobalData.gem))
			{
				PlayerPrefs.SetInt(GlobalData.gem, 0);
			}

			int g = PlayerPrefs.GetInt(GlobalData.gem, 0);
			_gemText.text = g.ToString();
		}


		private void OnEnable()
		{
			ValueInit();
			EventManager.StartListening(GlobalData.energyAdd, EnergyAdd);
			EventManager.StartListening(GlobalData.energyRemove, EnergyRemove);
			EventManager.StartListening(GlobalData.coinAdd, CoinAdd);
			EventManager.StartListening(GlobalData.coinRemove, CoinRemove);
			EventManager.StartListening(GlobalData.gemAdd, GemAdd);
			EventManager.StartListening(GlobalData.gemRemove, GemRemove);
			
			
		}

		private void OnDisable()
		{
			EventManager.StopListening(GlobalData.energyAdd, EnergyAdd);
			EventManager.StopListening(GlobalData.energyRemove, EnergyRemove);
			EventManager.StopListening(GlobalData.coinAdd, CoinAdd);
			EventManager.StopListening(GlobalData.coinRemove, CoinRemove);
			EventManager.StopListening(GlobalData.gemAdd, GemAdd);
			EventManager.StopListening(GlobalData.gemRemove, GemRemove);
			
			
		}
		

		public void Update()
		{
			_energyText.text = GameCoinGemEnergyCount.GetEnergyCount().ToString();
			_coinText.text = GameCoinGemEnergyCount.GetCoinCount().ToString();
			_gemText.text = GameCoinGemEnergyCount.GetGemCount().ToString();
		}

		#region energy

		public void EnergyAdd(int e)
		{
			GameCoinGemEnergyCount.EnergyAddCount(e);
			
		}

		public void EnergyRemove(int e)
		{
			GameCoinGemEnergyCount.EnergyRemoveCount(e);
			
		}

		public bool IsEnergyReady(int val) 
		{
			int e = PlayerPrefs.GetInt(GlobalData.energy, 0);
			if ((e-val) >= 0)
			{
				return true;
			}
			else
			{
				return false;
			}
				
		}

		#endregion

		#region coin

		public void CoinAdd(int c)
		{
			GameCoinGemEnergyCount.CoinAddCount(c);
		}

		public void CoinRemove(int c)
		{
			GameCoinGemEnergyCount.CoinRemoveCount(c);
		}

		public bool IsCoinReady(int val) 
		{
			int e = PlayerPrefs.GetInt(GlobalData.coin, 0);
			if ((e-val) >= 0)
			{
				return true;
			}
			else
			{
				return false;
			}
				
		}

		#endregion
		
		#region gem

		public void GemAdd(int g)
		{
			GameCoinGemEnergyCount.GemAddCount(g);
			
		}

		public void GemRemove(int g)
		{
			GameCoinGemEnergyCount.GemRemoveCount(g);
		}

		public bool IsGemReady(int val)
		{

			return GameCoinGemEnergyCount.IsGemReadyToReduce(val);
		}

		#endregion


		private void LateUpdate()
		{
			if (PlayerPrefs.GetInt(GlobalData.coin, 0) <= 0)
			{
				PlayerPrefs.SetInt(GlobalData.coin, 0);
			}
		}


		public string ScoreShow(int Score)
		{
			float Scor = Score;
			string result;
			string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
			int i;
 
			for (i = 0; i < ScoreNames.Length; i++)
				if (Scor < 900)
					break;
				else Scor = Mathf.Floor(Scor / 100f) / 10f;
 
			if (Scor == Mathf.Floor(Scor))
				result = Scor.ToString() + ScoreNames[i];
			else result = Scor.ToString("F1") + ScoreNames[i];
			return result;
		}
		
		
		
		
		#endregion

	}


	public static class GameCoinGemEnergyCount
	{
		public static int GetEnergyCount()
		{
			int e = 0;
			if (!PlayerPrefs.HasKey(GlobalData.energy))
			{
				PlayerPrefs.SetInt(GlobalData.energy, 0);
			}
			else
			{
				e = PlayerPrefs.GetInt(GlobalData.energy, 0);
			}

			return e;
		}

		public static void EnergyAddCount(int e)
		{
			int tmp = PlayerPrefs.GetInt(GlobalData.energy, 0);
			tmp += e;
			PlayerPrefs.SetInt(GlobalData.energy, tmp);
		}

		public static void EnergyRemoveCount(int e)
		{
			int tmp = PlayerPrefs.GetInt(GlobalData.energy, 0);
			tmp -= e;
			PlayerPrefs.SetInt(GlobalData.energy, tmp);
		}
		
		
		
		
		public static int GetCoinCount()
		{
			int c = 0;
			if (!PlayerPrefs.HasKey(GlobalData.coin))
			{
				PlayerPrefs.SetInt(GlobalData.coin, 0);
			}
			else
			{
				c = PlayerPrefs.GetInt(GlobalData.coin, 0);
			}

			return c;
		}

		public static void CoinAddCount(int c)
		{
			int tmp = PlayerPrefs.GetInt(GlobalData.coin, 0);
			tmp += c;
			PlayerPrefs.SetInt(GlobalData.coin, tmp);
		}

		public static void CoinRemoveCount(int c)
		{
			int tmp = PlayerPrefs.GetInt(GlobalData.coin, 0);
			tmp -= c;
			PlayerPrefs.SetInt(GlobalData.coin, tmp);
		}
		
		
		
		
		public static int GetGemCount()
		{
			int g = 0;
			if (!PlayerPrefs.HasKey(GlobalData.gem))
			{
				PlayerPrefs.SetInt(GlobalData.gem, 0);
			}
			else
			{
				g = PlayerPrefs.GetInt(GlobalData.gem, 0);
			}

			return g;
		}

		public static void GemAddCount(int g)
		{
			int tmp = PlayerPrefs.GetInt(GlobalData.gem, 0);
			tmp += g;
			PlayerPrefs.SetInt(GlobalData.gem, tmp);
		}

		public static void GemRemoveCount(int g)
		{
			int tmp = PlayerPrefs.GetInt(GlobalData.gem, 0);
			tmp -= g;
			PlayerPrefs.SetInt(GlobalData.gem, tmp);
		}

		public static bool IsGemReadyToReduce(int val)
		{
			int e = PlayerPrefs.GetInt(GlobalData.gem, 0);
			if ((e-val) >= 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
	
	
	
}
